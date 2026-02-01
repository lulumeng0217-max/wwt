<!-- components/cms/CmsBadge.vue -->
<template>
    <!-- 
      这里没有任何样式逻辑 
      class="cms-badge" 只是为了方便可能有全局 CSS 覆盖，或者空置也行
      所有的 absolute, top-0, bg-red 等都由父级 CmsRenderer 透传进来
    -->
    <div class="cms-badge">
        {{ props.text }}
    </div>
</template>

<script setup lang="ts">
const props = defineProps<{
    text: string // 只接收内容
}>()
</script>
<style scoped>
.cms-badge {
    background-color: #dc2626;
    color: #ffffff;
    font-size: 12px;
    font-weight: 700;
    padding: 4px 12px;
    border-bottom-left-radius: 8px
}
</style>