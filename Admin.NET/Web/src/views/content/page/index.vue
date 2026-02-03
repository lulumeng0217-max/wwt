<template>
	<div class="builder-layout">
		<!-- ================= 左侧面板：页面结构树 ================= -->
		<aside class="left-panel">
			<!-- 顶部固定区域：新建 + 搜索 -->
			<div class="panel-header">
				<div class="header-actions">
					<span class="panel-title">Pages</span>
					<el-tooltip content="Create New Page" placement="top">
						<el-button type="primary" :icon="Plus" circle size="small"
							@click="openCreateDialog"></el-button>
					</el-tooltip>
				</div>
				<!-- 搜索过滤框 -->
				<div class="search-box">
					<el-input v-model="searchKeyword" placeholder="Filter pages..." prefix-icon="Search" clearable
						size="small" />
				</div>
			</div>

			<!-- 树形列表区域 -->
			<div class="page-tree-container">
				<el-scrollbar>
					<el-tree ref="pageTreeRef" :data="pageTreeData" node-key="id"
						:props="{ label: 'title', children: 'children' }" :filter-node-method="filterPageNode"
						highlight-current default-expand-all :expand-on-click-node="false"
						@node-click="handlePageSelect">
						<template #default="{ node, data }">
							<div class="custom-tree-node" :class="{ 'is-active': currentPreviewId === data.id }">
								<div class="node-main">
									<el-icon class="node-icon">
										<component :is="data.pagetype === 'template' ? 'Files' : 'Document'" />
									</el-icon>
									<span class="node-label" :title="data.requestPath">{{ node.label }}</span>
								</div>
								<!-- 状态指示点 -->

								<el-button type="primary" link @click="EditPage(data.id)">Edit</el-button>
								<!-- <el-tooltip :content="data.status === 1 ? 'Enabled' : 'Disabled'" placement="right"
									:show-after="500">
									<span class="status-dot" :class="{ online: data.status === 1 }"></span>
								</el-tooltip> -->
							</div>
						</template>
					</el-tree>

					<!-- <div v-if="pageList.length === 0" class="empty-placeholder">
						<el-empty description="No pages" :image-size="60" />
					</div> -->
				</el-scrollbar>
			</div>
		</aside>

		<!-- ================= 中间面板：Iframe 预览区 ================= -->
		<main class="center-panel">
			<!-- 顶部工具栏 -->
			<div class="preview-toolbar">
				<div class="left-tools">
					<div class="device-switch">
						<el-radio-group v-model="deviceMode" size="small">
							<el-radio-button label="desktop"><el-icon>
									<Monitor />
								</el-icon></el-radio-button>
							<el-radio-button label="mobile"><el-icon>
									<Iphone />
								</el-icon></el-radio-button>
						</el-radio-group>
					</div>
				</div>

				<div class="url-display">
					<el-input v-model="currentUrl" readonly size="small" class="url-input">
						<template #prefix><el-icon>
								<Link />
							</el-icon></template>
					</el-input>
				</div>

				<div class="right-tools">
					<el-button type="primary" size="small" :loading="isSaving" :disabled="!hasChanges"
						@click="saveComponents">
						{{ isSaving ? 'Saving...' : 'Save Changes' }}
					</el-button>
				</div>
			</div>

			<!-- 画布容器 -->
			<div class="iframe-container">
				<div class="iframe-wrapper" :class="deviceMode">
					<iframe ref="previewIframe" :src="iframeSrc" frameborder="0" class="preview-iframe"
						@load="onIframeLoad"></iframe>
				</div>
			</div>

			<!-- ================= 右侧悬浮：组件结构树 ================= -->
			<!-- ================= 右侧悬浮面板控制 ================= -->

			<!-- 1. 重新打开面板的悬浮按钮 (当面板关闭时显示) -->
			<transition name="el-fade-in">
				<div v-if="!showRightPanel" class="right-panel-trigger" @click="showRightPanel = true"
					title="Open Structure">
					<el-icon>
						<Operation />
					</el-icon>
				</div>
			</transition>

			<!-- 2. 组件结构树面板 -->
			<transition name="el-zoom-in-left">
				<div v-show="showRightPanel" class="right-panel">
					<!-- 顶部：标题 + 操作按钮 -->
					<div class="panel-head">
						<div class="head-left">
							<el-icon class="mr-1">
								<Operation />
							</el-icon>
							<span class="title">Structure</span>
						</div>
						<div class="actions">
							<!-- 添加组件按钮 -->
							<el-button type="primary" size="small" icon="Plus" @click="openComponentDrawer">Add
								Component</el-button>
							<!-- 关闭按钮 -->
							<el-button text circle :icon="Close" size="small" @click="showRightPanel = false"
								title="Close Panel"></el-button>
						</div>
					</div>

					<!-- 内容：组件树 -->
					<div class="panel-body">
						<div v-if="previewBlocks.length === 0" class="empty-state">
							<el-empty description="No components" :image-size="60">
								<el-button type="primary" plain size="small" @click="openComponentDrawer('0')">Add Root
									Component</el-button>
							</el-empty>
						</div>

						<el-scrollbar v-else>
							<el-tree :data="componentTreeDisplay" node-key="id" default-expand-all draggable
								:allow-drop="allowDrop" :allow-drag="allowDrag" @node-drop="handleDrop"
								:props="{ label: 'label', children: 'children' }" class="structure-tree"
								:expand-on-click-node="false">
								<template #default="{ node, data }">
									<div class="custom-tree-node">
										<!-- 左侧：图标 + 名称 -->
										<div class="node-content">
											<!-- 根据类型显示不同颜色的图标 -->
											<el-icon class="node-icon" :class="getKindClass(data.kind)">
												<component :is="data.icon || 'Box'" />
											</el-icon>
											<span class="node-label">{{ node.label }}</span>
											<el-tag v-if="data.isNew" size="small" type="success" effect="plain"
												class="mini-tag">New</el-tag>
										</div>

										<!-- 右侧：操作按钮 (悬停显示) -->
										<div class="node-actions">
											<!-- 只有容器(Kind=10)或特殊布局才能添加子级 -->
											<!-- 注意：你需要确保你的 data 里包含 kind 属性 -->
											<el-button v-if="isContainer(data)" link type="primary" size="small"
												@click.stop="handleAppend(data)" title="Append Child"> Append
											</el-button>

											<el-button link type="danger" size="small"
												@click.stop="removeComponent(data.id)" title="Delete"> Delete
											</el-button>
										</div>
									</div>
								</template>
							</el-tree>
						</el-scrollbar>
					</div>

					<!-- 底部：如果有未保存更改显示提示 -->
					<div class="panel-footer" v-if="hasChanges">
						<div class="unsaved-bar">
							<el-icon class="is-loading">
								<Loading />
							</el-icon>
							<span>Unsaved changes</span>
						</div>
					</div>
				</div>
			</transition>
		</main>

		<!-- ================= 弹窗：新建页面表单 ================= -->
		<el-dialog v-model="dialogVisible" title="Create Page" width="700px" :close-on-click-modal="false"
			destroy-on-close append-to-body>
			<el-form ref="formRef" :model="formData" :rules="rules" label-width="100px" class="create-form">
				<el-tabs v-model="activeTab" class="custom-tabs">
					<!-- 基础信息 -->
					<el-tab-pane label="Basic Info" name="basic">
						<!-- 增加外层 padding div -->
						<div class="form-tab-content">
							<el-row :gutter="20">
								<el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
									<el-form-item label="Parent Page" prop="pid">
										<el-tree-select v-model="formData.pid" :data="pageTreeData"
											:props="{ label: 'title', children: 'children', value: 'id' }"
											placeholder="Select Parent (Default Root)" check-strictly clearable
											class="w-100" />
									</el-form-item>
								</el-col>

								<!-- Name 和 Path 分两列，但保持足够的垂直间距 -->
								<el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
									<el-form-item label="Name" prop="title">
										<el-input v-model="formData.title" placeholder="Page Title" />
									</el-form-item>
								</el-col>
								<el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
									<el-form-item label="Path" prop="requestPath">
										<el-input v-model="formData.requestPath" placeholder="e.g: /about">
											<template #prepend>/</template>
										</el-input>
									</el-form-item>
								</el-col>

								<el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
									<el-form-item label="pagetype" prop="pagetype">
										<el-select v-model="formData.pagetype" placeholder="Select Page Type"
											class="w-100">
											<el-option label="Standard Page" value="page" />
											<el-option label="Template" value="template" />
										</el-select>
									</el-form-item>
								</el-col>

								<el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
									<el-form-item label="Content" prop="sourceType">
										<el-radio-group v-model="sourceType">
											<el-radio-button label="empty">Blank Page</el-radio-button>
											<el-radio-button label="template">Copy Template</el-radio-button>
										</el-radio-group>
									</el-form-item>
								</el-col>

								<el-col :span="24" v-if="sourceType === 'template'">
									<div class="tpl-select-box">
										<el-form-item label="Template" style="margin-bottom: 0">
											<el-select v-model="formData.templateId" placeholder="Select a template"
												class="w-100">
												<el-option v-for="t in templateList" :key="t.id" :label="t.title"
													:value="t.id" />
											</el-select>
										</el-form-item>
									</div>
								</el-col>
							</el-row>
						</div>
					</el-tab-pane>

					<!-- SEO 设置 -->
					<el-tab-pane label="SEO & Meta" name="seo">
						<div class="form-tab-content">
							<el-col :xs="24" class="mb20">
								<el-form-item label="SEO Title" prop="subTitle">
									<el-input v-model="formData.subTitle" placeholder="Browser Title" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" class="mb20">
								<el-form-item label="Keywords">
									<el-input v-model="formData.keywords" type="textarea" :rows="3"
										placeholder="Comma separated keywords" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" class="mb20">
								<el-form-item label="Description">
									<el-input v-model="formData.description" type="textarea" :rows="3"
										placeholder="Meta description" />
								</el-form-item>
							</el-col>
						</div>
					</el-tab-pane>

					<!-- 高级设置 -->
					<el-tab-pane label="Advanced" name="advanced">
						<div class="form-tab-content">
							<el-row :gutter="20">
								<<el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
									<el-form-item label="Status">
										<el-switch v-model="formData.status" :active-value="1" :inactive-value="0"
											active-text="Enabled" />
									</el-form-item>
									</el-col>
									<el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
										<el-form-item label="Is Dynamic">
											<el-switch v-model="formData.isDynamic" active-text="Yes"
												inactive-text="No" />
										</el-form-item>
									</el-col>
							</el-row>
							<el-form-item label="OG Image">
								<el-input v-model="formData.ogImage" placeholder="Image URL for social sharing" />
							</el-form-item>
						</div>
					</el-tab-pane>
				</el-tabs>
			</el-form>
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="dialogVisible = false">Cancel</el-button>
					<el-button type="primary" @click="submitCreate">Create & Edit</el-button>
				</span>
			</template>
		</el-dialog>

		<!-- ================= 抽屉：添加组件 ================= -->
		<el-drawer v-model="componentDrawerVisible" title="Add Component" direction="rtl" size="420px"
			class="component-drawer" destroy-on-close>
			<div class="drawer-layout">
				<el-divider class="custom-divider" />
				<!-- 组件列表区域 -->
				<el-scrollbar class="component-scroll">
					<div class="component-grid">
						<div v-for="comp in availableComponentTypes" :key="comp.id" class="comp-card"
							@click="addComponent(comp)">
							<!-- 左侧图标区 -->
							<div class="card-icon-wrapper"
								:style="{ background: getComponentStyle(comp.componentKind).bg, color: getComponentStyle(comp.componentKind).color }">
								<el-icon :size="20">
									<component :is="getComponentStyle(comp.componentKind).icon" />
								</el-icon>
							</div>

							<!-- 中间文字区 -->
							<div class="card-content">
								<div class="card-header">
									<h4 class="comp-title">{{ comp.name }}</h4>
									<el-tag size="small" effect="plain" class="type-tag">{{
										getComponentStyle(comp.componentKind).label }}</el-tag>
								</div>
								<!-- 使用 stripHtml 清洗描述 -->
								<p class="comp-desc">{{ stripHtml(comp.description || '') || 'No description available'
									}}</p>
							</div>

							<!-- 右侧加号 -->
							<div class="card-action">
								<el-button type="primary" link icon="Plus" circle></el-button>
							</div>
						</div>
					</div>
				</el-scrollbar>
			</div>
		</el-drawer>
	</div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted, onUnmounted, nextTick, watch } from 'vue';
