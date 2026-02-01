import {useBaseApi} from '/@/api/base';

// cmscontentdata接口服务
export const useCmsComponentDataApi = () => {
	const baseApi = useBaseApi("cmsComponentData");
	return {
		// 分页查询cmscontentdata
		page: baseApi.page,
		// 查看cmscontentdata详细
		detail: baseApi.detail,
		// 新增cmscontentdata
		add: baseApi.add,
		// 更新cmscontentdata
		update: baseApi.update,
		// 删除cmscontentdata
		delete: baseApi.delete,
		// 批量删除cmscontentdata
		batchDelete: baseApi.batchDelete,
		// 导出cmscontentdata数据
		exportData: baseApi.exportData,
		// 导入cmscontentdata数据
		importData: baseApi.importData,
		// 下载cmscontentdata数据导入模板
		downloadTemplate: baseApi.downloadTemplate,
	}
}

// cmscontentdata实体
export interface CmsComponentData {
	// 主键Id
	id: number;
	// 
	cmsComponentId?: number;
	// 
	title: string;
	// 
	subtitle: string;
	// 
	content: string;
	// 
	icon: string;
	// 
	props: string;
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
	// 
	linkUrl: string;
	// 
	imageUrl: string;
	// 
	bgColor: string;
}