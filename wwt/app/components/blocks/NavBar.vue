<script setup lang="ts">
interface Props {
  logoText?: string; // 简化处理，实际项目可能是 logoUrl
  phoneNumber?: string;
}

withDefaults(defineProps<Props>(), {
  logoText: 'Wendy Wu Tours',
  phoneNumber: '1300727998'
});
// 自动解构 data, pending, error
// const { data: menuList, pending } = await useApi('/test/blockList')
const menuItems = ['Destinations', 'Tour Styles', 'Solos', 'Cruises', 'Tailor-Made', 'Offers', 'Events'];
</script>

<template>
  <nav class="bg-white border-b border-gray-100 py-4 shadow-sm relative z-20">
    <div class="container mx-auto flex justify-between items-center px-4">
      <!-- Logo -->
      <div class="flex items-center gap-2">
        <div class="w-8 h-8 bg-[#E3342F] rounded-full flex items-center justify-center text-white font-bold italic">
          W
        </div>
        <span class="text-xl font-serif text-gray-800">{{ logoText }}</span>
      </div>

      <!-- Menu (Hidden on mobile) -->
      <ul class="hidden lg:flex gap-6 text-sm text-gray-700 font-medium">
        <li v-for="item in menuItems" :key="item" class="cursor-pointer hover:text-[#E3342F] transition-colors">
          {{ item }}
        </li>
      </ul>

      <!-- Contact -->
      <div class="text-right hidden sm:block">
        <div class="text-xl font-bold text-gray-800">{{ phoneNumber }}</div>
        <div class="text-xs text-gray-500 cursor-pointer hover:underline">Contact Us</div>
      </div>
    </div>
  </nav>
</template>