import { Monitor, Iphone, Plus, Close, Link, Search, Delete, Document, Files, Box, Grid, Operation, Loading, PriceTag, Coin, Connection } from '@element-plus/icons-vue';
import type { FormInstance, FormRules } from 'element-plus';
import { ElMessage, ElTree } from 'element-plus';
import { getAPI } from '/@/utils/axios-utils';
import { CmsComponentTypeApi, CmsComponentApi, CmsPageApi } from '/@/api-services';
import { CmsComponentDto, CmsPageOutput, AddCmsPageInput, CmsComponentTypeOutput, AddCmsComponentInput, ComponentKindEnum } from '/@/api-services/models';
import { nanoid } from 'nanoid';
import { id } from 'element-plus/es/locale';
// ---------------- 接口扩展 ----------------
// 为了适配 UI 树形结构，扩展定义
interface CmsPageTreeItem extends CmsPageOutput {
	children?: CmsPageTreeItem[];
	pid?: number; // 确保有 pid
}

interface PreviewBlock {
	id: string; // 临时唯一ID (UUID or temp_timestamp)
	originalId?: number; // 数据库真实ID
	component: string;
	componentTypeId: number;
	componentKind: ComponentKindEnum;
	pid: string; // 父组件ID (数据库ID或0) -> 注意：如果是新组件，这里逻辑比较复杂，通常前端模拟树需要临时ID，这里简化为只用 originalId 做父级关联，或者 0
	tempPid?: string; // 如果父级也是新建的，需要用临时ID关联 (高级功能，此处暂按 pid=0或真实ID处理)
	props: Record<string, any>;
	styles: Record<string, any>;
	sortOrder: number;
	isNew: boolean;
	isDeleted: boolean;
	isModified: boolean;
	children?: PreviewBlock[]; // 辅助生成树
}

