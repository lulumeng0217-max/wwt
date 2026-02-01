<template>
    <div class="builder-layout">

        <!-- ================= 左侧面板：资源与操作 ================= -->
        <!-- ================= 左侧面板：导航式页面列表 ================= -->
        <aside class="left-panel">
            <!-- 顶部固定区域：新建 + 搜索 -->
            <div class="panel-header">
                <div class="header-actions">
                    <span class="panel-title">Page List</span>
                    <el-button type="primary" size="small" icon="Plus" circle @click="openCreateDialog"
                        title="New Page"></el-button>
                </div>

                <!-- 搜索过滤框 -->
                <div class="search-box">
                    <el-input v-model="searchKeyword" placeholder="Search path or title..." prefix-icon="Search"
                        clearable size="small" />
                </div>
            </div>

            <!-- 列表区域 -->
            <div class="page-list-container">
                <el-scrollbar>
                    <ul class="nav-list">
                        <li v-for="page in filteredPages" :key="page.id" class="nav-item"
                            :class="{ active: currentPreviewId === page.id }" @click="handlePageSelect(page)">
                            <!-- 左侧图标 -->
                            <div class="item-icon">
                                <el-icon v-if="page.pagetype === 'template'">
                                    <files />
                                </el-icon>
                                <el-icon v-else>
                                    <document />
                                </el-icon>
                            </div>

                            <!-- 中间文字：路径 (标题) -->
                            <div class="item-content">
                                <div class="main-text">
                                    <span class="path">{{ page.requestPath }}</span>
                                    <span class="title" v-if="page.title">({{ page.title }})</span>
                                </div>
                            </div>

                            <!-- 右侧状态点 -->
                            <div class="item-status">
                                <el-tooltip :content="page.status === 1 ? '已启用' : '已禁用'" placement="right">
                                    <span class="status-dot" :class="{ 'online': page.status === 1 }"></span>
                                </el-tooltip>
                            </div>
                        </li>

                        <!-- 空状态 -->
                        <li v-if="filteredPages.length === 0" class="empty-list">
                            <el-empty description="No pages found" :image-size="60" />
                        </li>
                    </ul>
                </el-scrollbar>
            </div>
        </aside>


        <!-- ================= 中间面板：Iframe 预览区 ================= -->
        <main class="center-panel">
            <!-- 顶部工具栏 -->
            <div class="preview-toolbar">
                <div class="device-switch">
                    <el-radio-group v-model="deviceMode" size="small">
                        <el-radio-button label="desktop"><el-icon>
                                <Monitor />
                            </el-icon> PC</el-radio-button>
                        <el-radio-button label="mobile"><el-icon>
                                <Iphone />
                            </el-icon> mobile</el-radio-button>
                    </el-radio-group>
                </div>
                <div class="url-bar">
                    <el-input v-model="currentUrl" readonly size="small" prefix-icon="Link">
                        <template #prepend>Previewing:</template>
                    </el-input>
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
            <transition name="el-zoom-in-left">
                <div v-show="showRightPanel" class="right-float-panel">
                    <div class="panel-head">
                        <span>current page component</span>
                        <el-button link icon="Close" @click="showRightPanel = false"></el-button>
                    </div>
                    <div class="panel-body">
                        <el-tree :data="componentTreeData" node-key="id" default-expand-all
                            :props="{ label: 'label', children: 'children' }">
                            <template #default="{ node, data }">
                                <span class="custom-tree-node">
                                    <el-icon class="mr-1">
                                        <component :is="data.icon || 'Box'" />
                                    </el-icon>
                                    <span>{{ node.label }}</span>
                                    <el-tag v-if="data.tag" size="small" effect="plain" class="ml-2">{{ data.tag}}</el-tag>
                                </span>
                            </template>
                        </el-tree>
                    </div>
                </div>
            </transition>
        </main>

        <!-- ================= 弹窗：新建页面表单 ================= -->
        <el-dialog v-model="dialogVisible" title="create page" width="800px" :close-on-click-modal="false">
            <el-form ref="formRef" :model="formData" :rules="rules" label-width="100px" class="create-form">
                <el-row :gutter="20">
                    <!-- 基础信息列 -->
                    <el-col :span="12">
                        <el-divider content-position="left">基础信息</el-divider>
                        <el-form-item label="page name" prop="title">
                            <el-input v-model="formData.title" placeholder="please enter name" />
                        </el-form-item>
                        <el-form-item label="path" prop="requestPath">
                            <el-input v-model="formData.requestPath" placeholder="e.g: /about-us">
                                <template #prepend>/</template>
                            </el-input>
                        </el-form-item>
                        <el-form-item label="Title" prop="subTitle">
                            <el-input v-model="formData.subTitle" placeholder="SEO Title" />
                        </el-form-item>
                        <el-form-item label="state">
                            <el-select v-model="formData.status">
                                <el-option label="启用" :value="1" />
                                <el-option label="禁用" :value="0" />
                            </el-select>
                        </el-form-item>
                    </el-col>

                    <!-- 模板选择列 -->
                    <el-col :span="12">
                        <el-divider content-position="left">Init content</el-divider>
                        <el-form-item label="choose sourse" prop="sourceType">
                            <el-radio-group v-model="sourceType" class="source-select">
                                <div class="source-option" :class="{ active: sourceType === 'empty' }"
                                    @click="sourceType = 'empty'">
                                    <el-radio label="empty">empty page</el-radio>
                                </div>
                                <div class="source-option" :class="{ active: sourceType === 'template' }"
                                    @click="sourceType = 'template'">
                                    <el-radio label="template">copy layout</el-radio>
                                </div>
                            </el-radio-group>
                        </el-form-item>

                        <div v-if="sourceType === 'template'" class="tpl-select-area">
                            <el-select v-model="selectedTemplateId" placeholder="please select template" class="w-100">
                                <el-option v-for="t in templateList" :key="t.id" :label="t.title" :value="t.id" />
                            </el-select>
                            <div v-if="selectedTemplateId" class="preview-mini">
                                Selected: {{templateList.find(t => t.id === selectedTemplateId)?.title}}
                                <br>
                                <!-- <small class="text-gray">Incloud: {{templateList.find(t => t.id ===
                                    selectedTemplateId)?.components.join(', ')}}</small> -->
                            </div>
                        </div>
                    </el-col>
                </el-row>

                <!-- SEO 高级配置 (折叠面板) -->
                <el-divider content-position="left">SEO</el-divider>
                <el-collapse>
                    <el-collapse-item title="SEO Config" name="1">
                        <el-form-item label="keywords" prop="keywords">
                            <el-input v-model="formData.keywords" type="textarea" :rows="2"
                                placeholder="meta keywords" />
                        </el-form-item>
                        <el-form-item label="description" prop="description">
                            <el-input v-model="formData.description" type="textarea" :rows="2"
                                placeholder="meta description" />
                        </el-form-item>
                        <el-form-item label="Robots" prop="robots">
                            <el-input v-model="formData.robots" placeholder="index, follow" />
                        </el-form-item>
                    </el-collapse-item>

                    <el-collapse-item title="Open Graph" name="2">
                        <el-row :gutter="20">
                            <el-col :span="12">
                                <el-form-item label="OG Title" prop="ogTitle">
                                    <el-input v-model="formData.ogTitle" />
                                </el-form-item>
                                <el-form-item label="OG Type" prop="ogType">
                                    <el-input v-model="formData.ogType" placeholder="website / article" />
                                </el-form-item>
                            </el-col>
                            <el-col :span="12">
                                <el-form-item label="OG Image" prop="ogImage">
                                    <el-input v-model="formData.ogImage" placeholder="图片 URL" />
                                </el-form-item>
                            </el-col>
                        </el-row>
                    </el-collapse-item>

                    <el-collapse-item title="system config" name="3">
                        <el-form-item label="isdynamic">
                            <el-switch v-model="formData.isDynamic" active-text="是" inactive-text="否" />
                        </el-form-item>
                        <el-form-item label="Canonical">
                            <el-input v-model="formData.canonicalUrl" />
                        </el-form-item>
                    </el-collapse-item>
                </el-collapse>

            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="dialogVisible = false">取消</el-button>
                    <el-button type="primary" @click="submitCreate">
                        立即创建并预览
                    </el-button>
                </span>
            </template>
        </el-dialog>

    </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed,onMounted } from 'vue'
