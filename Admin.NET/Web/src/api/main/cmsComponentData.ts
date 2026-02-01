import {useBaseApi} from '/@/api/base';

// cmsconpentdata接口服务
export const useCmsComponentDataApi = () => {
	const baseApi = useBaseApi("cmsComponentData");
	return {
		// 分页查询cmsconpentdata
		page: baseApi.page,
		// 查看cmsconpentdata详细
		detail: baseApi.detail,
		// 新增cmsconpentdata
		add: baseApi.add,
		// 更新cmsconpentdata
		update: baseApi.update,
		// 删除cmsconpentdata
		delete: baseApi.delete,
		// 批量删除cmsconpentdata
		batchDelete: baseApi.batchDelete,
	}
}

// cmsconpentdata实体
export interface CmsComponentData {
	// 
	id: number;
}