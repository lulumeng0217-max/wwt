// app/composables/useAsyncAPI.ts
import type { AsyncDataOptions } from 'nuxt/app'
import type { ApiError } from './useAPI'

// 简化的选项接口，避免复杂的类型约束
export interface UseAsyncAPIOptions<T = any> {
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
 * 自定义useAsyncData封装 - 使用自定义$api实例
 * @param key 缓存键
 * @param handler 数据获取函数，会自动注入 $api 实例
 * @param options 配置选项
 * @returns AsyncData响应对象
 */
export function useAsyncAPI<T = any>(
  key: string,
  handler: (nuxtApp: any) => Promise<T>,
  options?: UseAsyncAPIOptions<T>
) {
  const asyncHandler = async (nuxtApp: any) => {
    // 确保 handler 可以访问 $api 实例（统一拦截器）
    // handler 接收的 nuxtApp 已经包含了 $api
    return await handler(nuxtApp)
  }
  
  // 构建选项对象，使用 any 避免复杂的类型约束
  const asyncOptions: any = {
    server: options?.server ?? true,
    lazy: options?.lazy ?? false,
    dedupe: options?.dedupe ?? 'cancel',
  }
  
  // 添加可选配置
  if (options?.default) asyncOptions.default = options.default
  if (options?.transform) asyncOptions.transform = options.transform
  if (options?.watch) asyncOptions.watch = options.watch
  if (options?.timeout) asyncOptions.timeout = options.timeout
  if (options?.getCachedData) asyncOptions.getCachedData = options.getCachedData
  if (options?.immediate !== undefined) asyncOptions.immediate = options.immediate
  if (options?.client !== undefined) asyncOptions.client = options.client
  
  return useAsyncData<T, ApiError>(key, asyncHandler, asyncOptions)
}

/**
 * 懒加载版本 - 不阻塞导航
 * @param key 缓存键
 * @param handler 数据获取函数
 * @param options 配置选项
 * @returns AsyncData响应对象
 */
export function useLazyAsyncAPI<T = any>(
  key: string,
  handler: (nuxtApp: any) => Promise<T>,
  options?: UseAsyncAPIOptions<T>
) {
  return useAsyncAPI<T>(key, handler, { ...options, lazy: true })
}

/**
 * 服务端专用版本 - 仅在服务端执行
 * @param key 缓存键
 * @param handler 数据获取函数
 * @param options 配置选项
 * @returns AsyncData响应对象
 */
export function useServerAsyncAPI<T = any>(
  key: string,
  handler: (nuxtApp: any) => Promise<T>,
  options?: UseAsyncAPIOptions<T>
) {
  return useAsyncAPI<T>(key, handler, { ...options, server: true, client: false })
}

/**
 * 客户端专用版本 - 仅在客户端执行
 * @param key 缓存键
 * @param handler 数据获取函数
 * @param options 配置选项
 * @returns AsyncData响应对象
 */
export function useClientAsyncAPI<T = any>(
  key: string,
  handler: (nuxtApp: any) => Promise<T>,
  options?: UseAsyncAPIOptions<T>
) {
  return useAsyncAPI<T>(key, handler, { ...options, server: false })
}

/**
 * 带缓存的API调用 - 适用于不经常变化的数据
 * 使用统一的 $api 实例，自动应用请求头、返回参数处理和错误拦截
 * @param key 缓存键
 * @param url API地址
 * @param options 配置选项
 * @returns AsyncData响应对象
 */
export function useCachedAPI<T = any>(
  key: string,
  url: string,
  options?: UseAsyncAPIOptions<T>
) {
  return useAsyncAPI<T>(
    key,
    async (nuxtApp) => {
      // 使用统一的 $api 实例，自动应用所有拦截器
      const api = nuxtApp.$api as typeof $fetch
      return await api<T>(url) as T
    },
    {
      ...options,
      // 缓存配置
      getCachedData: (key: string, nuxtApp: any) => {
        // 优先使用缓存数据
        return nuxtApp.payload.data[key] || nuxtApp.static.data[key]
      },
    }
  )
}

/**
 * 手动刷新缓存的API调用
 * @param key 缓存键
 * @param handler 数据获取函数
 * @param options 配置选项
 * @returns AsyncData响应对象 + refresh方法
 */
export function useRefreshableAPI<T = any>(
  key: string,
  handler: (nuxtApp: any) => Promise<T>,
  options?: UseAsyncAPIOptions<T>
) {
  const { data, error, pending, refresh, clear, status } = useAsyncAPI<T>(
    key,
    handler,
    options
  )
  
  // 强制刷新（忽略缓存）
  const forceRefresh = async () => {
    clear()
    await refresh()
  }
  
  return {
    data,
    error,
    pending,
    status,
    refresh,
    forceRefresh,
    clear
  }
}