import { Monitor, Iphone, Plus, Close, Link, Grid, List, Picture, Box, Files } from '@element-plus/icons-vue'
import type { FormInstance, FormRules } from 'element-plus'
import { getAPI } from '/@/utils/axios-utils';
import { CmsComponentTypeApi,CmsComponentApi,CmsPageApi } from '/@/api-services';
import { CmsComponentDto,CmsPageOutput,AddCmsPageInput,UpdateCmsPageInput,AddCmsComponentTypeInput,CmsComponentTypeOutput } from '/@/api-services/models';


// ---------------- state page ----------------
const state = reactive({
    pageList: [] as CmsPageOutput[],
    componentsList: [] as Array<CmsComponentDto>,
})


// UI 状态
const deviceMode = ref<'desktop' | 'mobile'>('desktop')
const currentPreviewId = ref<number>(0)
const showRightPanel = ref(true)
const dialogVisible = ref(false)
const iframeSrc = ref('about:blank') // 初始为空，或者指向你的 Nuxt 首页
const currentUrl = ref('http://127.0.0.1:3000')
const url = ref('http://localhost:3000')
// 表单相关
const formRef = ref<FormInstance>()
const sourceType = ref<'empty' | 'template'>('empty')
const selectedTemplateId = ref<Number>(0)
// 2. 搜索关键词状态
const searchKeyword = ref('')
// 表单数据模型 (对应 DB)
const formData = reactive<AddCmsPageInput>({
    subTitle: '',
    requestPath: '',
    title: '',
    pagetype: 'page',
    status: 1,
    isDynamic: false,
    description: '',
    keywords: '',
    robots: 'index, follow',
    ogTitle: '',
    ogType: 'website',
    ogImage: ''
})

