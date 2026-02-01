<script lang="ts" name="cmsPage" setup>
import { ref, reactive, onMounted } from "vue";
import { ElMessage } from "element-plus";
import type { FormRules } from "element-plus";
import { formatDate } from '/@/utils/formatTime';
import { useCmsPageApi } from '/@/api/content/cmsPage';
import Editor from '/@/components/editor/index.vue';
//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const cmsPageApi = useCmsPageApi();
const ruleFormRef = ref();

const state = reactive({
	title: '',
	loading: false,
	showDialog: false,
	ruleForm: {} as any,
	stores: {},
	dropdownData: {} as any,
});

// 自行添加其他规则
const rules = ref<FormRules>({
  pagetype: [{required: true, message: '请选择Pagetype！', trigger: 'blur',},],
  templateId: [{required: true, message: '请选择TemplateId！', trigger: 'blur',},],
  subeTitle: [{required: true, message: '请选择SubeTitle！', trigger: 'blur',},],
  status: [{required: true, message: '请选择Status！', trigger: 'blur',},],
  isDynamic: [{required: true, message: '请选择IsDynamic！', trigger: 'blur',},],
});

// 页面加载时
onMounted(async () => {
});

// 打开弹窗
const openDialog = async (row: any, title: string) => {
	state.title = title;
	row = row ?? { title: "NULL::character varying",requestPath: "NULL::character varying",realPath: "NULL::character varying",description: "NULL::character varying",keywords: "NULL::character varying",canonicalUrl: "NULL::character varying",robots: "NULL::character varying",ogTitle: "NULL::character varying",ogImage: "NULL::character varying",ogType: "NULL::character varying",createUserName: "NULL::character varying",updateUserName: "NULL::character varying", };
	state.ruleForm = row.id ? await cmsPageApi.detail(row.id).then(res => res.data.result) : JSON.parse(JSON.stringify(row));
	state.showDialog = true;
};

// 关闭弹窗
const closeDialog = () => {
	emit("reloadTable");
	state.showDialog = false;
};

// 提交
const submit = async () => {
	ruleFormRef.value.validate(async (isValid: boolean, fields?: any) => {
		if (isValid) {
			let values = state.ruleForm;
			await cmsPageApi[state.ruleForm.id ? 'update' : 'add'](values);
			closeDialog();
		} else {
			ElMessage({
				message: `表单有${Object.keys(fields).length}处验证失败，请修改后再提交`,
				type: "error",
			});
		}
	});
};

//将属性或者函数暴露给父组件
defineExpose({ openDialog });
</script>
<template>
	<div class="cmsPage-container">
		<el-dialog v-model="state.showDialog" :width="800" draggable :close-on-click-modal="false">
			<template #header>
				<div style="color: #fff">
					<span>{{ state.title }}</span>
				</div>
			</template>
			<el-form :model="state.ruleForm" ref="ruleFormRef" label-width="auto" :rules="rules">
				<el-row :gutter="35">
					<el-form-item v-show="false">
						<el-input v-model="state.ruleForm.id" />
					</el-form-item>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="Pagetype" prop="pagetype">
							<el-input v-model="state.ruleForm.pagetype" placeholder="请输入Pagetype" maxlength="50" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="TemplateId" prop="templateId">
							<el-input v-model="state.ruleForm.templateId" placeholder="请输入TemplateId" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="Title" prop="title">
							<el-input v-model="state.ruleForm.title" placeholder="请输入Title" maxlength="255" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="SubeTitle" prop="subeTitle">
							<el-input v-model="state.ruleForm.subeTitle" placeholder="请输入SubeTitle" maxlength="255" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="Status" prop="status">
							<el-input-number v-model="state.ruleForm.status" placeholder="请输入Status" clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="RequestPath" prop="requestPath">
							<el-input v-model="state.ruleForm.requestPath" placeholder="请输入RequestPath" maxlength="255" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="RealPath" prop="realPath">
							<el-input v-model="state.ruleForm.realPath" placeholder="请输入RealPath" maxlength="255" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="IsDynamic" prop="isDynamic">
							<el-switch v-model="state.ruleForm.isDynamic" />
						</el-form-item>
					</el-col>
					<el-col :xs="24" class="mb20" >
						<el-form-item label="Description" prop="description">
							<Editor v-model:get-html="state.ruleForm.description" /> 
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="Keywords" prop="keywords">
							<el-input v-model="state.ruleForm.keywords" placeholder="请输入Keywords" maxlength="255" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="CanonicalUrl" prop="canonicalUrl">
							<el-input v-model="state.ruleForm.canonicalUrl" placeholder="请输入CanonicalUrl" maxlength="255" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="Robots" prop="robots">
							<el-input v-model="state.ruleForm.robots" placeholder="请输入Robots" maxlength="50" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="OgTitle" prop="ogTitle">
							<el-input v-model="state.ruleForm.ogTitle" placeholder="请输入OgTitle" maxlength="255" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="OgImage" prop="ogImage">
							<el-input v-model="state.ruleForm.ogImage" placeholder="请输入OgImage" maxlength="255" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="OgType" prop="ogType">
							<el-input v-model="state.ruleForm.ogType" placeholder="请输入OgType" maxlength="50" show-word-limit clearable />
						</el-form-item>
					</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" >
						<el-form-item label="DeleteTime" prop="deleteTime">
							<el-date-picker v-model="state.ruleForm.deleteTime" type="date" placeholder="DeleteTime" />
						</el-form-item>
					</el-col>
				</el-row>
			</el-form>
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="() => state.showDialog = false">取 消</el-button>
					<el-button @click="submit" type="primary" v-reclick="1000">确 定</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>
<style lang="scss" scoped>
:deep(.el-select), :deep(.el-input-number) {
  width: 100%;
}
</style>