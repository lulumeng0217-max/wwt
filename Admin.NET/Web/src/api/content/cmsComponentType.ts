import {useBaseApi} from '/@/api/base';

// component_type接口服务
export const useCmsComponentTypeApi = () => {
	const baseApi = useBaseApi("cmsComponentType");
	return {
		// 分页查询component_type
		page: baseApi.page,
		// 查看component_type详细
		detail: baseApi.detail,
		// 新增component_type
		add: baseApi.add,
		// 更新component_type
		update: baseApi.update,
		// 删除component_type
		delete: baseApi.delete,
		// 批量删除component_type
		batchDelete: baseApi.batchDelete,
		// 导出component_type数据
		exportData: baseApi.exportData,
		// 导入component_type数据
		importData: baseApi.importData,
		// 下载component_type数据导入模板
		downloadTemplate: baseApi.downloadTemplate,
	}
}

// component_type实体
export interface CmsComponentType {
	// 主键Id
	id: number;
	// Name
	name?: string;
	// ComponentKind
	componentKind?: number;
	// Description
	description: string;
	// DefaultProps
	defaultProps: string;
	// SetStyles
	setStyles: string;
	// Fields
	fields: string;
	// Status
	status?: number;
	// 软删除
	isDelete?: boolean;
	// 软删除时间
	deleteTime: string;
	// 创建时间
	createTime: string;
	// 更新时间
	updateTime: string;
	// 创建者Id
	createUserId: number;
	// 创建者姓名
	createUserName: string;
	// 修改者Id
	updateUserId: number;
	// 修改者姓名
	updateUserName: string;
}