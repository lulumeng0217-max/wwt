// Mock API: 获取单个用户
export default defineEventHandler(async (event) => {
  const id = getRouterParam(event, 'id')
  
  await new Promise(resolve => setTimeout(resolve, 300))
  
  const users: Record<string, any> = {
    '1': { id: 1, name: '张三', email: 'zhangsan@example.com', role: 'admin', bio: '系统管理员' },
    '2': { id: 2, name: '李四', email: 'lisi@example.com', role: 'user', bio: '普通用户' },
    '3': { id: 3, name: '王五', email: 'wangwu@example.com', role: 'user', bio: '普通用户' },
  }
  
  const user = users[id || '']
  
  if (!user) {
    throw createError({
      statusCode: 404,
      message: '用户不存在'
    })
  }
  
  return {
    code: 200,
    message: 'success',
    result: user
  }
})
