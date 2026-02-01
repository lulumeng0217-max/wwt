<template>
    <!-- 
      1. baseClasses: 这里只写 flex 相关的默认逻辑
      2. 父级传来的 class (比如 'mt-4 bg-red') 会自动合并到这里
      3. 父级传来的 style (比如 'width: 100px') 会自动合并到 style
    -->
    <div class="cms-layout-box" :class="layoutClasses">
        <slot />
    </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'

const props = defineProps<{
    // 只接收影响内部布局的参数
    direction?: 'row' | 'col',
    justify?: 'start' | 'center' | 'between' | 'around',
    align?: 'start' | 'center' | 'end' | 'stretch',
    gap?: string | number,
    wrap?: boolean
}>()

const layoutClasses = computed(() => [
    'flex', // 默认就是 flex 布局
    
    // 内部排列逻辑
    props.direction === 'row' ? 'flex-row' : 'flex-col',
    props.wrap ? 'flex-wrap' : 'flex-nowrap',
    
    props.justify ? `justify-${props.justify}` : '',
    props.align ? `items-${props.align}` : '',
    
    // 处理 gap (假设是 UnoCSS 类)
    (props.gap && !String(props.gap).includes('px')) ? `gap-${props.gap}` : ''
])
</script>

<style scoped>
/* 如果有 gap 是 px 单位的，还是得靠 style 处理，
   但因为透传存在，我们很难直接在 style 里混入 props 计算值。
   所以 gap 最好统一用 UnoCSS 的 gap-4 这种格式，
   或者通过 CSS 变量透传（如下高级技巧）
*/
.cms-layout-box {
    position: relative;
    box-sizing: border-box;
}
</style>