// 表单校验规则
const rules = reactive<FormRules>({
    name: [{ required: true, message: '请输入页面名称', trigger: 'blur' }],
    requestpath: [{ required: true, message: '请输入访问路径', trigger: 'blur' }],
    title: [{ required: true, message: '请输入SEO标题', trigger: 'blur' }]
})

// 右侧组件树数据 (Mock，实际应从 iframe postMessage 获取)
const componentTreeData = ref<any[]>([])

// ---------------- 逻辑方法 ----------------
onMounted(async () => {
   await fetchPageList()
})

// get template list from api
const templateList = computed(() => {
    return state.pageList.filter(p => p.pagetype === 'template');
});

//get page list from api
const fetchPageList = async () => {
    const res = await getAPI(CmsPageApi).apiCmsPagePagePost({page:1,pageSize:9999});
    state.pageList = res.data.result?.items || []
}
//get component list from api
const fetchComponentList = async (pageId:number) => {
    const res = await getAPI(CmsComponentApi).apiWWTCMSCmsComponentGetCmsComponentPageIdGet(pageId);
    return res.data.result;
}

// 打开弹窗
const openCreateDialog = () => {
    // 重置表单
    formData.subTitle = ''
    formData.requestPath = ''
    formData.title = ''
    sourceType.value = 'empty'
    selectedTemplateId.value = 0
    dialogVisible.value = true
}

// 提交创建
const submitCreate = async () => {
    if (!formRef.value) return
    await formRef.value.validate(async (valid) => {
        if (valid) {
            console.log('提交到后端的数据:', formData)
            console.log('选择的模板来源:', sourceType.value, selectedTemplateId.value)

            // 模拟创建成功后的操作
            dialogVisible.value = false
            const res= await getAPI(CmsPageApi).apiCmsPageAddPost(formData);
            // 1. 设置预览 URL (这里假设 Nuxt 能够通过参数预览)
            // 实际场景：你可能先调用 API 保存，拿到 ID 后 previewUrl = `/preview/${id}`
            const previewUrl = `http://localhost:3000${formData.requestPath}?preview=true`
            currentUrl.value = previewUrl
            iframeSrc.value = previewUrl

            // 2. 更新右侧组件树 (根据选择的模板模拟数据)
            updateComponentTree(res.data.result || 0)

            showRightPanel.value = true
        }
    })
}

// 仅点击左侧模板进行预览 (不创建)
const handlePageSelect = (tpl: CmsPageOutput) => {
    currentPreviewId.value = tpl.id ?? 0
    currentUrl.value = `${url.value}${tpl.requestPath}` // 假设的路由
    iframeSrc.value = currentUrl.value // 刷新 iframe
    updateComponentTree(tpl.id ?? 0)
    showRightPanel.value = true
}
// 3. 计算属性：过滤列表
const filteredPages = computed(() => {
    if (!searchKeyword.value) return state.pageList
    const kw = searchKeyword.value.toLowerCase()
    return state.pageList.filter(p =>
        p.requestPath?.toLowerCase().includes(kw) ||
        p.title?.toLowerCase().includes(kw) ||
        p.subTitle?.toLowerCase().includes(kw)
    )
})
// 模拟：根据模板ID更新右侧树
const updateComponentTree =async (tplId: Number) => {
   const components = await fetchComponentList(tplId as number);
    // const tpl = pageListMock.value.find(t => t.id === tplId)
    if (components && components.length > 0) {
        const children = components.map((c, index) => ({
            id: `c_${index}`,
            label: c.cmsComponentType?.name || 'Component',
            icon: 'Box'
        }))
       
    }
}