// ---------------- 状态定义 ----------------
const state = reactive({
	pageList: [] as CmsPageOutput[],
	drawComponentTypes: [] as CmsComponentTypeOutput[],
});

// UI Ref
const pageTreeRef = ref<InstanceType<typeof ElTree>>();
const deviceMode = ref<'desktop' | 'mobile'>('desktop');
const currentPreviewId = ref<number>(0);
const showRightPanel = ref(true);
const iframeSrc = ref('about:blank');
const currentUrl = ref('');
const urlBase = 'http://127.0.0.1:3000/preview'; // 预览基地址

// 新建页面相关
const dialogVisible = ref(false);
const formRef = ref<FormInstance>();
const activeTab = ref('basic');
const sourceType = ref<'empty' | 'template'>('empty');
const selectedTemplateId = ref<number | undefined>(undefined);
const formData = reactive<AddCmsPageInput & { pid?: number }>({
	pid: undefined,
	title: '',
	requestPath: '',
	subTitle: '',
	pagetype: 'page',
	status: 1,
	isDynamic: false,
	description: '',
	keywords: '',
	robots: 'index, follow',
	ogTitle: '',
	ogType: 'website',
	ogImage: '',
});

// 验证规则
const rules = reactive<FormRules>({
	title: [{ required: true, message: 'Required', trigger: 'blur' }],
	requestPath: [{ required: true, message: 'Required', trigger: 'blur' }],
});

// 组件管理相关
const componentDrawerVisible = ref(false);
const availableComponentTypes = ref<CmsComponentTypeOutput[]>([]);
const previewBlocks = ref<PreviewBlock[]>([]);
const targetParentId = ref<string>('0'); // 添加组件时的目标父ID (Original ID)
const currentPreviewPage = ref<CmsPageOutput | null>(null);
const isSaving = ref(false);
const searchKeyword = ref('');

// ---------------- 计算属性 ----------------

// 1. 页面树形结构生成
const pageTreeData = computed(() => {
	// 深拷贝避免修改原数组
	const pages = JSON.parse(JSON.stringify(state.pageList)) as CmsPageTreeItem[];
	// 添加 pid 默认为 0 如果没有的话
	pages.forEach((p) => {
		if (p.pid === undefined) p.pid = 0;
	});
	return listToTree(pages);
});

