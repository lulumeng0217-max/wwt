<script lang="ts" name="cmsComponentType" setup>
import { ref, reactive, onMounted } from "vue";
import { ElMessage } from "element-plus";
import type { FormRules } from "element-plus";
import { formatDate } from '/@/utils/formatTime';
import { useCmsComponentTypeApi } from '/@/api/content/cmsComponentType';
import Editor from '/@/components/editor/index.vue';
import JsonEditorVue from 'json-editor-vue'
//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const cmsComponentTypeApi = useCmsComponentTypeApi();
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
	name: [{ required: true, message: '请选择Name！', trigger: 'blur', },],
	componentKind: [{ required: true, message: '请选择ComponentKind！', trigger: 'change', },],
	status: [{ required: true, message: '请选择Status！', trigger: 'blur', },],
});

// 页面加载时
onMounted(async () => {
});

// 打开弹窗
const openDialog = async (row: any, title: string) => {
	state.title = title;
	console.log('row',row);
	ruleFormRef.value?.resetFields();
	// state.ruleForm.DefaultProps={};
	// state.ruleForm.SetStyles={};
	// state.ruleForm.Fields={};
	state.ruleForm = {};
	// state.ruleForm = JSON.parse(JSON.stringify(row));
	if (row!=null&&row!=undefined&&row.id) {
		console.log('row.id',row.id);
		const result= await cmsComponentTypeApi.detail(row.id).then(res => res.data.result);
		result.DefaultProps = result.defaultProps? JSON.parse(result.defaultProps):{};
		result.SetStyles = result.setStyles? JSON.parse(result.setStyles):{};
		result.Fields =result.fields? JSON.parse(result.fields):{};
		state.ruleForm = row.id ? result: JSON.parse(JSON.stringify(row));
	}
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
			await cmsComponentTypeApi[state.ruleForm.id ? 'update' : 'add'](values);
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
	<div class="cmsComponentType-container">
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
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="Name" prop="name">
							<el-input v-model="state.ruleForm.name" placeholder="请输入Name" maxlength="255"
								show-word-limit clearable />
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="ComponentKind" prop="componentKind">
							<g-sys-dict v-model="state.ruleForm.componentKind" code="ComponentKindEnum"
								render-as="select" placeholder="请选择ComponentKind" clearable filterable />
						</el-form-item>
					</el-col>

					<el-col :xs="24" class="mb20">
						<el-form-item label="DefaultProps" prop="defaultProps">
							<JsonEditorVue v-model="state.ruleForm.DefaultProps" class="editor-box" mode="text"
								:mainMenuBar="false" :navigationBar="false" :statusBar="true"  style="width: 100%;"/>
						</el-form-item>
					</el-col>
					<el-col :xs="24" class="mb20">
						<el-form-item label="SetStyles" prop="setStyles">
							<JsonEditorVue v-model="state.ruleForm.SetStyles" class="editor-box" mode="text"
								:mainMenuBar="false" :navigationBar="false" :statusBar="true" style="width: 100%;"/>
						</el-form-item>
					</el-col>
					<el-col :xs="24" class="mb20">
						<el-form-item label="Fields" prop="fields">
							<JsonEditorVue v-model="state.ruleForm.Fields" class="editor-box" mode="text" :mainMenuBar="false"
								:navigationBar="false" :statusBar="true" style="width: 100%;" />
						</el-form-item>
					</el-col>
					<el-col :xs="24"  class="mb20">
						<el-form-item label="Status" prop="status">
							<el-input v-model="state.ruleForm.status" placeholder="Status" show-word-limit clearable />
						</el-form-item>
					</el-col>
					<el-col :xs="24" class="mb20">
						<el-form-item label="Description" prop="description">
							<Editor v-model:get-html="state.ruleForm.description" />
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
:deep(.el-select),
:deep(.el-input-number) {
	width: 100%;
}
</style>