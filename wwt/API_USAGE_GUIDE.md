# API封装使用指南

本指南展示了如何使用新的Nuxt 4 API封装系统。

## 📦 安装和配置

所有组件已创建完成，无需额外安装。只需确保环境变量配置正确：

### 环境变量配置
```bash
# .env
API_BASE_URL=https://your-api-domain.com/api
API_TIMEOUT=10000
API_DEBUG=false
API_SECRET=your-secret-key
```

## 🚀 基本使用

### 1. 在组件中使用useAPI

#### 首页数据获取（阻塞式）
```vue
<template>
  <div>
    <h1>文章列表</h1>
    <div v-if="pending">加载中...</div>
    <div v-else-if="error" class="error">
      加载失败: {{ error.message }}
    </div>
    <div v-else>
      <article v-for="post in posts" :key="post.id">
        <h2>{{ post.title }}</h2>
        <p>{{ post.excerpt }}</p>
      </article>
    </div>
  </div>
</template>

<script setup lang="ts">
// 获取首页文章列表
const { data: posts, pending, error } = await useAPI('/api/posts')

// 带查询参数
const { data: filteredPosts } = await useAPI('/api/posts', {
  query: { category: 'tech', page: 1 }
})
</script>
```

#### 用户详情页（懒加载）
```vue
<template>
  <div>
    <!-- 加载状态 -->
    <div v-if="status === 'pending'" class="loading">
      <LoadingSpinner />
      <p>正在加载用户信息...</p>
    </div>
    
    <!-- 错误状态 -->
    <div v-else-if="status === 'error'" class="error">
      <ErrorMessage :error="error" />
      <button @click="refresh">重试</button>
    </div>
    
    <!-- 成功状态 -->
    <div v-else-if="status === 'success'" class="user-profile">
      <img :src="user.avatar" :alt="user.name" />
      <h1>{{ user.name }}</h1>
      <p>{{ user.bio }}</p>
    </div>
  </div>
</template>

<script setup lang="ts">
const route = useRoute()

// 懒加载用户详情
const { data: user, status, error, refresh } = await useLazyAPI(`/api/users/${route.params.id}`, {
  // 懒加载选项
  immediate: true,
  
  // 默认值
  default: () => ({
    name: '加载中...',
    bio: '正在获取用户信息...'
  })
})

// 监听路由变化
watch(() => route.params.id, (newId) => {
  if (newId) {
    refresh()
  }
})
</script>
```

### 2. 在方法中使用useHttpAPI

#### 表单提交
```vue
<template>
  <form @submit.prevent="submitForm">
    <input v-model="form.name" placeholder="姓名" required />
    <input v-model="form.email" placeholder="邮箱" required />
    <textarea v-model="form.message" placeholder="留言"></textarea>
    <button type="submit" :disabled="isSubmitting">
      {{ isSubmitting ? '提交中...' : '提交' }}
    </button>
  </form>
  
  <div v-if="submitError" class="error">
    提交失败: {{ submitError }}
  </div>
</template>

<script setup lang="ts">
const { post } = useHttpAPI()

const form = ref({
  name: '',
  email: '',
  message: ''
})

const isSubmitting = ref(false)
const submitError = ref('')

async function submitForm() {
  isSubmitting.value = true
  submitError.value = ''
  
  try {
    const result = await post('/api/contact', form.value)
    console.log('提交成功:', result)
    
    // 重置表单
    form.value = { name: '', email: '', message: '' }
    
    // 显示成功提示
    alert('留言提交成功！')
  } catch (error) {
    submitError.value = error.message
    console.error('提交失败:', error)
  } finally {
    isSubmitting.value = false
  }
}
</script>
```

#### 文件上传
```vue
<template>
  <div>
    <input type="file" @change="handleFileChange" accept="image/*" />
    <button @click="uploadFile" :disabled="!file || isUploading">
      {{ isUploading ? '上传中...' : '上传' }}
    </button>
    
    <div v-if="uploadProgress > 0" class="progress">
      <div class="progress-bar" :style="{ width: uploadProgress + '%' }"></div>
      <span>{{ uploadProgress }}%</span>
    </div>
  </div>
</template>

<script setup lang="ts">
const { upload } = useHttpAPI()

const file = ref<File | null>(null)
const isUploading = ref(false)
const uploadProgress = ref(0)

function handleFileChange(event: Event) {
  const target = event.target as HTMLInputElement
  file.value = target.files?.[0] || null
}

async function uploadFile() {
  if (!file.value) return
  
  isUploading.value = true
  uploadProgress.value = 0
  
  try {
    const formData = new FormData()
    formData.append('file', file.value)
    formData.append('category', 'avatar')
    
    const result = await upload('/api/upload', formData)
    console.log('上传成功:', result)
    
    // 显示上传的图片
    file.value = null
  } catch (error) {
    console.error('上传失败:', error)
    alert(`上传失败: ${error.message}`)
  } finally {
    isUploading.value = false
    uploadProgress.value = 0
  }
}
</script>
```

#### 批量请求
```vue
<script setup lang="ts">
const { useBatchAPI } = useHttpAPI()

async function loadDashboardData() {
  try {
    const [userStats, recentPosts, notifications] = await useBatchAPI([
      { method: 'GET', url: '/api/user/stats' },
      { method: 'GET', url: '/api/posts/recent' },
      { method: 'GET', url: '/api/notifications' }
    ])
    
    console.log('所有数据加载完成:', {
      userStats,
      recentPosts,
      notifications
    })
  } catch (error) {
    console.error('批量加载失败:', error)
  }
}
</script>
```

### 3. 使用useAsyncAPI

