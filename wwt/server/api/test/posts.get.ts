// Mock API: 获取文章列表
export default defineEventHandler(async (event) => {
  const query = getQuery(event)
  const page = parseInt(query.page as string) || 1
  const limit = parseInt(query.limit as string) || 10
  
  await new Promise(resolve => setTimeout(resolve, 600))
  
  const posts = [
    { id: 1, title: 'Vue 3 新特性', content: 'Vue 3 带来了很多新特性...', author: '张三', createdAt: '2024-01-01' },
    { id: 2, title: 'Nuxt 4 入门指南', content: 'Nuxt 4 是下一代框架...', author: '李四', createdAt: '2024-01-02' },
    { id: 3, title: 'TypeScript 最佳实践', content: 'TypeScript 让代码更安全...', author: '王五', createdAt: '2024-01-03' },
  ]
  
  return {
    code: 200,
    message: 'success',
    result: {
      list: posts,
      total: posts.length,
      page,
      limit
    }
  }
})
