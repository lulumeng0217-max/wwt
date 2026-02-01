// Mock API: 删除用户
export default defineEventHandler(async (event) => {
  const id = getRouterParam(event, 'id')
  
  await new Promise(resolve => setTimeout(resolve, 300))
  
  if (!id) {
    throw createError({
      statusCode: 400,
      message: '用户ID不能为空'
    })
  }
  
  return {
    code: 200,
    message: '删除成功',
    result: { id: parseInt(id) }
  }
})
