<script setup lang="ts">
import { getCmsComponent, type SupportedComponentType } from '~/composables/useComponentMap'
import CmsRenderer from '~/components/CmsRenderer.vue'
/** 1️⃣ 接收 slug */
const props = defineProps<{
  slug: string
}>()

/** 2️⃣ 拉 CMS 页面数据 */
const { data: pageRes, pending, error } = await useAsyncData(
  `cms-page:${props.slug}`,
  () => useHttpAPI().get('/page')
)

/** 3️⃣ 找到当前页面 */
const page = computed(() => {
  return pageRes.value?.data?.find(
    (p: any) => p.request_path === props.slug
  )
})

/** 4️⃣ 404 */
watchEffect(() => {
  if (!pending.value && !page.value) {
    throw createError({ statusCode: 404 })
  }
})

/** 5️⃣ SEO（只在 page 存在时） */
watchEffect(() => {
  if (!page.value) return

  useSeoMeta({
    title: page.value.title,
    description: page.value.description,
    ogImage: page.value.og_image,
  })
})

/** 6️⃣ 动态组件解析 */
// const resolveCmsComponent = (type: string) => {
//   const component = getCmsComponent(type as SupportedComponentType)

//   if (!component) {
//     console.warn(`[CMS] Component "${type}" not registered`)
//     return null
//   }

//   return component
// }
</script>

<template>
  <div
    v-if="page"
    :class="page.page_config?.custom_css_class"
    :style="{ backgroundColor: page.page_config?.background_color }"
  >

  <CmsRenderer :Comp="page.components" />
    <!-- <component
      v-for="block in page.components"
      :key="block.id"
      :is="resolveCmsComponent(block.type)"
      v-bind="block.props"
      :style="block.styles"
    > -->
          <!-- 3. 关键：如果有 children，通过默认插槽递归渲染自己 -->
    <!-- <template v-if="block.children && block.children.length" #default>
      <CmsPageRenderer 
        v-for="child in block.children" 
        :key="child.id" 
        :widget="child" 
      />
    </template> -->
    <!-- </component> -->
  
  </div>
</template>
