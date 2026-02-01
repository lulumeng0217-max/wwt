	// composables/useHttp.ts
	import { ofetch } from 'ofetch'
	import type { ApiResponse, RequestConfig } from '~/types/api'
    import { useStorage } from '@vueuse/core'
    export const accessTokenKey = 'access-token';
	export const useHttp = () => {
	  const config = useRuntimeConfig()
	  const router = useRouter()
	  // 假设 Token 存在 cookie 中，key 为 'token'
	  const token = useStorage('admin.net:access-token',"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEzMDAwMDAwMDAxMDEsIlRlbmFudElkIjoxMzAwMDAwMDAwMDAxLCJBY2NvdW50Ijoic3VwZXJBZG1pbi5ORVQiLCJSZWFsTmFtZSI6Iui2hee6p-euoeeQhuWRmCIsIkFjY291bnRUeXBlIjo5OTksIk9yZ0lkIjowLCJPcmdOYW1lIjpudWxsLCJPcmdUeXBlIjpudWxsLCJMYW5nQ29kZSI6ImVuLVVTIiwiaWF0IjoxNzY4OTg4MzA4LCJuYmYiOjE3Njg5ODgzMDgsImV4cCI6MTc2OTU5MzEwOCwiaXNzIjoiQWRtaW4uTkVUIiwiYXVkIjoiQWRtaW4uTkVUIn0.e98y_QBuMbf05Qe41Kiu-SXgjwKtropagQNm7lgwvQU") 
	  const httpIns = ofetch.create({
	    baseURL: config.public.apiBase || 'http://localhost:3000/api',
	    timeout: 10000,
		onRequest({ options }:any) {
		  // 1. 请求拦截：自动注入 Token
		  if (!(options).skipAuth && token.value) {
			options.headers = {
			  ...options.headers,
			  Authorization: `Bearer ${token.value}`,
			}
		  }
		},
	    onResponse({ response }) {
	      // 1. 响应拦截：统一解包
	      const res = response._data as ApiResponse
	      // 假设 code 200 表示业务成功
	      if (res.code === 200) {
	        // 策略：默认只返回 result (业务数据)，简化组件调用
	        // 如果你需要 extras (分页信息等)，可以在返回对象中拼接
	        // 例如: return { list: res.result, total: res.extras.total }
	        return res.result
	      }
	      // 业务逻辑错误（如 code 401, 400 等）
	      const error = new Error(res.message || '请求失败')
	      error.name = 'BusinessError'
	      ;(error as any).response = response
	      ;(error as any).data = res // 将完整响应挂载到 error 上，方便调试
	      throw error
	    },
	    onResponseError({ response }) {
	      // HTTP 错误拦截：处理 500, 404, 网络断开等
	      // 假设后端在 HTTP 错误时也会返回这个 JSON 结构，
	      // 或者直接返回 HTML/text。这里做一个兼容判断。
	      let msg = '网络错误'
	      let code = response?.status
	      if (response._data) {
	         // 如果错误信息里也有 message 字段，优先使用
	         msg = response._data.message || response.statusText
	      }
	      const error = new Error(msg)
	      error.name = 'HttpError'
	      ;(error as any).response = response
	      throw error
	    },
	  })
	  return {
		get: <T = any>(url: string, params?: Record<string, any>, opts?: RequestConfig) => {
		  return httpIns(url, { method: 'GET', params, ...opts }) as Promise<T>
		},
		post: <T = any>(url: string, body?: Record<string, any>, opts?: RequestConfig) => {
		  return httpIns(url, { method: 'POST', body, ...opts }) as Promise<T>
		},
		put: <T = any>(url: string, body?: Record<string, any>, opts?: RequestConfig) => {
		  return httpIns(url, { method: 'PUT', body, ...opts }) as Promise<T>
		},
		delete: <T = any>(url: string, body?: Record<string, any>, opts?: RequestConfig) => {
		  return httpIns(url, { method: 'DELETE', body, ...opts }) as Promise<T>
		},
		instance: httpIns
	  }
	}