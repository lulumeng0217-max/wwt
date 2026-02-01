<script setup lang="ts">
useHead({
  script: [
    {
      type: 'importmap',
      innerHTML: `
{
  "imports": {
    "vue": "/vueruntime/vue.runtime.esm-browser.js"
  }
}
      `
    }
  ]
})
</script>

<template>
  <div>
     <NuxtLayout>
      <NuxtPage />
    </NuxtLayout>
  </div>
</template>
