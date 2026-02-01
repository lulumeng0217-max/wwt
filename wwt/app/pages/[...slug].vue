	<script setup lang="ts">
	const route = useRoute()
     console.log('Current slug/path:', route);
	// [...slug] 捕获到的路径是一个数组，我们需要拼回去，例如 ['about', 'team'] -> '/about/team'
	// 如果 Nuxt 给出的是完整的 path，直接用 route.path 即可
	// 在 Nuxt 的 catch-all 路由中，route.path 通常就是完整的请求路径
	const currentSlug = route.path
    console.log('Current slug/path:', currentSlug);
	// 监听数据加载情况，如果没数据则抛出 404 错误，让 Nuxt 显示 404 页面
	// 注意：这里我们依赖 CmsPageRenderer 内部的 useFetch 的 error 状态
	// 但为了触发 Nuxt 的 404 页面，我们需要在顶层 throw error
	// 这里为了简单演示，我们假设 CmsPageRenderer 已经处理了显示，或者我们可以使用 suspense 处理
	// 另一种简单的写法：直接传递 slug，CmsPageRenderer 内部处理 UI 显示
	</script>
	<template>
	  <CmsPageRenderer :slug="currentSlug" />
	</template>