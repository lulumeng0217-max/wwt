import {useBaseApi} from '/@/api/base';

// cmspage接口服务
export const useCmsPageApi = () => {
	const baseApi = useBaseApi("cmsPage");
	return {
		// 分页查询cmspage
		page: baseApi.page,
		// 查看cmspage详细
		detail: baseApi.detail,
		// 新增cmspage
		add: baseApi.add,
		// 更新cmspage
		update: baseApi.update,
		// 删除cmspage
		delete: baseApi.delete,
		// 批量删除cmspage
		batchDelete: baseApi.batchDelete,
		// 导出cmspage数据
		exportData: baseApi.exportData,
		// 导入cmspage数据
		importData: baseApi.importData,
		// 下载cmspage数据导入模板
		downloadTemplate: baseApi.downloadTemplate,
	}
}

// cmspage实体
export interface CmsPage {
	// 主键Id
	id: number;
	// 创建者Id
	createUserId: number;
	// 创建者姓名
	createUserName: string;
	// 修改者Id
	updateUserId: number;
	// 修改者姓名
	updateUserName: string;
	// 创建时间
	createTime: string;
	// 更新时间
	updateTime: string;
	// 租户Id
	tenantId: number;
	// 软删除
	isDelete?: boolean;
	// 软删除时间
	deleteTime: string;
}