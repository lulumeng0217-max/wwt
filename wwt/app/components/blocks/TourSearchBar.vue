<script setup lang="ts">
// 这里通常不需要太多 CMS 配置，除非你想配置下拉框的默认文字
</script>

<template>
  <div class="bg-[#333333] py-3 border-t border-gray-700 relative z-10">
    <div class="container mx-auto px-4">
      <div class="grid grid-cols-1 md:grid-cols-5 gap-2">
        <!-- Input 1 -->
        <div class="md:col-span-1">
          <input type="text" placeholder="Country, City, Attraction..." class="w-full h-10 px-3 rounded text-sm text-gray-700 focus:outline-none focus:ring-2 focus:ring-red-500"/>
        </div>
        <!-- Selects -->
        <select class="h-10 px-3 rounded text-sm text-gray-600 w-full focus:outline-none">
          <option>Any Month</option>
        </select>
        <select class="h-10 px-3 rounded text-sm text-gray-600 w-full focus:outline-none">
          <option>Any Duration</option>
        </select>
        <select class="h-10 px-3 rounded text-sm text-gray-600 w-full focus:outline-none">
          <option>Any Passengers</option>
        </select>
        
        <!-- Search Button -->
        <button class="bg-[#E3342F] hover:bg-red-700 text-white font-bold h-10 rounded uppercase tracking-wide transition-colors">
          Search
        </button>
      </div>
    </div>
  </div>
</template>