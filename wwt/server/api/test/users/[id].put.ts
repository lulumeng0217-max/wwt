// Mock API: 更新用户
export default defineEventHandler(async (event) => {
  const id = getRouterParam(event, 'id')
  const body = await readBody(event)
  
  await new Promise(resolve => setTimeout(resolve, 400))
  
  if (!id) {
    throw createError({
      statusCode: 400,
      message: '用户ID不能为空'
    })
  }
  
  const updatedUser = {
    id: parseInt(id),
    ...body,
    updatedAt: new Date().toISOString()
  }
  
  return {
    code: 200,
    message: '更新成功',
    result: updatedUser
  }
})
