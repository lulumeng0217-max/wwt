<script setup lang="ts">
interface Props {
  text?: string;
  buttonText?: string;
  buttonLink?: string;
}

// 设置默认值，防止 CMS 没填数据时报错
withDefaults(defineProps<Props>(), {
  text: 'Worldwide Sale ONLY - China - Save $1600',
  buttonText: 'View Offer',
  buttonLink: '#'
});
</script>

<template>
  <div class="bg-black text-white text-xs py-2 px-4">
    <div class="container mx-auto flex flex-col md:flex-row justify-between items-center gap-2">
      <!-- 左侧：促销文案 + 按钮 -->
      <div class="flex items-center gap-4 flex-1">
        <span class="font-medium truncate">{{ text }}</span>
        <a :href="buttonLink" class="bg-[#E3342F] hover:bg-red-700 text-white px-3 py-1 rounded text-xs transition-colors">
          {{ buttonText }}
        </a>
      </div>

      <!-- 右侧：工具栏 (暂时写死，也可配置) -->
      <div class="flex items-center gap-4 text-gray-300">
        <div class="flex items-center cursor-pointer hover:text-white">
          <span>🌐 Select: AU</span>
        </div>
        <div class="flex items-center cursor-pointer hover:text-white">
          <span>👤 Login or Register</span>
        </div>
      </div>
    </div>
  </div>
</template>