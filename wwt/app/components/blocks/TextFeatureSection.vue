<script setup lang="ts">
interface Props {
  title: string
  features: string[]
}

defineProps<Props>()
</script>

<template>
  <section class="bg-[#f9f9f9] py-16 px-4">
    <div class="container mx-auto text-center">
      <!-- 标题 -->
      <h1 class="text-3xl md:text-4xl text-gray-700 font-light mb-8 tracking-wide">
        {{ title }}
      </h1>

      <!-- 特性列表 -->
      <ul class="flex flex-col md:flex-row flex-wrap justify-center items-center gap-y-2 md:gap-x-0 text-gray-600 font-light text-sm md:text-base">
        <li 
          v-for="(feature, index) in features" 
          :key="index"
          class="flex items-center"
        >
          <!-- 竖线分隔符：只在非第一个元素前显示 (移动端隐藏，PC端显示) -->
          <span v-if="index > 0" class="hidden md:inline-block w-[1px] h-4 bg-red-400 mx-4"></span>
          <span>{{ feature }}</span>
        </li>
      </ul>
    </div>
  </section>
</template>