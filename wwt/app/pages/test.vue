<template>
  <div class="container mx-auto px-4 py-8 max-w-7xl">
    <h1 class="text-3xl font-bold mb-8 text-gray-900">API 测试页面</h1>
    
    <!-- useAPI 测试区域 -->
    <section class="mb-12 bg-white rounded-lg shadow-md p-6">
      <h2 class="text-2xl font-semibold mb-4 text-gray-800 border-b pb-2">
        1. useAPI 测试（useFetch 封装）
      </h2>
      
      <!-- GET 请求 -->
      <div class="mb-6">
        <h3 class="text-lg font-medium mb-3 text-gray-700">GET 请求 - 用户列表</h3>
        <div class="flex gap-2 mb-3">
          <button 
            @click="loadUsers" 
            :disabled="usersPending"
            class="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 disabled:opacity-50"
          >
            {{ usersPending ? '加载中...' : '获取用户列表' }}
          </button>
          <button 
            @click="refreshUsers" 
            class="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600"
          >
            刷新
          </button>
        </div>
        <div v-if="usersError" class="p-3 bg-red-50 border border-red-200 rounded text-red-700 mb-3">
          错误: {{ usersError.message }}
        </div>
        <div v-if="usersData" class="p-4 bg-gray-50 rounded">
          <pre class="text-sm overflow-auto">{{ JSON.stringify(usersData, null, 2) }}</pre>
        </div>
      </div>
      
      <!-- 懒加载测试 -->
      <div class="mb-6">
        <h3 class="text-lg font-medium mb-3 text-gray-700">懒加载 - 文章列表</h3>
        <div class="flex gap-2 mb-3">
          <button 
            @click="loadPosts" 
            :disabled="postsPending"
            class="px-4 py-2 bg-purple-500 text-white rounded hover:bg-purple-600 disabled:opacity-50"
          >
            {{ postsPending ? '加载中...' : '获取文章列表' }}
          </button>
        </div>
        <div v-if="postsError" class="p-3 bg-red-50 border border-red-200 rounded text-red-700 mb-3">
          错误: {{ postsError.message }}
        </div>
        <div v-if="postsData" class="p-4 bg-gray-50 rounded">
          <pre class="text-sm overflow-auto">{{ JSON.stringify(postsData, null, 2) }}</pre>
        </div>
      </div>
    </section>
    
    <!-- useAsyncAPI 测试区域 -->
    <section class="mb-12 bg-white rounded-lg shadow-md p-6">
      <h2 class="text-2xl font-semibold mb-4 text-gray-800 border-b pb-2">
        2. useAsyncAPI 测试（useAsyncData 封装）
      </h2>
      
      <div class="mb-6">
        <h3 class="text-lg font-medium mb-3 text-gray-700">异步数据获取</h3>
        <div class="flex gap-2 mb-3">
          <button 
            @click="loadUserDetail" 
            :disabled="userDetailPending"
            class="px-4 py-2 bg-indigo-500 text-white rounded hover:bg-indigo-600 disabled:opacity-50"
          >
            {{ userDetailPending ? '加载中...' : '获取用户详情 (ID: 1)' }}
          </button>
          <button 
            @click="refreshUserDetail" 
            class="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600"
          >
            刷新
          </button>
        </div>
        <div v-if="userDetailError" class="p-3 bg-red-50 border border-red-200 rounded text-red-700 mb-3">
          错误: {{ userDetailError.message }}
        </div>
        <div v-if="userDetailData" class="p-4 bg-gray-50 rounded">
          <pre class="text-sm overflow-auto">{{ JSON.stringify(userDetailData, null, 2) }}</pre>
        </div>
      </div>
    </section>
    
    <!-- useHttpAPI 测试区域 -->
    <section class="mb-12 bg-white rounded-lg shadow-md p-6">
      <h2 class="text-2xl font-semibold mb-4 text-gray-800 border-b pb-2">
        3. useHttpAPI 测试（HTTP 方法封装）
      </h2>
      
      <!-- GET 请求 -->
      <div class="mb-6">
        <h3 class="text-lg font-medium mb-3 text-gray-700">GET 请求</h3>
        <button 
          @click="testGet" 
          :disabled="httpLoading"
          class="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 disabled:opacity-50 mb-3"
        >
          {{ httpLoading ? '请求中...' : 'GET 用户列表' }}
        </button>
        <div v-if="httpResult" class="p-4 bg-gray-50 rounded">
          <pre class="text-sm overflow-auto">{{ JSON.stringify(httpResult, null, 2) }}</pre>
        </div>
      </div>
      
      <!-- POST 请求 -->
      <div class="mb-6">
        <h3 class="text-lg font-medium mb-3 text-gray-700">POST 请求 - 创建用户</h3>
        <div class="mb-3 space-y-2">
          <input 
            v-model="newUser.name" 
            placeholder="姓名" 
            class="w-full px-3 py-2 border rounded"
          />
          <input 
            v-model="newUser.email" 
            placeholder="邮箱" 
            class="w-full px-3 py-2 border rounded"
          />
        </div>
        <button 
          @click="testPost" 
          :disabled="httpLoading"
          class="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600 disabled:opacity-50 mb-3"
        >
          {{ httpLoading ? '创建中...' : '创建用户' }}
        </button>
        <div v-if="httpResult" class="p-4 bg-gray-50 rounded">
          <pre class="text-sm overflow-auto">{{ JSON.stringify(httpResult, null, 2) }}</pre>
        </div>
      </div>
      
      <!-- PUT 请求 -->
      <div class="mb-6">
        <h3 class="text-lg font-medium mb-3 text-gray-700">PUT 请求 - 更新用户</h3>
        <div class="mb-3 space-y-2">
          <input 
            v-model.number="updateUserId" 
            type="number" 
            placeholder="用户ID" 
            class="w-full px-3 py-2 border rounded"
          />
          <input 
            v-model="updateUser.name" 
            placeholder="新姓名" 
            class="w-full px-3 py-2 border rounded"
          />
        </div>
        <button 
          @click="testPut" 
          :disabled="httpLoading"
          class="px-4 py-2 bg-yellow-500 text-white rounded hover:bg-yellow-600 disabled:opacity-50 mb-3"
        >
          {{ httpLoading ? '更新中...' : '更新用户' }}
        </button>
        <div v-if="httpResult" class="p-4 bg-gray-50 rounded">
          <pre class="text-sm overflow-auto">{{ JSON.stringify(httpResult, null, 2) }}</pre>
        </div>
      </div>
      
      <!-- DELETE 请求 -->
      <div class="mb-6">
        <h3 class="text-lg font-medium mb-3 text-gray-700">DELETE 请求 - 删除用户</h3>
        <div class="mb-3">
          <input 
            v-model.number="deleteUserId" 
            type="number" 
            placeholder="用户ID" 
            class="w-full px-3 py-2 border rounded"
          />
        </div>
        <button 
          @click="testDelete" 
          :disabled="httpLoading"
          class="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600 disabled:opacity-50 mb-3"
        >
          {{ httpLoading ? '删除中...' : '删除用户' }}
        </button>
        <div v-if="httpResult" class="p-4 bg-gray-50 rounded">
          <pre class="text-sm overflow-auto">{{ JSON.stringify(httpResult, null, 2) }}</pre>
        </div>
      </div>
      
      <!-- 错误测试 -->
      <div class="mb-6">
        <h3 class="text-lg font-medium mb-3 text-gray-700">错误处理测试</h3>
        <div class="flex gap-2 flex-wrap mb-3">
          <button 
            @click="testError('400')" 
            class="px-4 py-2 bg-red-400 text-white rounded hover:bg-red-500"
          >
            400 错误
          </button>
          <button 
            @click="testError('401')" 
            class="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
          >
            401 错误
          </button>
          <button 
            @click="testError('403')" 
            class="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
          >
            403 错误
          </button>
          <button 
            @click="testError('404')" 
            class="px-4 py-2 bg-red-700 text-white rounded hover:bg-red-800"
          >
            404 错误
          </button>
          <button 
            @click="testError('500')" 
            class="px-4 py-2 bg-red-800 text-white rounded hover:bg-red-900"
          >
            500 错误
          </button>
        </div>
        <div v-if="httpError" class="p-3 bg-red-50 border border-red-200 rounded text-red-700">
          <strong>错误类型:</strong> {{ httpError.name }}<br>
          <strong>错误消息:</strong> {{ httpError.message }}<br>
          <strong>状态码:</strong> {{ httpError.code }}
        </div>
      </div>
    </section>
    
    <!-- 统一拦截器说明 -->
    <section class="bg-blue-50 rounded-lg shadow-md p-6">
      <h2 class="text-2xl font-semibold mb-4 text-gray-800 border-b pb-2">
        📋 统一拦截器说明
      </h2>
      <div class="space-y-2 text-gray-700">
        <p><strong>✅ 统一请求头:</strong> 所有请求自动添加 Authorization、Content-Type、Accept、X-Requested-With 等</p>
        <p><strong>✅ 统一返回处理:</strong> 自动解包 {code: 200, result: data} 格式，返回 result 字段</p>
        <p><strong>✅ 统一错误处理:</strong> 401 自动跳转登录，其他错误统一格式处理</p>
        <p><strong>✅ 选项支持:</strong> skipAuth、skipErrorHandler 可跳过统一处理</p>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
