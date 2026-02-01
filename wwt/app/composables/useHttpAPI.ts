// app/composables/useHttpAPI.ts
import type { ApiError } from './useAPI'

export interface HttpAPIOptions {
  skipAuth?: boolean
  skipErrorHandler?: boolean
  timeout?: number
}

/**
 * 便捷HTTP方法封装 - 适用于方法内调用
 * @returns HTTP方法对象
 */
export const useHttpAPI = () => {
  const { $api } = useNuxtApp()
  
  // 类型断言：$api 是 $fetch 实例
  const api = $api as typeof $fetch
  
  return {
    /**
     * GET请求
     * @param url 请求地址
     * @param params 查询参数
     * @param options 配置选项
     * @returns Promise<T>
     */
    get: <T = any>(
      url: string, 
      params?: Record<string, any>, 
      options?: HttpAPIOptions
    ): Promise<T> => {
      return api<T>(url, { 
        method: 'GET', 
        params, 
        ...options 
      } as any) as Promise<T>
    },
    
    /**
     * POST请求
     * @param url 请求地址
     * @param body 请求体
     * @param options 配置选项
     * @returns Promise<T>
     */
    post: <T = any>(
      url: string, 
      body?: Record<string, any>, 
      options?: HttpAPIOptions
    ): Promise<T> => {
      return api<T>(url, { 
        method: 'POST', 
        body, 
        ...options 
      } as any) as Promise<T>
    },
    
    /**
     * PUT请求
     * @param url 请求地址
     * @param body 请求体
     * @param options 配置选项
     * @returns Promise<T>
     */
    put: <T = any>(
      url: string, 
      body?: Record<string, any>, 
      options?: HttpAPIOptions
    ): Promise<T> => {
      return api<T>(url, { 
        method: 'PUT', 
        body, 
        ...options 
      } as any) as Promise<T>
    },
    
    /**
     * DELETE请求
     * @param url 请求地址
     * @param body 请求体
     * @param options 配置选项
     * @returns Promise<T>
     */
    delete: <T = any>(
      url: string, 
      body?: Record<string, any>, 
      options?: HttpAPIOptions
    ): Promise<T> => {
      return api<T>(url, { 
        method: 'DELETE', 
        body, 
        ...options 
      } as any) as Promise<T>
    },
    
    /**
     * PATCH请求
     * @param url 请求地址
     * @param body 请求体
     * @param options 配置选项
     * @returns Promise<T>
     */
    patch: <T = any>(
      url: string, 
      body?: Record<string, any>, 
      options?: HttpAPIOptions
    ): Promise<T> => {
      return api<T>(url, { 
        method: 'PATCH', 
        body, 
        ...options 
      } as any) as Promise<T>
    },
    
    /**
     * 上传文件
     * @param url 请求地址
     * @param formData 表单数据
     * @param options 配置选项
     * @returns Promise<T>
     */
    upload: <T = any>(
      url: string, 
      formData: FormData, 
      options?: HttpAPIOptions
    ): Promise<T> => {
      return api<T>(url, { 
        method: 'POST', 
        body: formData,
        headers: {
          // 不要设置Content-Type，让浏览器自动设置multipart/form-data
        },
        ...options 
      } as any) as Promise<T>
    },
    
    /**
     * 下载文件
     * @param url 请求地址
     * @param params 查询参数
     * @param options 配置选项
     * @returns Promise<Blob>
     */
    download: (
      url: string, 
      params?: Record<string, any>, 
      options?: HttpAPIOptions
    ): Promise<Blob> => {
      return api<Blob>(url, { 
        method: 'GET', 
        params,
        responseType: 'blob',
        ...options 
      } as any)
    },
    
    /**
     * 原始实例（用于特殊场景）
     */
    instance: api,
  }
}

/**
 * 批量请求封装
 * @param requests 请求数组
 * @param options 配置选项
 * @returns Promise<T[]>
 */
export const useBatchAPI = async <T = any>(
  requests: Array<{
    method: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH'
    url: string
    body?: Record<string, any>
    params?: Record<string, any>
  }>,
  options?: HttpAPIOptions
): Promise<T[]> => {
  const { get, post, put, delete: del, patch } = useHttpAPI()
  
  const promises = requests.map(async (req) => {
    switch (req.method) {
      case 'GET':
        return await get<T>(req.url, req.params, options)
      case 'POST':
        return await post<T>(req.url, req.body, options)
      case 'PUT':
        return await put<T>(req.url, req.body, options)
      case 'DELETE':
        return await del<T>(req.url, req.body, options)
      case 'PATCH':
        return await patch<T>(req.url, req.body, options)
      default:
        throw new Error(`Unsupported method: ${req.method}`)
    }
  })
  
  return Promise.all(promises)
}

/**
 * 重试机制封装
 * @param fn 请求函数
 * @param maxRetries 最大重试次数
 * @param delay 重试延迟
 * @returns Promise<T>
 */
export const useRetryAPI = async <T = any>(
  fn: () => Promise<T>,
  maxRetries: number = 3,
  delay: number = 1000
): Promise<T> => {
  let lastError: any
  
  for (let i = 0; i <= maxRetries; i++) {
    try {
      return await fn()
    } catch (error) {
      lastError = error
      
      // 如果是最后一次重试，直接抛出错误
      if (i === maxRetries) {
        throw lastError
      }
      
      // 等待后重试
      await new Promise(resolve => setTimeout(resolve, delay))
    }
  }
  
  throw lastError
}