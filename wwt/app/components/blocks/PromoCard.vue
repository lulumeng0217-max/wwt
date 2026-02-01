<script setup lang="ts">
export interface PromoCardProps {
  id?: string | number
  imgSrc: string
  badgeText?: string
  // 支持 CMS 传入不同颜色的配置
  badgeColor?: 'orange' | 'red' | 'blue'
  title: string
  description: string
  btnText?: string
  btnLink: string
  // 底部额外信息 (如: deposit $99pp)
  footerText?: string 
}

const props = withDefaults(defineProps<PromoCardProps>(), {
  badgeColor: 'orange',
  btnText: 'View Offer'
})

// UnoCSS 动态类名映射
const badgeColorClass = {
  orange: 'bg-[#e67e22]', // 截图里的橙色
  red: 'bg-[#e74c3c]',
  blue: 'bg-[#3498db]'
}
</script>

<template>
  <!-- h-full 确保在 grid 中卡片高度拉伸一致 -->
   
  <div class="cms-container group flex flex-col h-full bg-white shadow-sm hover:shadow-xl transition-shadow duration-300 border border-gray-100">
    
    <!-- 顶部图片区域 -->
    <div class="relative w-full aspect-[4/3] overflow-hidden bg-gray-200">
      <!-- 左上角 Badge -->
      <div 
        v-if="badgeText"
        class="absolute top-4 left-0 z-10 px-4 py-1.5 text-white text-xs font-bold uppercase tracking-wider shadow-md"
        :class="badgeColorClass[badgeColor]"
      >
        {{ badgeText }}
      </div>
      
      <!-- 图片: 生产环境建议用 NuxtImg -->
      <img 
        :src="imgSrc" 
        :alt="title" 
        class="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500 ease-out"
      />
    </div>

    <!-- 内容区域: flex-grow 让它占据剩余空间 -->
    <div class="flex flex-col flex-grow p-6 md:p-8 text-center items-center">
      <h3 class="text-xl font-semibold text-gray-800 mb-4 leading-tight">
        {{ title }}
      </h3>
      
      <p class="text-gray-500 text-sm leading-relaxed mb-8 line-clamp-4 flex-grow">
        {{ description }}
      </p>

      <!-- 按钮区域 -->
      <div class="mt-auto flex flex-col items-center gap-3">
        <NuxtLink 
          :to="btnLink"
          class="inline-block px-8 py-2.5 rounded-full bg-[#ef5350] text-white font-medium hover:bg-[#e53935] transition-colors shadow-sm hover:shadow-md"
        >
          {{ btnText }}
        </NuxtLink>

        <!-- 只有部分卡片有的底部文字 -->
        <p v-if="footerText" class="text-xs text-gray-400 font-medium">
          {{ footerText }}
        </p>
      </div>
    </div>
    <!-- ❗❗ 必须加这个，子组件(children)才会渲染在这里 ❗❗ -->
    <div class="nested-children">
      <slot />
    </div>
  </div>
</template>