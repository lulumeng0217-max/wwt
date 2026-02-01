<!-- <script setup lang="ts">
import { loadRemoteComponent } from '~/utils/loadRemoteComponent'

const props = defineProps<{
    block: {
        url: string
        props?: Record<string, any>
    }
}>()

const Comp = computed(() => {
    return loadRemoteComponent(props.block.url)
})
</script>

<template>
    <ClientOnly>
        <component :is="Comp" v-bind="block.props" />
    </ClientOnly>
</template> -->
