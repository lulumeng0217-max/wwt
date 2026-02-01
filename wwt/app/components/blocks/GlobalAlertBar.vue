<script setup lang="ts">
interface Props {
  text: string
  linkText?: string
  linkUrl?: string
}

// 设置默认值
withDefaults(defineProps<Props>(), {
  linkText: 'Read the latest facts >>',
  linkUrl: '#'
})
</script>

<template>
  <!-- bg-[#00a699]: 根据截图吸取的青色 -->
  <div class="w-full bg-[#00a699] text-white py-3 px-4 text-center text-sm md:text-base font-medium">
    <div class="container mx-auto flex flex-col md:flex-row items-center justify-center gap-2">
      <!-- 简单的 SVG 图标 -->
      <div class="flex items-center gap-2">
        <div class="i-heroicons-information-circle-solid text-xl bg-white rounded-full text-[#00a699]" />
        <!-- 如果没装 icon preset，可以用下面的 svg 替代 -->
        <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
          <path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clip-rule="evenodd" />
        </svg>
        <span>{{ text }}</span>
      </div>
      
      <NuxtLink 
        v-if="linkUrl" 
        :to="linkUrl" 
        class="underline hover:text-gray-100 transition-colors"
      >
        {{ linkText }}
      </NuxtLink>
    </div>
  </div>
</template>