// 2. 组件树形结构生成 (用于右侧展示和选择父级)
const componentTreeDisplay = computed(() => {
	// 过滤掉已删除的
	const activeBlocks = previewBlocks.value.filter((b) => !b.isDeleted);
	console.log('Active Blocks for Tree:', activeBlocks);
	// 构建树
	return listToTree(
		activeBlocks.map((b) => ({
			id: b.id,
			label: b.component,
			icon: getComponentIcon(b.componentKind),
			kind: b.componentKind,
			originalId: b.originalId,
			pid: b.pid,
			isNew: b.isNew,
		}))
	);
});

// 获取抽屉选择数据
const getComponentTypeList = async () => {
	const res = await getAPI(CmsComponentTypeApi).apiCmsComponentTypePagePost({ page: 1, pageSize: 999 });
	state.drawComponentTypes = res.data.result?.items || [];
	return res.data.result?.items || [];
};

const hasChanges = computed(() => {
	return previewBlocks.value.some((b) => b.isNew || b.isModified || b.isDeleted);
});

const templateList = computed(() => state.pageList.filter((p) => p.pagetype === 'template'));

// ---------------- 方法实现 ----------------

// 通用：列表转树
function listToTree<T extends { id?: number | string; pid?: number | string; children?: T[] }>(list: T[]): T[] {
	const map: Record<string, T> = {};
	const node: T[] = [];
	const roots: T[] = [];

	// 初始化 map 和 children
	for (let i = 0; i < list.length; i += 1) {
		map[list[i].id!] = list[i];
		list[i].children = [];
	}

	for (let i = 0; i < list.length; i += 1) {
		const item = list[i];
		const pid = item.pid ?? 0; // 假设 undefined 为 0
		if (pid !== 0 && map[pid]) {
			map[pid].children!.push(item);
		} else {
			roots.push(item);
		}
	}
	return roots;
}

// 页面搜索过滤
watch(searchKeyword, (val) => {
	pageTreeRef.value!.filter(val);
});
const filterPageNode = (value: string, data: CmsPageTreeItem) => {
	if (!value) return true;
	const path = data.requestPath?.toLowerCase() || '';
	const title = data.title?.toLowerCase() || '';
	return path.includes(value.toLowerCase()) || title.includes(value.toLowerCase());
};

// 获取图标
const getComponentIcon = (kind?: number) => {
	// 10: Container, 20: Layout, 30: Content
	if (kind === 10) return 'Grid';
	if (kind === 20) return 'Files';
	return 'Box';
};

// 初始化
onMounted(async () => {
	await fetchPageList();
	await fetchComponentTypes();
	await getComponentTypeList();
	window.addEventListener('message', onIframeMessage);
});

onUnmounted(() => {
	window.removeEventListener('message', onIframeMessage);
});

// API 调用
const fetchPageList = async () => {
	const res = await getAPI(CmsPageApi).apiCmsPagePagePost({ page: 1, pageSize: 9999 });
	state.pageList = res.data.result?.items || [];
};

const fetchComponentTypes = async () => {
	const res = await getAPI(CmsComponentTypeApi).apiCmsComponentTypePagePost({ page: 1, pageSize: 999 });
	res.data.result?.items?.forEach((comp) => {
		if (comp.defaultProps) {
			try {
				comp.defaultProps = JSON.parse(comp.defaultProps);
			} catch {
				comp.defaultProps = null;
			}
		} else {
			comp.defaultProps = null;
		}
	});
	availableComponentTypes.value = res.data.result?.items || [];
};

const fetchComponents = async (pageId: number) => {
	const res = await getAPI(CmsComponentApi).apiWWTCMSCmsComponentGetCmsComponentPageIdGet(pageId);
	// root 处理 props
	res.data.result?.forEach((comp) => {
		if (comp.props) {
			try {
				comp.props = JSON.parse(comp.props);
			} catch {
				comp.props = null;
			}
		} else {
			comp.props = null;
		}
	});
	return res.data.result || [];
};

// 页面操作
const openCreateDialog = () => {
	formData.pid = 0; // 默认根
	formData.title = '';
	formData.requestPath = '';
	formData.subTitle = '';
	activeTab.value = 'basic';
	dialogVisible.value = true;
};

const EditPage = async (id: number) => {
	formRef.value?.resetFields();
	sourceType.value = 'empty';
	selectedTemplateId.value = undefined;
	//get page data
	const res = await getAPI(CmsPageApi).apiCmsPageDetailGet(id);
	if (res.data.result?.templateId != undefined && res.data.result?.templateId != 0) {
		sourceType.value = 'template';
	}
	Object.assign(formData, res.data.result);
	dialogVisible.value = true;
};

const submitCreate = async () => {
	if (!formRef.value) return;
	await formRef.value.validate(async (valid) => {
		if (valid) {
			try {
				let res: any = null;
				// 调用 API
				if (formData.id) {
					res = await getAPI(CmsPageApi).apiCmsPageUpdatePost(formData);
				} else {
					res = await getAPI(CmsPageApi).apiCmsPageAddPost(formData);
				}
				ElMessage.success('Page created');
				dialogVisible.value = false;

				await fetchPageList();

				// 选中新页面并开始预览
				if (res.data.result) {
					// 查找新页面的对象
					const newPage = state.pageList.find((p) => p.id === res.data.result);
					if (newPage) handlePageSelect(newPage);
				}
			} catch (err) {
				// handle error
			}
		}
	});
};