// 页面标题
useHead({
  title: 'API 测试页面'
})

// ========== useAPI 测试 ==========
// 注意：server/api/test/users.get.ts 映射为 /api/test/users
// baseURL 已设置为 /api，所以路径应该是 /test/users（不包含 /api 前缀）
const { data: usersData, pending: usersPending, error: usersError, refresh: refreshUsers } = await useLazyAPI('/test/users', {
  immediate: false
})

const loadUsers = () => {
  refreshUsers()
}

// 文章列表（懒加载）
const { data: postsData, pending: postsPending, error: postsError, refresh: refreshPosts } = await useLazyAPI('/test/posts', {
  immediate: false
})

const loadPosts = () => {
  refreshPosts()
}

// ========== useAsyncAPI 测试 ==========
const { 
  data: userDetailData, 
  pending: userDetailPending, 
  error: userDetailError,
  refresh: refreshUserDetail 
} = useAsyncAPI('user-detail', async (nuxtApp) => {
  // 使用统一的 $api 实例
  const api = nuxtApp.$api as typeof $fetch
  return await api('/test/users/1')
}, {
  immediate: false
})

const loadUserDetail = () => {
  refreshUserDetail()
}

// ========== useHttpAPI 测试 ==========
const { get, post, put, delete: del } = useHttpAPI()