// Iframe 加载完成事件
const onIframeLoad = () => {
    console.log('Iframe loaded. Ready to communicate with Nuxt.')
    // 这里可以使用 postMessage 与 iframe 内部通信，获取真实的组件结构
    // const iframeWin = (document.querySelector('.preview-iframe') as HTMLIFrameElement).contentWindow
    // iframeWin?.postMessage({ type: 'GET_STRUCTURE' }, '*')
}

</script>

<style scoped lang="scss">
.builder-layout {
    display: flex;
    height: 100vh;
    width: 100%;
    overflow: hidden;
    background-color: #f5f7fa;

    /* --- 左侧 --- */
    .left-panel {
        width: 280px;
        background: #fff;
        border-right: 1px solid #e4e7ed;
        display: flex;
        flex-direction: column;
        flex-shrink: 0;

        .panel-header {
            padding: 20px;
            border-bottom: 1px solid #ebeef5;

            h3 {
                margin: 0 0 15px 0;
                font-size: 16px;
                color: #303133;
            }
        }

        .template-section {
            flex: 1;
            padding: 15px;
            display: flex;
            flex-direction: column;
            overflow: hidden;

            .section-title {
                font-size: 13px;
                color: #909399;
                margin-bottom: 10px;
            }

            .template-list {
                padding-right: 5px;
            }

            .tpl-item {
                display: flex;
                align-items: center;
                padding: 10px;
                margin-bottom: 10px;
                border: 1px solid #ebeef5;
                border-radius: 6px;
                cursor: pointer;
                transition: all 0.2s;

                &:hover {
                    border-color: #409EFF;
                }

                &.active {
                    background-color: #ecf5ff;
                    border-color: #409EFF;
                }

                .tpl-icon {
                    width: 36px;
                    height: 36px;
                    border-radius: 4px;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    margin-right: 10px;
                }

                .tpl-info {
                    .name {
                        font-size: 14px;
                        font-weight: 500;
                        color: #303133;
                    }

                    .desc {
                        font-size: 12px;
                        color: #909399;
                        margin-top: 2px;
                    }
                }
            }
        }
    }

    /* --- 中间 --- */
    .center-panel {
        flex: 1;
        display: flex;
        flex-direction: column;
        position: relative;
        background-image:
            linear-gradient(45deg, #f0f0f0 25%, transparent 25%, transparent 75%, #f0f0f0 75%, #f0f0f0),
            linear-gradient(45deg, #f0f0f0 25%, transparent 25%, transparent 75%, #f0f0f0 75%, #f0f0f0);
        background-size: 20px 20px;
        background-position: 0 0, 10px 10px;

        .preview-toolbar {
            height: 50px;
            background: #fff;
            border-bottom: 1px solid #dcdfe6;
            display: flex;
            align-items: center;
            padding: 0 20px;
            justify-content: space-between;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
            z-index: 10;

            .url-bar {
                width: 400px;
            }
        }

        .iframe-container {
            flex: 1;
            display: flex;
            justify-content: center;
            padding: 20px;
            overflow: auto;

            .iframe-wrapper {
                background: #fff;
                box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
                transition: width 0.3s;

                &.desktop {
                    width: 100%;
                    height: 100%;
                    border: none;
                }

                &.mobile {
                    width: 375px;
                    height: 667px;
                    /* iPhone 8 size */
                    margin-top: 20px;
                    border: 10px solid #333;
                    border-radius: 20px;
                    overflow: hidden;
                }

                .preview-iframe {
                    width: 100%;
                    height: 100%;
                    display: block;
                }
            }
        }

        /* --- 右侧浮动 --- */
        .right-float-panel {
            position: absolute;
            right: 20px;
            top: 70px;
            /* Toolbar height + margin */
            width: 260px;
            background: #fff;
            border-radius: 4px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            display: flex;
            flex-direction: column;
            max-height: calc(100vh - 100px);
            z-index: 100;

            .panel-head {
                padding: 10px 15px;
                background: #f5f7fa;
                border-bottom: 1px solid #ebeef5;
                font-weight: bold;
                display: flex;
                justify-content: space-between;
                align-items: center;
            }

            .panel-body {
                padding: 10px;
                overflow-y: auto;
            }
        }
    }
}

/* 弹窗样式调整 */
.create-form {
    padding: 0 10px;
}

.text-gray {
    color: #909399;
}

.w-100 {
    width: 100%;
}

.source-select {
    display: flex;
    flex-direction: column;
    width: 100%;
    gap: 10px;

    .source-option {
        border: 1px solid #dcdfe6;
        padding: 10px 15px;
        border-radius: 4px;
        cursor: pointer;

        &:hover {
            border-color: #409EFF;
        }

        &.active {
            border-color: #409EFF;
            background-color: #f0f9eb;
        }
    }
}

.tpl-select-area {
    margin-top: 10px;
    background: #f5f7fa;
    padding: 10px;
    border-radius: 4px;

    .preview-mini {
        margin-top: 8px;
        font-size: 12px;
        line-height: 1.4;
    }
}

.custom-tree-node {
    display: flex;
    align-items: center;
    font-size: 13px;

    .ml-2 {
        margin-left: 5px;
    }

    .mr-1 {
        margin-right: 5px;
    }
}

/* --- 左侧面板样式优化 --- */
.left-panel {
    width: 260px;
    /* 稍微窄一点，更像菜单 */
    background: #fff;
    border-right: 1px solid #e6e6e6;
    display: flex;
    flex-direction: column;
    flex-shrink: 0;
    user-select: none;

    /* 顶部 Header */
    .panel-header {
        padding: 16px;
        border-bottom: 1px solid #f0f0f0;
        background-color: #fff;

        .header-actions {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 12px;

            .panel-title {
                font-size: 16px;
                font-weight: 700;
                color: #1f2937;
            }
        }

        .search-box {

            /* 让搜索框更协调 */
            :deep(.el-input__wrapper) {
                background-color: #f3f4f6;
                box-shadow: none;

                &:hover,
                &.is-focus {
                    background-color: #fff;
                    box-shadow: 0 0 0 1px #409EFF inset;
                }
            }
        }
    }

    /* 列表容器 */
    .page-list-container {
        flex: 1;
        overflow: hidden;
        /* 交给 scrollbar */
        background-color: #fff;
    }

    /* 导航列表样式 UL */
    .nav-list {
        list-style: none;
        padding: 8px;
        margin: 0;
    }

    /* 单个列表项样式 LI */
    .nav-item {
        display: flex;
        align-items: center;
        padding: 10px 12px;
        margin-bottom: 2px;
        border-radius: 6px;
        cursor: pointer;
        transition: all 0.2s ease;
        border: 1px solid transparent;
        /* 预留边框位，防止抖动 */

        /* 悬停状态 */
        &:hover {
            background-color: #f3f4f6;
        }

        /* 选中激活状态 */
        &.active {
            background-color: #ecf5ff;
            color: #409EFF;
            border-color: rgba(64, 158, 255, 0.2);

            .item-content .main-text .path {
                color: #409EFF;
                font-weight: 600;
            }

            .item-content .sub-text {
                color: #a0cfff;
            }
        }

        /* 图标 */
        .item-icon {
            margin-right: 12px;
            display: flex;
            align-items: center;
            color: #909399;
            font-size: 16px;
        }

        &.active .item-icon {
            color: #409EFF;
        }

        /* 文字内容区 */
        .item-content {
            flex: 1;
            overflow: hidden;
            display: flex;
            flex-direction: column;
            justify-content: center;

            .main-text {
                display: flex;
                align-items: baseline;
                /* 文字底部对齐 */
                white-space: nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
                font-size: 14px;

                .path {
                    color: #374151;
                    margin-right: 6px;
                }

                .title {
                    font-size: 12px;
                    color: #9ca3af;
                }
            }

            .sub-text {
                font-size: 12px;
                color: #9ca3af;
                margin-top: 2px;
                white-space: nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
            }
        }

        /* 状态点 */
        .item-status {
            margin-left: 8px;
            display: flex;
            align-items: center;

            .status-dot {
                width: 6px;
                height: 6px;
                border-radius: 50%;
                background-color: #d1d5db;
                /* 默认灰色 */

                &.online {
                    background-color: #67C23A;
                    /* 绿色 */
                    box-shadow: 0 0 0 2px rgba(103, 194, 58, 0.2);
                }
            }
        }
    }

    .empty-list {
        padding-top: 40px;
    }
}
</style>