const handlePageSelect = async (page: CmsPageOutput) => {
	currentPreviewId.value = page.id!;
	currentPreviewPage.value = page;
	currentUrl.value = `${urlBase}${page.requestPath}`;
	iframeSrc.value = `${currentUrl.value}?preview=true&t=${Date.now()}`; // 防止缓存
	// 加载组件数据
	const dbComponents = await fetchComponents(page.id!);
	// 转换为 PreviewBlock 格式
	const blocks: PreviewBlock[] = dbComponents.map((c) => ({
		id: c.id?.toString() || '',
		originalId: c.id,
		component: c.cmsComponentType?.name || 'Unknown',
		componentTypeId: c.componentTypeId || 0,
		pid: c.pid?.toString() || '0',
		props: parseProps(c.props),
		componentKind: c.cmsComponentType?.componentKind || ComponentKindEnum.NUMBER_30,
		sortOrder: c.sortOrder || 0,
		styles: c.styles ? JSON.parse(c.styles) : {},
		isNew: false,
		isDeleted: false,
		isModified: false,
	}));

	previewBlocks.value = blocks.sort((a, b) => a.sortOrder - b.sortOrder);
	showRightPanel.value = true;
};

// 辅助：解析 JSON
const parseProps = (str: string | null | undefined) => {
	if (!str) return {};
	try {
		return JSON.parse(str);
	} catch {
		return {};
	}
};


const openComponentDrawer = (parentId: string = '0') => {
	targetParentId.value = '0';
	componentDrawerVisible.value = true;
};

const addComponent = (type: CmsComponentTypeOutput) => {
	const newBlock: PreviewBlock = {
		id: nanoid(), // 使用 nanoid 生成唯一字符串 ID
		component: type.name!,
		componentTypeId: type.id!,
		pid: targetParentId.value, // 核心：使用选择的父ID
		props: parseProps(type.defaultProps),
		sortOrder: previewBlocks.value.length + 10,
		componentKind: type.componentKind || ComponentKindEnum.NUMBER_30,
		isNew: true,
		isDeleted: false,
		isModified: false,
		styles: {},
	};

	previewBlocks.value.push(newBlock);
	sendPreviewToIframe();
	componentDrawerVisible.value = false;
	ElMessage.success(`Added ${type.name} to ${targetParentId.value === '0' ? 'Root' : 'Parent ' + targetParentId.value}`);
};

const removeComponent = (id: string) => {
	const idx = previewBlocks.value.findIndex((b) => b.id === id);
	if (idx === -1) return;

	const block = previewBlocks.value[idx];
	if (block.isNew) {
		previewBlocks.value.splice(idx, 1);
	} else {
		block.isDeleted = true;
	}
	sendPreviewToIframe();
};

// 拖拽相关 (简单实现)
const allowDrop = (draggingNode: any, dropNode: any, type: string) => {
	// 仅允许同级排序或放入容器，此处简化为只允许同级排序
	return type !== 'inner';
};
const allowDrag = (node: any) => {
	return true;
};
const handleDrop = () => {
	// 实际拖拽需要更新 sortOrder 和 pid，这里暂时略过复杂逻辑，因为树结构展示是 computed 的
	// 如果要支持拖拽改变父级，需要修改 previewBlocks 中对应 item 的 pid
};

// Iframe 通信
const previewIframe = ref<HTMLIFrameElement>();
const onIframeLoad = () => {
	sendPreviewToIframe();
};

export interface CMSComponentBlock {
	id: string;
	type: 'GlobalAlertBar' | 'TextFeatureSection' | 'OffersSection' | string; // 扩展为 string 以兼容未来新组件
	sort_order: number;
	is_visible: boolean;
	// UnoCSS 类名字符串 (e.g. "mt-4 hidden md:flex")
	class?: string;

	// 业务参数 & 布局参数
	props: Record<string, any>;
	// 内联样式字符串 (e.g. "border: 1px solid red;")
	styles?: Record<string, any>;
	children?: CMSComponentBlock[];
}
// 辅助函数：将 PreviewBlock 转换为 CMSComponentBlock
const convertToCMSComponentBlock = (block: PreviewBlock): CMSComponentBlock => {
	return {
		id: block.id,
		type: block.component,
		sort_order: block.sortOrder,
		is_visible: true, // 默认为可见，可以根据业务需要调整
		class: block.props?.class || '',
		props: block.props || {},
		styles: block.styles || {},
		children: block.children?.map((child) => convertToCMSComponentBlock(child as PreviewBlock)),
	};
};

// 修改 sendPreviewToIframe 方法
const sendPreviewToIframe = () => {
	const iframe = previewIframe.value;
	if (!iframe?.contentWindow || !currentPreviewPage.value) return;

	// 1. 准备数据 - 过滤掉已删除的
	const activeBlocks = previewBlocks.value.filter((b) => !b.isDeleted);

	// 2. 构建树形结构并转换为 CMSComponentBlock 格式
	// 先给每个 block 添加 children 属性，用于 listToTree
	const blocksWithChildren = activeBlocks.map((b) => ({ ...b, children: [] as PreviewBlock[] }));
	const treeBlocks = listToTree(blocksWithChildren);
    console.log('Tree Blocks:', treeBlocks);
	// 3. 转换为 CMSComponentBlock 树
	const cmsBlocks = treeBlocks.map((b) => convertToCMSComponentBlock(b));
 console.log('Tree cmsBlocks:', cmsBlocks);
	// 4. 组装消息对象
	const messageData = {
		pageInfo: {
			id: currentPreviewPage.value.id,
			requestPath: currentPreviewPage.value.requestPath,
			title: currentPreviewPage.value.title,
			subTitle: currentPreviewPage.value.subTitle,
		},
		blocks: cmsBlocks,
	};

	// 5. 【核心修复】使用 JSON 序列化再反序列化，彻底去除 Vue Proxy 和不可克隆对象
	const cleanData = JSON.parse(JSON.stringify(messageData));

	console.log('Sending clean data:', cleanData);

	iframe.contentWindow.postMessage(
		{
			type: 'CMS_PREVIEW_UPDATE',
			data: cleanData,
		},
		'*'
	);
};

