// Mock API: 创建用户
export default defineEventHandler(async (event) => {
  const body = await readBody(event)
  
  await new Promise(resolve => setTimeout(resolve, 400))
  
  // 模拟验证
  if (!body.name || !body.email) {
    throw createError({
      statusCode: 400,
      message: '姓名和邮箱是必填项'
    })
  }
  
  const newUser = {
    id: Date.now(),
    name: body.name,
    email: body.email,
    role: body.role || 'user',
    bio: body.bio || '',
    createdAt: new Date().toISOString()
  }
  
  return {
    code: 200,
    message: '创建成功',
    result: newUser
  }
})