#### 复杂数据处理
```vue
<script setup lang="ts">
const route = useRoute()

// 复杂数据获取和处理
const { data: dashboard, pending, refresh } = await useAsyncAPI(
  'dashboard',
  async () => {
    // 获取用户信息
    const user = await $fetch('/api/user/profile')
    
    // 获取统计数据
    const stats = await $fetch('/api/user/stats')
    
    // 获取最近活动
    const activities = await $fetch('/api/user/activities', {
      query: { limit: 10 }
    })
    
    // 复杂数据处理
    return {
      user,
      stats: {
        ...stats,
        completionRate: Math.round((stats.completed / stats.total) * 100)
      },
      activities: activities.map(activity => ({
        ...activity,
        timeAgo: formatTimeAgo(activity.createdAt)
      }))
    }
  },
  {
    // 转换数据
    transform: (data) => {
      console.log('原始数据:', data)
      return data
    },
    
    // 缓存策略
    getCachedData: (key, nuxtApp) => {
      // 检查是否有有效缓存
      const cached = nuxtApp.payload.data[key]
      if (cached && cached.user) {
        return cached
      }
      return undefined
    }
  }
)

// 监听数据变化
watch(dashboard, (newData) => {
  if (newData) {
    console.log('Dashboard数据更新:', newData)
  }
})
</script>
```

#### 带刷新机制的数据
```vue
<script setup lang="ts">
const { data: messages, forceRefresh, pending } = await useRefreshableAPI(
  'messages',
  '/api/messages'
)

// 定时刷新
const { pause, resume } = useIntervalFn(() => {
  forceRefresh()
}, 30000) // 每30秒刷新一次

// 页面可见性控制
onMounted(() => {
  document.addEventListener('visibilitychange', () => {
    if (document.hidden) {
      pause() // 页面隐藏时暂停刷新
    } else {
      resume() // 页面显示时恢复刷新
    }
  })
})
</script>
```

## 🔧 高级功能

### 1. 错误处理

#### 全局错误处理
```vue
<script setup lang="ts">
// 跳过全局错误处理
const { data, error } = await useAPI('/api/public-data', {
  skipErrorHandler: true
})

if (error.value) {
  // 自定义错误处理逻辑
  switch (error.value?.code) {
    case 404:
      navigateTo('/404')
      break
    case 403:
      navigateTo('/403')
      break
    default:
      showError({
        statusCode: error.value?.code || 500,
        statusMessage: error.value?.message || '服务器错误'
      })
  }
}
</script>
```

#### 重试机制
```vue
<script setup lang="ts">
const { useRetryAPI } = useHttpAPI()

async function fetchDataWithRetry() {
  try {
    const data = await useRetryAPI(
      async () => {
        return await get('/api/unstable-data')
      },
      3, // 最大重试3次
      1000 // 重试间隔1秒
    )
    console.log('数据获取成功:', data)
  } catch (error) {
    console.error('重试后仍然失败:', error)
  }
}
</script>
```

### 2. 认证相关

#### 跳过认证
```vue
<script setup lang="ts">
// 获取公开数据，不需要Token
const { data: publicData } = await useAPI('/api/public/info', {
  skipAuth: true
})

// 调用第三方API
const { get: externalGet } = useHttpAPI()

const externalData = await externalGet('https://external-api.com/data', undefined, {
  skipAuth: true,
  timeout: 5000
})
</script>
```

### 3. 服务端专用

#### 仅服务端执行
```vue
<script setup lang="ts">
// 仅在服务端获取数据，客户端不会重复请求
const { data: serverData } = await useServerAPI('/api/server-config')

// SEO数据获取
const { data: seoData } = await useServerAsyncAPI(
  'seo-data',
  async () => {
    const page = await $fetch('/api/page/seo')
    const meta = await $fetch('/api/page/meta')
    
    return {
      title: page.title,
      description: page.description,
      keywords: meta.keywords,
      openGraph: meta.openGraph
    }
  }
)

// 设置SEO
useSeoMeta({
  title: seoData.value?.title || '默认标题',
  description: seoData.value?.description,
  keywords: seoData.value?.keywords
})
</script>
```

## 📝 最佳实践

### 1. 数据获取策略
- **关键数据**: 使用`useAPI`（阻塞式）
- **非关键数据**: 使用`useLazyAPI`（非阻塞式）
- **复杂处理**: 使用`useAsyncAPI`（自定义handler）
- **服务端数据**: 使用`useServerAPI`（仅SSR）

### 2. 错误处理
- 统一使用`try-catch`包装
- 根据错误类型进行不同处理
- 提供用户友好的错误提示
- 实现重试机制

### 3. 性能优化
- 使用懒加载减少首屏阻塞
- 合理使用缓存机制
- 批量请求减少HTTP开销
- 适当的超时设置

### 4. 类型安全
- 始终为API响应定义TypeScript类型
- 使用泛型确保类型推导
- 为不同API端点定义专门的接口

## 🔄 从旧代码迁移

### 替换useHttp
```typescript
// ❌ 旧代码
const { get } = useHttp()
const data = await get('/api/user')

// ✅ 新代码
const { data } = await useAPI('/api/user')

// 或者
const { get } = useHttpAPI()
const data = await get('/api/user')
```

### 替换$fetch
```typescript
// ❌ 旧代码
const data = await $fetch('/api/posts')

// ✅ 新代码
const { data } = await useAsyncData('posts', () => $fetch('/api/posts'))
```

## 🎯 总结

新的API封装系统提供了：
- ✅ 统一的拦截器处理
- ✅ 自动Token管理
- ✅ 灵活的数据获取策略
- ✅ 完整的错误处理
- ✅ SSR优化
- ✅ 类型安全
- ✅ 开发者友好的API

根据不同的使用场景选择合适的方法，构建高效、可维护的Nuxt应用！