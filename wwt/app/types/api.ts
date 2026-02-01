// types/api.ts
// 定义后端返回的完整结构
export interface ApiResponse<T = any> {
  code: number      // 状态码
  type: string      // 类型，如 "success", "error"
  message: string   // 消息提示
  result: T         // 真实的业务数据
  extras: any | null // 额外信息（如分页总条数、扩展字段）
  time: string      // 服务器时间
}

// 请求配置扩展
export interface RequestConfig {
  skipErrorHandler?: boolean // 是否跳过全局错误处理
  skipAuth?: boolean         // 是否跳过 Token 注入
}

// API错误类型
export interface ApiError {
  name: string
  message: string
  response?: any
  data?: any
  code?: number
}

// HTTP请求选项
export interface HttpOptions {
  method?: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH'
  params?: Record<string, any>
  body?: Record<string, any> | FormData
  headers?: Record<string, string> | Headers
  timeout?: number
  responseType?: 'json' | 'blob' | 'text'
  skipAuth?: boolean
  skipErrorHandler?: boolean
}

// 分页数据类型
export interface PaginatedResponse<T> {
  list: T[]
  total: number
  page: number
  pageSize: number
  hasMore: boolean
}

// 文件上传进度类型
export interface UploadProgress {
  loaded: number
  total: number
  percentage: number
}

// API响应状态
export type ApiStatus = 'idle' | 'pending' | 'success' | 'error'

// 批量请求类型
export interface BatchRequest {
  method: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH'
  url: string
  body?: Record<string, any>
  params?: Record<string, any>
}

// 常用API数据类型
export interface User {
  id: number
  name: string
  email: string
  avatar?: string
  role?: string
  createdAt: string
  updatedAt: string
}

export interface LoginResponse {
  token: string
  refreshToken: string
  user: User
  expiresIn: number
}

export interface RefreshTokenResponse {
  token: string
  expiresIn: number
}