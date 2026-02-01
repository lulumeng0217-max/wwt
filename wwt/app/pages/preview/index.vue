<!-- Nuxt项目: pages/preview.vue -->
<template>
   <div class="page-renderer">
    <template v-for="block in pageData.blocks" :key="block.id">
      <!-- 核心逻辑：动态组件渲染 -->
      <component 
        :is="resolveCmsComponent(block.component)" 
        v-bind="block.props"
        v-if="resolveCmsComponent(block.component)"
      />
      <!-- 开发环境调试用：如果找不到组件 -->
      <div v-else class="p-4 bg-red-100 text-red-600 border border-red-400 text-center">
        [Error: Component "{{ block.component }}" not found]
      </div>
    </template>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'

// 假设你的组件都在 components/blocks 目录下
// 你需要根据 block.type 映射到具体的组件
// 引入所有 Block 组件
const resolveCmsComponent = (type: string) => {
  console.log('Resolving component for type:', type)
    const component = getCmsComponent(type as SupportedComponentType)

    if (!component) {
        console.warn(`[CMS] Component "${type}" not registered`)
        return null
    }

    return component
}

const pageData = ref<any>({})

// 监听来自后台的消息
const handleMessage = (event: MessageEvent) => {
  // 安全检查：建议校验 event.origin，防止恶意调用
  // if (event.origin !== 'http://your-admin-url.com') return;
  const { type, data } = event.data
  console.log('Received message:', event.data)
  if (type === 'CMS_PREVIEW_UPDATE') {
    pageData.value = data
  }
}

onMounted(() => {
  window.addEventListener('message', handleMessage)
  //以此通知后台：预览页加载完毕，可以发数据了
  window.parent.postMessage({ type: 'CMS_PREVIEW_READY' }, '*')
})

onUnmounted(() => {
  window.removeEventListener('message', handleMessage)
})

// 这是一个纯预览页，不需要常规的布局（header/footer），或者你需要特定的 layout
definePageMeta({
  layout: 'default'
})
</script>

<style scoped>
/* 确保预览页背景等样式正确 */
.preview-wrapper {
  min-height: 100vh;
  background: #fff;
}
</style>