const httpResult = ref<any>(null)
const httpError = ref<any>(null)
const httpLoading = ref(false)

const newUser = ref({
  name: '',
  email: ''
})

const updateUserId = ref(1)
const updateUser = ref({
  name: ''
})

const deleteUserId = ref(1)

const testGet = async () => {
  httpLoading.value = true
  httpError.value = null
  try {
    httpResult.value = await get('/test/users')
  } catch (error: any) {
    httpError.value = error
    httpResult.value = null
  } finally {
    httpLoading.value = false
  }
}

const testPost = async () => {
  if (!newUser.value.name || !newUser.value.email) {
    alert('请填写姓名和邮箱')
    return
  }
  
  httpLoading.value = true
  httpError.value = null
  try {
    httpResult.value = await post('/test/users', {
      name: newUser.value.name,
      email: newUser.value.email,
      role: 'user'
    })
    // 清空表单
    newUser.value = { name: '', email: '' }
  } catch (error: any) {
    httpError.value = error
    httpResult.value = null
  } finally {
    httpLoading.value = false
  }
}

const testPut = async () => {
  if (!updateUserId.value || !updateUser.value.name) {
    alert('请填写用户ID和姓名')
    return
  }
  
  httpLoading.value = true
  httpError.value = null
  try {
    httpResult.value = await put(`/test/users/${updateUserId.value}`, {
      name: updateUser.value.name
    })
    updateUser.value.name = ''
  } catch (error: any) {
    httpError.value = error
    httpResult.value = null
  } finally {
    httpLoading.value = false
  }
}

const testDelete = async () => {
  if (!deleteUserId.value) {
    alert('请填写用户ID')
    return
  }
  
  httpLoading.value = true
  httpError.value = null
  try {
    httpResult.value = await del(`/test/users/${deleteUserId.value}`)
  } catch (error: any) {
    httpError.value = error
    httpResult.value = null
  } finally {
    httpLoading.value = false
  }
}

const testError = async (type: string) => {
  httpLoading.value = true
  httpError.value = null
  try {
    httpResult.value = await get(`/test/error?type=${type}`)
  } catch (error: any) {
    httpError.value = error
    httpResult.value = null
  } finally {
    httpLoading.value = false
  }
}
</script>