const onIframeMessage = (e: MessageEvent) => {
	if (e.data?.type === 'CMS_PREVIEW_READY') sendPreviewToIframe();
};

// 保存
const saveComponents = async () => {
	if (!currentPreviewPage.value) return;
	isSaving.value = true;
	console.log('Saving components:', previewBlocks.value);
	try {
		const newBlocks = previewBlocks.value.filter((b) =>!b.isDeleted);
		console.log('Saving newBlocks:', newBlocks);
		if (newBlocks.length > 0) {
			// 构造 componentList 数组
			const componentList = newBlocks.map((b) => ({
				id: b.id, // 如果有原始 ID 则传递
				componentTypeId: b.componentTypeId,
				pid: b.pid, // 确保 PID 是数字
				props: JSON.stringify(b.props), // 序列化 props
				styles: JSON.stringify(b.styles || {}), // 序列化 styles
				sortOrder: b.sortOrder,
				isVisible: true,
			}));

			const batchInput = {
				pageId: currentPreviewPage.value!.id,
				componentList: componentList,
			};

			// 发送请求
			await getAPI(CmsComponentApi).apiWWTCMSCmsComponentBatchAddPost(batchInput);
		}

		ElMessage.success('Saved successfully');
		// 3. 刷新页面数据，重新获取 ID
		await handlePageSelect(currentPreviewPage.value);
	} catch (e) {
		ElMessage.error('Save failed');
		console.error(e);
	} finally {
		isSaving.value = false;
	}
};
// ---------------- 新增/修改的工具函数 ----------------

// 1. 清洗 HTML 标签，只保留文本
const stripHtml = (html?: string) => {
	if (!html) return '';
	const tmp = document.createElement('DIV');
	tmp.innerHTML = html;
	let text = tmp.textContent || tmp.innerText || '';
	// 截取前50个字符，避免太长
	return text.length > 50 ? text.substring(0, 50) + '...' : text;
};

// 2. 根据 Kind 获取更丰富的图标配置
const getComponentStyle = (kind?: number) => {
	switch (kind) {
		case 10: // Container
			return { icon: 'Grid', color: '#409eff', bg: '#ecf5ff', label: 'Container' };
		case 20: // Layout
			return { icon: 'Files', color: '#e6a23c', bg: '#fdf6ec', label: 'Layout' };
		default: // Content (30) & Others
			return { icon: 'Document', color: '#67c23a', bg: '#f0f9eb', label: 'Content' };
	}
};

// ---------------- 新增/修改的方法 ----------------
// 1. 判断是否为容器 (决定是否显示 Append 按钮)
//  Kind: 10=Container, 20=Layout, 30=Content
const isContainer = (data: any) => {
	// 从 previewBlocks 查找该组件的详细信息以获取 kind
	const block = previewBlocks.value.find((b) => b.id === data.id);
	if (block && block.componentKind === ComponentKindEnum.NUMBER_10) return true; // Container

	return false;
};

// 2. 获取图标颜色样式类
const getKindClass = (kind?: number) => {
	if (kind === 10) return 'icon-container';
	if (kind === 20) return 'icon-layout';
	return 'icon-content';
};

// 3. 点击节点上的 "Append"
const handleAppend = (data: any) => {
	// if (!data.originalId && data.originalId !== 0) {
	//     ElMessage.warning('Please save the page before adding children to this new component.');
	//     return;
	// }
	console.log('Appending to component:', data);
	targetParentId.value = data.id;
	componentDrawerVisible.value = true;
};
</script>

<style scoped lang="scss">
/* 变量定义 */
$left-width: 280px;
$right-width: 300px;
$header-height: 50px;
$bg-color: #f5f7fa;
$border-color: #e4e7ed;
$primary-color: #409eff;

.builder-layout {
	display: flex;
	height: 100vh;
	width: 100%;
	background-color: $bg-color;
	overflow: hidden;
	font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
}

/* --- 左侧面板 --- */
.left-panel {
	width: $left-width;
	background: #fff;
	border-right: 1px solid $border-color;
	display: flex;
	flex-direction: column;
	flex-shrink: 0;
	z-index: 20;
	box-shadow: 2px 0 8px rgba(0, 0, 0, 0.02);

	.panel-header {
		padding: 16px;
		border-bottom: 1px solid $border-color;

		.header-actions {
			display: flex;
			justify-content: space-between;
			align-items: center;
			margin-bottom: 12px;

			.panel-title {
				font-size: 16px;
				font-weight: 700;
				color: #1a1a1a;
			}
		}
	}

	.page-tree-container {
		flex: 1;
		overflow: hidden;
		padding: 10px 0;

		.empty-placeholder {
			margin-top: 40px;
		}

		/* 树节点样式定制 */
		:deep(.el-tree-node__content) {
			height: 38px;
			border-radius: 4px;
			margin: 0 8px;

			&:hover {
				background-color: #f0f7ff;
			}
		}

		:deep(.el-tree-node.is-current > .el-tree-node__content) {
			background-color: #ecf5ff;
			color: $primary-color;
			font-weight: 500;
		}

		.custom-tree-node {
			display: flex;
			align-items: center;
			justify-content: space-between;
			width: 100%;
			padding-right: 8px;
			font-size: 14px;

			.node-main {
				display: flex;
				align-items: center;
				overflow: hidden;

				.node-icon {
					margin-right: 6px;
					font-size: 16px;
					color: #909399;
				}

				.node-label {
					white-space: nowrap;
					overflow: hidden;
					text-overflow: ellipsis;
				}
			}

			.status-dot {
				width: 6px;
				height: 6px;
				border-radius: 50%;
				background-color: #dcdfe6;
				flex-shrink: 0;

				&.online {
					background-color: #67c23a;
					box-shadow: 0 0 0 2px rgba(103, 194, 58, 0.2);
				}
			}

			&.is-active {
				.node-main .node-icon {
					color: $primary-color;
				}
			}
		}
	}
}

