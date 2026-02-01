// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  modules: ["@unocss/nuxt", "@nuxt/icon", "@vueuse/nuxt"],
  compatibilityDate: "2025-07-15",
  devtools: { enabled: true },
  css: ["~/assets/css/main.css"],
  runtimeConfig: {
    // 服务端私有配置
    apiSecret: process.env.API_SECRET,

    // 客户端公共配置
    public: {
      // 你的后端接口地址，开发环境可以是 localhost，生产环境是真实域名
      apiBase: process.env.API_BASE_URL || "http://localhost:3000/api",

      // API超时时间
      apiTimeout: process.env.API_TIMEOUT || 10000,

      // 是否启用API调试日志
      apiDebug: process.env.API_DEBUG || false,

      // Token存储key
      tokenKey: "admin.net:access-token",
    },
  },
  // app: {
  //   head: {
  //     title: "WWT CMS",
  //     meta: [
  //       { name: "viewport", content: "width=device-width, initial-scale=1" },
  //       { charset: "utf-8" },
  //       { name: "description", content: "A CMS built with Nuxt 3" },
  //     ],
  //     link: [{ rel: "icon", type: "image/x-icon", href: "/favicon.ico" }],
  //     scripts: [],
  //   },
  // },
  // 实验性功能
  experimental: {
    // 启用payload提取（优化SSR性能）
    payloadExtraction: true,

    // 启用渲染层
    renderJsonPayloads: true,
  },

  // 构建配置
  build: {
    // 转译目标
    transpile: ["ofetch"],
  },

  // Nitro配置（服务端）
  nitro: {
    // 压缩响应
    compressPublicAssets: true,

    // 开发时启用热重载
    devProxy: {
      "/api": {
        target: process.env.API_PROXY_TARGET || "http://localhost:3000",
        changeOrigin: true,
        prependPath: true,
      },
    },
  },
});
