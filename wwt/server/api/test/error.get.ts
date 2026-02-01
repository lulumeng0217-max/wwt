// Mock API: 测试错误处理
export default defineEventHandler(async (event) => {
  const query = getQuery(event)
  const errorType = query.type || '400'
  
  await new Promise(resolve => setTimeout(resolve, 300))
  
  const errorMap: Record<string, { code: number, message: string }> = {
    '400': { code: 400, message: '请求参数错误' },
    '401': { code: 401, message: '未授权，请先登录' },
    '403': { code: 403, message: '无权限访问' },
    '404': { code: 404, message: '资源不存在' },
    '500': { code: 500, message: '服务器内部错误' },
    'business': { code: 200, message: '业务错误测试' }
  }
  
  const error = errorMap[errorType as string] || errorMap['400']
  
  // 业务错误（code 200 但 message 表示错误）
  if (errorType === 'business') {
    return {
      code: 200,
      message: '业务错误：操作失败',
      result: null
    }
  }
  
  // HTTP 错误
  throw createError({
    statusCode: error.code,
    message: error.message
  })
})
