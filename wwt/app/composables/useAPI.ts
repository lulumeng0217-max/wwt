// app/composables/useAPI.ts
import type { UseFetchOptions } from 'nuxt/app'

// 扩展错误类型
export interface ApiError {
  name: string
  message: string
  response?: any
  data?: any
  code?: number
}

// 简化的选项接口，避免复杂的类型约束
export interface UseAPIOptions<T = any> {
  skipAuth?: boolean
  skipErrorHandler?: boolean
  server?: boolean
  client?: boolean
  lazy?: boolean
  immediate?: boolean
  default?: () => T | Ref<T>
  transform?: (input: any) => T
  watch?: any
  dedupe?: 'cancel' | 'defer'
  timeout?: number
  getCachedData?: (key: string, nuxtApp: any, context: any) => T | undefined
  [key: string]: any
}

/**
 * 自定义useFetch封装 - 使用自定义$api实例
 * @param url API地址
 * @param options 配置选项
 * @returns useFetch响应对象
 */
export function useAPI<T = any>(
  url: string | (() => string),
  options?: UseAPIOptions<T>
) {
  const { $api } = useNuxtApp()
  
  // 构建选项对象，使用 any 避免复杂的类型约束
  const fetchOptions: any = {
    // 基本配置
    $fetch: $api as any,
    server: options?.server ?? true,
    lazy: options?.lazy ?? false,
  }
  
  // 传递 skipAuth 选项到 $fetch（用于统一拦截器）
  if (options?.skipAuth !== undefined) {
    fetchOptions.skipAuth = options.skipAuth
  }
  
  // 传递 skipErrorHandler 选项到 $fetch（用于统一拦截器）
  if (options?.skipErrorHandler !== undefined) {
    fetchOptions.skipErrorHandler = options.skipErrorHandler
  }
  
  // 添加可选配置
  if (options?.default) fetchOptions.default = options.default
  if (options?.transform) fetchOptions.transform = options.transform
  if (options?.watch) fetchOptions.watch = options.watch
  if (options?.dedupe) fetchOptions.dedupe = options.dedupe
  if (options?.timeout) fetchOptions.timeout = options.timeout
  if (options?.getCachedData) fetchOptions.getCachedData = options.getCachedData
  if (options?.immediate !== undefined) fetchOptions.immediate = options.immediate
  if (options?.client !== undefined) fetchOptions.client = options.client
  
  // 注意：错误处理已经在 api.ts 插件的统一拦截器中处理
  // 如果需要自定义错误处理，可以通过 skipErrorHandler 跳过统一处理
  
  return useFetch<T, ApiError>(url, fetchOptions)
}

/**
 * 服务端专用版本 - 仅在服务端执行
 * @param url API地址
 * @param options 配置选项
 * @returns useFetch响应对象
 */
export function useServerAPI<T>(
  url: string | (() => string),
  options?: UseAPIOptions<T>
) {
  return useAPI<T>(url, { ...options, server: true, client: false })
}

/**
 * 懒加载版本 - 不阻塞导航
 * @param url API地址
 * @param options 配置选项
 * @returns useFetch响应对象
 */
export function useLazyAPI<T = any>(
  url: string | (() => string),
  options?: UseAPIOptions<T>
) {
  return useAPI<T>(url, { ...options, lazy: true })
}