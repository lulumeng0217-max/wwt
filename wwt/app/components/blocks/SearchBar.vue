<script setup lang="ts">
/**
 * TourSearchForm.vue
 * 基于 Nuxt 4 + UnoCSS 的旅游搜索组件
 */
import { ref, reactive, computed } from 'vue'

// --- 定义 Props (可选，如果你想从父组件传入配置) ---
interface Props {
  initialLocation?: string
}
const props = withDefaults(defineProps<Props>(), {
  initialLocation: ''
})

// --- 1. 模拟数据定义 (Mock Data) ---

// 这里的 Duration 数据对应原代码中的 option
const durationOptions = [
  { label: '1-10 days', value: '1,10' },
  { label: '10-15 days', value: '10,15' },
  { label: '15-20 days', value: '15,20' },
  { label: '20-30 days', value: '20,30' },
]

// 乘客数据生成 (1-10)
const passengerOptions = Array.from({ length: 10 }, (_, i) => ({
  label: `${i + 1} Passenger${i > 0 ? 's' : ''}`,
  value: i + 1
}))

// 日期数据生成 (逻辑复刻自 C# 代码：当前时间 -> 明年年底)
const dateOptions = computed(() => {
  const options = []
  const now = new Date()
  const currentYear = now.getFullYear()
  const currentMonth = now.getMonth()

  // 循环今年和明年
  for (let y = currentYear; y <= currentYear + 1; y++) {
    for (let m = 0; m < 12; m++) {
      // 创建逻辑日期 (每月15号)
      const targetDate = new Date(y, m, 15)
      
      // 只显示未来的月份
      if (targetDate > now) {
        const value = `${y}${String(m + 1).padStart(2, '0')}` // yyyyMM
        const label = targetDate.toLocaleDateString('en-US', { year: 'numeric', month: 'short' }) // MMM yyyy
        options.push({ value, label })
      }
    }
  }
  return options
})

// --- 2. 表单状态管理 ---
const formState = reactive({
  q: props.initialLocation, // 目的地
  dep: '', // 出发日期
  dur: '', // 时长
  pax: 0,  // 人数
})

// --- 3. 交互逻辑 ---
const isMobileMenuOpen = ref(false)
const toggleMobileMenu = () => isMobileMenuOpen.value = !isMobileMenuOpen.value

// 定义 Emits，将搜索事件抛给父组件处理
const emit = defineEmits<{
  (e: 'search', params: typeof formState): void
}>()

const handleSearch = () => {
  // 关闭移动端菜单
  isMobileMenuOpen.value = false
  // 触发搜索事件，传递数据
  emit('search', { ...formState })
  
  // 如果需要直接跳转：
  // navigateTo({ path: '/search', query: { ...formState } })
}
</script>

<template>
  <div class="search-form-wrapper w-full relative">
    
    <!-- Mobile Trigger (原代码: #search-mobile) -->
    <!-- 仅在移动端显示 (md:hidden) -->
    <div class="md:hidden py-2 px-4 bg-white/50">
      <div 
        class="w-full rounded border border-gray-300 bg-white px-4 py-3 text-gray-500 shadow-sm flex items-center justify-between cursor-pointer"
        @click="toggleMobileMenu"
      >
        <span>{{ formState.q || 'Country, City, Attraction or Tour' }}</span>
        <div class="i-carbon-search text-xl text-red-500"></div>
      </div>
    </div>

    <!-- Main Form Container -->
    <!-- 桌面端常显，移动端根据 isMobileMenuOpen 切换 -->
    <div 
      class="transition-all duration-300 ease-in-out"
      :class="[
        isMobileMenuOpen 
          ? 'fixed inset-0 z-50 bg-white p-4 flex flex-col justify-start overflow-y-auto' 
          : 'hidden md:block mx-auto max-w-6xl p-2'
      ]"
    >
      <!-- Mobile Close Button -->
      <button 
        v-if="isMobileMenuOpen"
        class="md:hidden w-full mb-6 py-3 bg-gray-100 text-gray-700 rounded-lg font-bold flex items-center justify-center gap-2 active:bg-gray-200"
        @click="toggleMobileMenu"
      >
        <div class="i-carbon-close text-xl"></div>
        Close Search
      </button>

      <form 
        @submit.prevent="handleSearch"
        class="flex flex-col md:flex-row md:items-end md:gap-2 gap-4"
      >
        <!-- 1. Search Input -->
        <div class="flex-grow md:w-1/3">
          <div class="relative group">
            <input 
              v-model="formState.q"
              type="text" 
              class="w-full h-12 px-4 rounded border border-gray-300 outline-none transition-colors focus:border-red-500 focus:ring-1 focus:ring-red-500"
              placeholder="Country, City, Attraction or Tour" 
            />
          </div>
        </div>

        <!-- 2. Date Select -->
        <div class="md:w-1/6">
          <div class="relative">
            <select 
              v-model="formState.dep"
              class="w-full h-12 px-4 bg-white rounded border border-gray-300 outline-none appearance-none cursor-pointer focus:border-red-500"
            >
              <option value="">Any Month</option>
              <option v-for="opt in dateOptions" :key="opt.value" :value="opt.value">
                {{ opt.label }}
              </option>
            </select>
            <!-- Custom Arrow -->
            <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-3 text-gray-500">
              <div class="i-carbon-chevron-down w-4 h-4"></div>
            </div>
          </div>
        </div>

        <!-- 3. Duration Select -->
        <div class="md:w-1/6">
          <div class="relative">
            <select 
              v-model="formState.dur"
              class="w-full h-12 px-4 bg-white rounded border border-gray-300 outline-none appearance-none cursor-pointer focus:border-red-500"
            >
              <option value="">Any Duration</option>
              <option v-for="opt in durationOptions" :key="opt.value" :value="opt.value">
                {{ opt.label }}
              </option>
            </select>
             <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-3 text-gray-500">
              <div class="i-carbon-chevron-down w-4 h-4"></div>
            </div>
          </div>
        </div>

        <!-- 4. Pax Select -->
        <div class="md:w-1/6">
          <div class="relative">
            <select 
              v-model="formState.pax"
              class="w-full h-12 px-4 bg-white rounded border border-gray-300 outline-none appearance-none cursor-pointer focus:border-red-500"
            >
              <option :value="0">Any Passengers</option>
              <option v-for="opt in passengerOptions" :key="opt.value" :value="opt.value">
                {{ opt.label }}
              </option>
            </select>
             <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-3 text-gray-500">
              <div class="i-carbon-chevron-down w-4 h-4"></div>
            </div>
          </div>
        </div>

        <!-- 5. Submit Button -->
        <div class="md:w-1/6">
          <button 
            type="submit" 
            class="w-full h-12 rounded bg-red-600 text-white font-bold hover:bg-red-700 transition-colors shadow-md flex items-center justify-center gap-2"
          >
            <div class="i-carbon-search md:hidden"></div>
            Search
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>
/* 
如果未安装 @unocss/preset-icons，
请删除模板中的 <div class="i-carbon-..." /> 
并使用下方 CSS 简单的箭头替代 
*/
select {
  /* 确保文字不会覆盖箭头 */
  padding-right: 2rem; 
}
</style>