/* --- 中间面板 --- */
.center-panel {
	flex: 1;
	display: flex;
	flex-direction: column;
	position: relative;

	.preview-toolbar {
		height: $header-height;
		background: #fff;
		border-bottom: 1px solid $border-color;
		display: flex;
		align-items: center;
		justify-content: space-between;
		padding: 0 20px;
		box-shadow: 0 1px 4px rgba(0, 0, 0, 0.05);
		z-index: 10;

		.url-display {
			flex: 1;
			max-width: 500px;
			margin: 0 20px;

			:deep(.el-input__wrapper) {
				background-color: #f5f7fa;
				box-shadow: none;
			}
		}
	}

	.iframe-container {
		flex: 1;
		background-color: #f0f2f5;
		background-image: radial-gradient(#e0e0e0 1px, transparent 1px);
		background-size: 20px 20px;
		display: flex;
		justify-content: center;
		padding: 30px;
		overflow: auto;

		.iframe-wrapper {
			background: #fff;
			box-shadow: 0 4px 24px rgba(0, 0, 0, 0.1);
			transition: all 0.3s ease;

			&.desktop {
				width: 100%;
				height: 100%;
				max-width: 1440px;
				border: none;
			}

			&.mobile {
				width: 375px;
				height: 667px;
				border-radius: 16px;
				border: 8px solid #333;
			}

			iframe {
				width: 100%;
				height: 100%;
			}
		}
	}

	/* --- 右侧悬浮面板样式 --- */
	.right-panel {
		position: absolute;
		right: 20px;
		top: 70px;
		bottom: 20px;
		width: 300px;
		/* 保持宽度 */
		background: #fff;
		border-radius: 8px;
		box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
		display: flex;
		flex-direction: column;
		border: 1px solid #ebeef5;
		z-index: 100;
		overflow: hidden;
		/* 防止圆角溢出 */

		/* 顶部 Header */
		.panel-head {
			padding: 10px 15px;
			background: #fff;
			border-bottom: 1px solid #f0f0f0;
			display: flex;
			justify-content: space-between;
			align-items: center;
			height: 50px;
			flex-shrink: 0;

			.head-left {
				display: flex;
				align-items: center;
				font-weight: 600;
				color: #303133;
				font-size: 15px;

				.mr-1 {
					margin-right: 6px;
					color: #909399;
				}
			}

			.actions {
				display: flex;
				align-items: center;
				gap: 8px;
			}
		}

		/* 内容区域 */
		.panel-body {
			flex: 1;
			overflow: hidden;
			background-color: #fbfbfb;
			/* 给树形区域一个极淡的背景，区分Header */

			/* 树形结构微调 */
			.component-tree {
				background: transparent;
				padding: 10px;
			}

			/* 节点样式 */
			.comp-tree-node {
				display: flex;
				align-items: center;
				justify-content: space-between;
				width: 100%;
				font-size: 13px;

				.node-left {
					display: flex;
					align-items: center;
					overflow: hidden;

					.comp-icon {
						margin-right: 8px;
						color: #909399;
						font-size: 14px;
					}

					.comp-name {
						color: #606266;
					}

					.mini-tag {
						margin-left: 6px;
						transform: scale(0.85);
						transform-origin: left center;
					}
				}

				.node-actions {
					display: none;
					/* 默认隐藏删除按钮 */
					margin-right: 4px;
				}

				&:hover .node-actions {
					display: block;
					/* 悬停显示 */
				}
			}

			/* 空状态垂直居中 */
			.empty-state {
				height: 100%;
				display: flex;
				align-items: center;
				justify-content: center;
			}
		}

		/* 底部提示条 */
		.panel-footer {
			background: #fff8e1;
			color: #e6a23c;
			font-size: 12px;
			padding: 8px 15px;
			border-top: 1px solid #faecd8;

			.unsaved-bar {
				display: flex;
				align-items: center;
				gap: 6px;
			}
		}
	}

	/* --- 重新打开面板的悬浮按钮 --- */
	.right-panel-trigger {
		position: absolute;
		right: 0;
		top: 100px;
		width: 40px;
		height: 40px;
		background: #fff;
		box-shadow: -2px 2px 8px rgba(0, 0, 0, 0.1);
		border-radius: 8px 0 0 8px;
		display: flex;
		align-items: center;
		justify-content: center;
		cursor: pointer;
		z-index: 90;
		color: #409eff;
		transition: all 0.2s;
		border: 1px solid #ebeef5;
		border-right: none;

		&:hover {
			width: 45px;
			background: #ecf5ff;
		}
	}
}

/* 抽屉 & 弹窗样式 */
.w-100 {
	width: 100%;
}

.custom-tabs {
	:deep(.el-tabs__nav-wrap::after) {
		height: 1px;
	}
}

.tpl-select-box {
	margin-top: 10px;
	background: #f9f9f9;
	padding: 10px;
	border-radius: 4px;
}

/* --- 优化后的添加组件抽屉样式 --- */

.drawer-layout {
	display: flex;
	flex-direction: column;
	height: 100%;
	padding: 0 5px;
}

/* 顶部选择区 */
.target-section {
	margin-bottom: 10px;

	.section-label {
		display: block;
		font-size: 14px;
		font-weight: 600;
		color: #303133;
		margin-bottom: 8px;
	}

	.container-select {
		:deep(.el-input__wrapper) {
			box-shadow: 0 0 0 1px #dcdfe6 inset;

			&:hover {
				box-shadow: 0 0 0 1px #409eff inset;
			}
		}
	}

	.hint-text {
		font-size: 12px;
		color: #909399;
		margin-top: 6px;
	}
}

.custom-divider {
	margin: 15px 0;
	border-top: 1px solid #f0f0f0;
}

/* 树选择下拉项样式 */
.select-option-item {
	display: flex;
	align-items: center;
	font-size: 13px;

	.mr-1 {
		margin-right: 6px;
		color: #909399;
	}

	.id-tag {
		margin-left: auto;
		color: #c0c4cc;
		font-size: 11px;
	}
}

/* 组件列表 */
.component-scroll {
	flex: 1;
}

.component-grid {
	display: flex;
	flex-direction: column;
	gap: 12px;
	padding-bottom: 20px;
}

/* 组件卡片 */
.comp-card {
	display: flex;
	align-items: center;
	padding: 16px;
	background: #fff;
	border: 1px solid #ebeef5;
	border-radius: 12px;
	cursor: pointer;
	transition: all 0.25s cubic-bezier(0.25, 0.8, 0.25, 1);
	position: relative;
	overflow: hidden;

	/* 悬停效果 */
	&:hover {
		border-color: #b3d8ff;
		transform: translateY(-2px);
		box-shadow: 0 8px 16px rgba(64, 158, 255, 0.08);

		.card-action {
			opacity: 1;
			transform: translateX(0);
		}
	}

	/* 左侧图标 */
	.card-icon-wrapper {
		width: 48px;
		height: 48px;
		border-radius: 10px;
		display: flex;
		align-items: center;
		justify-content: center;
		margin-right: 16px;
		flex-shrink: 0;
		transition: transform 0.3s;
	}

	/* 中间内容 */
	.card-content {
		flex: 1;
		overflow: hidden;

		.card-header {
			display: flex;
			align-items: center;
			justify-content: space-between;
			margin-bottom: 4px;

			.comp-title {
				margin: 0;
				font-size: 15px;
				font-weight: 600;
				color: #1f2d3d;
			}

			.type-tag {
				height: 20px;
				padding: 0 6px;
				font-size: 10px;
				margin-left: 8px;
			}
		}

		.comp-desc {
			margin: 0;
			font-size: 12px;
			color: #909399;
			line-height: 1.4;
			white-space: nowrap;
			overflow: hidden;
			text-overflow: ellipsis;
		}
	}

	/* 右侧动作 */
	.card-action {
		width: 30px;
		display: flex;
		justify-content: center;
		opacity: 0.5;
		transition: all 0.2s;

		.el-button {
			font-size: 16px;
		}
	}
}

/* --- 右侧结构树样式优化 --- */

.structure-tree {
	background: transparent;
	padding: 6px;

	/* 调整 Element Plus 树节点的默认样式 */
	:deep(.el-tree-node__content) {
		height: 36px;
		/*稍微增高一点，方便操作 */
		border-radius: 4px;
		margin-bottom: 2px;

		&:hover {
			background-color: #f0f7ff;
		}
	}
}

.custom-tree-node {
	flex: 1;
	display: flex;
	align-items: center;
	justify-content: space-between;
	font-size: 13px;
	padding-right: 8px;
	overflow: hidden;
	/* 防止内容撑开 */

	/* 左侧内容区 */
	.node-content {
		display: flex;
		align-items: center;
		overflow: hidden;

		.node-icon {
			margin-right: 6px;
			font-size: 14px;

			&.icon-container {
				color: #409eff;
			}

			/* 蓝色文件夹 */
			&.icon-layout {
				color: #e6a23c;
			}

			/* 橙色布局 */
			&.icon-content {
				color: #67c23a;
			}

			/* 绿色内容 */
		}

		.node-label {
			white-space: nowrap;
			overflow: hidden;
			text-overflow: ellipsis;
			color: #606266;
		}

		.mini-tag {
			margin-left: 6px;
			height: 16px;
			line-height: 14px;
			padding: 0 4px;
			font-size: 10px;
		}
	}

	/* 右侧操作区 */
	.node-actions {
		display: none;
		/* 默认隐藏 */
		align-items: center;
		gap: 4px;
		background: #f0f7ff;
		/* 遮住下面的文字防止重叠，配合 hover */
		padding-left: 8px;

		/* 调整按钮样式 */
		.el-button {
			padding: 4px 0;
			height: auto;
			font-size: 12px;
		}
	}

	/* 悬停时显示操作按钮 */
	&:hover .node-actions {
		display: flex;
	}
}

/* 选中节点的样式 */
:deep(.el-tree-node.is-current > .el-tree-node__content) {
	background-color: #ecf5ff;

	.node-label {
		color: #409eff;
		font-weight: 500;
	}
}

/** create tab */
.form-tab-content {
	/* 1. Set a fixed height */
	height: 400px;
	/* Adjust based on your design preferences */

	/* 2. Enable scrolling for long content */
	overflow-y: auto;
	overflow-x: hidden;

	/* 3. Add some padding so content doesn't touch the scrollbar */
	padding-right: 10px;
	padding-bottom: 10px;
}
</style>
