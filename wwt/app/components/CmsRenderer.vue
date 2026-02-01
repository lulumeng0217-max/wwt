<template>
        <component v-for="block in props.Comp" :key="block.id" :is="resolveCmsComponent(block.type)"
            v-bind="block.props" :style="block.styles" >
            <template v-if="block.children && block.children.length">
                <CmsRenderer :Comp="block.children" ></CmsRenderer>
            </template>

        </component>
</template>
<script setup lang="ts">
import type { CMSComponentBlock } from '~/types/cms'
/** 1️⃣ 接收 slug */
const props = defineProps<{
    Comp: Array<CMSComponentBlock> | undefined
}>()
console.log('CmsRenderer props.Comp:', props.Comp)
const resolveCmsComponent = (type: string) => {
    const component = getCmsComponent(type as SupportedComponentType)

    if (!component) {
        console.warn(`[CMS] Component "${type}" not registered`)
        return null
    }

    return component
}
</script>