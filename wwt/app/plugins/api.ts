// app/plugins/api.ts
import { useStorage } from '@vueuse/core'

export default defineNuxtPlugin((nuxtApp) => {
  const config = useRuntimeConfig()
  // 使用类型断言，因为 @vueuse/core 的 useStorage 返回 Ref
  const token = useStorage('admin.net:access-token', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjEzMDAwMDAwMDAxMDEsIlRlbmFudElkIjoxMzAwMDAwMDAwMDAxLCJBY2NvdW50Ijoic3VwZXJBZG1pbi5ORVQiLCJSZWFsTmFtZSI6Iui2hee6p-euoeeQhuWRmCIsIkFjY291bnRUeXBlIjo5OTksIk9yZ0lkIjowLCJPcmdOYW1lIjpudWxsLCJPcmdUeXBlIjpudWxsLCJMYW5nQ29kZSI6ImVuLVVTIiwiaWF0IjoxNzY4OTg4MzA4LCJuYmYiOjE3Njg5ODgzMDgsImV4cCI6MTc2OTU5MzEwOCwiaXNzIjoiQWRtaW4uTkVUIiwiYXVkIjoiQWRtaW4uTkVUIn0.e98y_QBuMbf05Qe41Kiu-SXgjwKtropagQNm7lgwvQU') as any
  
  // 创建自定义$fetch实例
  // 在服务端（SSR）时，使用相对路径访问 Nuxt 的 server API
  // 在客户端时，使用配置的 baseURL（如果配置了外部 API）或相对路径
  let baseURL: string
  console.log(import.meta.dev) 
  // if (import.meta.dev) {
  //   // 服务端：使用相对路径，Nuxt 会自动处理为内部路由
    baseURL = '/api'
  // } else {
  //   // 客户端：如果配置了外部 API baseURL，使用它；否则使用相对路径
  //   const configuredBase = config.public.apiBase
  //   if (configuredBase && configuredBase.startsWith('http')) {
  //     // 外部 API（如 http://localhost:3000/api）
  //     baseURL = configuredBase
  //   } else {
  //     // 使用相对路径访问 Nuxt 的 server API
  //     baseURL = '/api'
  //   }
  // }
  
  const api = $fetch.create({
    baseURL,
    timeout: 10000,
    
    // 请求拦截器 - 统一处理请求头
    onRequest({ request, options }) {
      // 确保 headers 对象存在
      if (!options.headers) {
        options.headers = new Headers()
      } else if (!(options.headers instanceof Headers)) {
        const headers = new Headers()
        Object.entries(options.headers).forEach(([key, value]) => {
          headers.set(key, value as string)
        })
        options.headers = headers
      }
      
      // 统一添加通用请求头
      // 1. 自动注入Token（除非明确跳过）
      const skipAuth = (options as any).skipAuth
      if (token.value && !skipAuth) {
        options.headers.set('Authorization', `Bearer ${token.value}`)
      }
      
      // 2. Content-Type（文件上传时由浏览器自动设置，不覆盖）
      if (!options.headers.has('Content-Type') && !(options.body instanceof FormData)) {
        options.headers.set('Content-Type', 'application/json')
      }
      
      // 3. Accept 头
      if (!options.headers.has('Accept')) {
        options.headers.set('Accept', 'application/json, */*')
      }
      
      // 4. X-Requested-With（标识为 AJAX 请求）
      if (!options.headers.has('X-Requested-With')) {
        options.headers.set('X-Requested-With', 'XMLHttpRequest')
      }
      
      // 5. 请求ID（用于追踪，可选）
      if (import.meta.client && !options.headers.has('X-Request-ID')) {
        options.headers.set('X-Request-ID', `${Date.now()}-${Math.random().toString(36).substr(2, 9)}`)
      }
      
      // 开发环境日志
      if (import.meta.dev) {
        console.log(`🚀 API Request: ${options.method || 'GET'} ${request}`)
      }
    },
    
    // 响应拦截器 - 统一处理返回参数
    onResponse({ response }) {
      // 开发环境日志
      if (import.meta.dev) {
        console.log(`✅ API Response: ${response.status} ${response.statusText}`)
      }
      
      // 统一处理API响应格式
      const apiResponse = response._data
      
      // 如果是标准格式 {code, message, result/data}
      if (apiResponse && typeof apiResponse === 'object' && 'code' in apiResponse) {
        if (apiResponse.code === 200 || apiResponse.code === 0) {
          // 返回业务数据，支持 result 或 data 字段
          return apiResponse.result !== undefined ? apiResponse.result : apiResponse.data
        } else {
          // 业务错误
          const error = new Error(apiResponse.message || apiResponse.msg || '请求失败')
          error.name = 'BusinessError'
          ;(error as any).response = response
          ;(error as any).data = apiResponse
          ;(error as any).code = apiResponse.code
          throw error
        }
      }
      
      // 如果不是标准格式，直接返回原始数据
      return apiResponse
    },
    
    // 错误拦截器 - 统一处理错误
    onResponseError({ response, request, options }) {
      // 开发环境日志
      if (import.meta.dev) {
        console.log(`❌ API Error: ${response.status} ${response.statusText} - ${request}`)
      }
      
      let msg = '网络错误'
      let code = response?.status
      let errorData: any = null
      
      // 尝试从响应中提取错误信息
      if (response._data) {
        if (typeof response._data === 'string') {
          msg = response._data
        } else if (typeof response._data === 'object') {
          msg = response._data.message || response._data.msg || response._data.error || response.statusText
          errorData = response._data
        } else {
          msg = response.statusText
        }
      } else {
        msg = response.statusText || '网络错误'
      }
      
      const error = new Error(msg)
      error.name = 'HttpError'
      ;(error as any).response = response
      ;(error as any).code = code
      ;(error as any).data = errorData
      ;(error as any).request = request
      
      // 获取 skipErrorHandler 选项
      const skipErrorHandler = (options as any).skipErrorHandler
      
      // 特殊错误处理
      if (code === 401) {
        // Token过期或未授权，清除token并跳转登录
        token.value = ''
        if (import.meta.client && !skipErrorHandler) {
          nuxtApp.runWithContext(() => {
            navigateTo('/login')
          })
        }
      } else if (code === 403) {
        // 无权限
        error.name = 'ForbiddenError'
        if (import.meta.client && !skipErrorHandler) {
          // 可以显示无权限提示
          console.warn('无权限访问该资源')
        }
      } else if (code === 404) {
        // 资源不存在
        error.name = 'NotFoundError'
      } else if (code === 500) {
        // 服务器错误
        error.name = 'ServerError'
      } else if (code === 502 || code === 503 || code === 504) {
        // 网关错误
        error.name = 'GatewayError'
        msg = '服务器暂时不可用，请稍后重试'
      }
      
      // 如果设置了 skipErrorHandler，只抛出错误不进行额外处理
      if (skipErrorHandler) {
        throw error
      }
      
      // 全局错误处理（可以在这里添加错误通知等）
      // 例如：使用 toast 显示错误消息
      if (import.meta.client) {
        // 可以在这里调用全局错误通知
        // useToast().error(msg)
      }
      
      throw error
    },
  })
  
  // 暴露给全局使用
  return {
    provide: {
      api,
    },
  }
})