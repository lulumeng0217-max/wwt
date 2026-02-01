<template>
  <div>
    <ComponentHeader />
    <slot />
    <ComponentFooter />
  </div>
</template>
<script setup lang="ts">
import ComponentHeader from '@/components/Header.vue'
import ComponentFooter from '@/components/Footer.vue'
</script>