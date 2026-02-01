import {useBaseApi} from '/@/api/base';

// component接口服务
export const useCmsComponentApi = () => {
	const baseApi = useBaseApi("cmsComponent");
	return {
		// 分页查询component
		page: baseApi.page,
		// 查看component详细
		detail: baseApi.detail,
		// 新增component
		add: baseApi.add,
		// 更新component
		update: baseApi.update,
		// 删除component
		delete: baseApi.delete,
		// 批量删除component
		batchDelete: baseApi.batchDelete,
		// 导出component数据
		exportData: baseApi.exportData,
		// 导入component数据
		importData: baseApi.importData,
		// 下载component数据导入模板
		downloadTemplate: baseApi.downloadTemplate,
	}
}

// component实体
export interface CmsComponent {
	// 主键Id
	id: number;
	// PageId
	pageId?: number;
	// ComponentTypeId
	componentTypeId?: number;
	// Pid
	pid?: number;
	// Props
	props?: string;
	// Styles
	styles: string;
	// SortOrder
	sortOrder?: number;
	// IsVisible
	isVisible?: boolean;
	// TenantId
	tenantId: number;
	// IsDelete
	isDelete?: boolean;
	// DeleteTime
	deleteTime: string;
	// CreateTime
	createTime: string;
	// UpdateTime
	updateTime: string;
	// CreateUserId
	createUserId: number;
	// CreateUserName
	createUserName: string;
	// UpdateUserId
	updateUserId: number;
	// UpdateUserName
	updateUserName: string;
}