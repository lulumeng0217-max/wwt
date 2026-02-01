// Mock API: 获取用户列表
export default defineEventHandler(async (event) => {
  // 模拟延迟
  await new Promise(resolve => setTimeout(resolve, 500))
  
  // 返回标准格式
  return {
    code: 200,
    message: 'success',
    result: [
      { id: 1, name: '张三', email: 'zhangsan@example.com', role: 'admin' },
      { id: 2, name: '李四', email: 'lisi@example.com', role: 'user' },
      { id: 3, name: '王五', email: 'wangwu@example.com', role: 'user' },
    ]
  }
})
