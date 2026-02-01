<script setup lang="ts">
interface Props {
  title: string
}

defineProps<Props>()
</script>

<template>
  <!-- Section 是语义容器，非常适合 CMS -->
  <section class="bg-white py-16">
    <div class="container mx-auto px-4 md:px-8">
      <!-- ✅ Grid = 容器核心职责 -->
      <div
        class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 lg:gap-8">
        <!-- ❗ children 由 CmsRenderer 注入 -->
        <slot />
      </div>

    </div>
  </section>
</template>
