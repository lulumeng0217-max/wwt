<script setup lang="ts">
interface Props {
  imageUrl?: string;
  title?: string;
  subtitle?: string;
  showVisaBadge?: boolean;
}

withDefaults(defineProps<Props>(), {
  imageUrl: 'https://images.unsplash.com/photo-1528360983277-13d9012356ee?q=80&w=2070&auto=format&fit=crop', // 默认日本图片
  title: 'Worldwide Sale',
  subtitle: 'Save up to $2,000 per couple on future tours',
  showVisaBadge: true
});
</script>

<template>
  <div class="relative w-full h-[500px] flex items-center justify-center overflow-hidden">
    <!-- 背景图 -->
    <div 
      class="absolute inset-0 bg-cover bg-center bg-no-repeat transition-transform duration-700 hover:scale-105"
      :style="{ backgroundImage: `url(${imageUrl})` }"
    >
      <!-- 黑色遮罩，让文字更清晰 -->
      <div class="absolute inset-0 bg-black/20"></div>
    </div>

    <!-- 内容区 -->
    <div class="relative z-10 text-center text-white px-4">
      <h1 class="text-4xl md:text-6xl font-bold mb-4 drop-shadow-md">{{ title }}</h1>
      <p class="text-lg md:text-2xl font-light drop-shadow-md">{{ subtitle }}</p>
    </div>

    <!-- Visa Free Badge (左侧悬浮) -->
    <div v-if="showVisaBadge" class="absolute left-[10%] top-10 hidden md:flex flex-col items-center justify-center w-32 h-32 bg-[#E3342F] rounded-full text-white shadow-lg rotate-[-15deg] border-4 border-white/30">
      <span class="text-xs font-bold uppercase tracking-wider">Travel</span>
      <span class="text-2xl font-extrabold leading-none">VISA</span>
      <span class="text-xl font-bold">FREE*</span>
    </div>
    
    <!-- 底部装饰条 (模拟图中日本寺庙下面的红栏杆遮挡感，可选) -->
    <div class="absolute bottom-0 w-full h-12 bg-gradient-to-t from-black/50 to-transparent"></div>
  </div>
</template>