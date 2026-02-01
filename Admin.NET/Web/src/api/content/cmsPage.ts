import {useBaseApi} from '/@/api/base';

// page接口服务
export const useCmsPageApi = () => {
	const baseApi = useBaseApi("cmsPage");
	return {
		// 分页查询page
		page: baseApi.page,
		// 查看page详细
		detail: baseApi.detail,
		// 新增page
		add: baseApi.add,
		// 更新page
		update: baseApi.update,
		// 删除page
		delete: baseApi.delete,
		// 批量删除page
		batchDelete: baseApi.batchDelete,
		// 导出page数据
		exportData: baseApi.exportData,
		// 导入page数据
		importData: baseApi.importData,
		// 下载page数据导入模板
		downloadTemplate: baseApi.downloadTemplate,
	}
}

// page实体
export interface CmsPage {
	// 主键Id
	id: number;
	// Pagetype
	pagetype?: string;
	// TemplateId
	templateId?: number;
	// Title
	title: string;
	// SubeTitle
	subeTitle?: string;
	// Status
	status?: number;
	// RequestPath
	requestPath: string;
	// RealPath
	realPath: string;
	// IsDynamic
	isDynamic?: boolean;
	// Description
	description: string;
	// Keywords
	keywords: string;
	// CanonicalUrl
	canonicalUrl: string;
	// Robots
	robots: string;
	// OgTitle
	ogTitle: string;
	// OgImage
	ogImage: string;
	// OgType
	ogType: string;
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