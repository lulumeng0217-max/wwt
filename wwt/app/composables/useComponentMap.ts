// composables/useCmsComponents.ts
// 1. 显式引入组件 (这里为了类型推导，不建议完全依赖自动导入)
import GlobalAlertBar from "~/components/blocks/GlobalAlertBar.vue";
import TextFeatureSection from "~/components/blocks/TextFeatureSection.vue";
import PromoCard from "~/components/blocks/PromoCard.vue";
import CmsGridContainer from "~/components/Container/CmsGridContainer.vue";
import CmsBadge from "~/components/blocks/CmsBadge.vue";
// ... 未来新增 100 个组件，都在这里 import
// 2. 定义映射配置对象
// 这是一个对象，Key 是字符串，Value 是 Vue 组件
const COMPONENT_MAP = {
  GlobalAlertBar,
  TextFeatureSection,
  PromoCard,
  CmsGridContainer,
  CmsBadge
  // 新增组件只需加在这里，例如: Carousel: CarouselComponent
} as const;
// 3. 【魔法】自动推导出类型
// keyof typeof COMPONENT_MAP 会自动提取上面的 Key，生成联合类型

// ===== 解析 block 名（支持版本）=====
function parseBlockType(type: string) {
  // HeroBanner@v2 → { name: HeroBanner, version: v2 }
  const [name, version = "latest"] = type.split("@");
  return { name, version };
}
export type SupportedComponentType = keyof typeof COMPONENT_MAP;
// 4. 封装获取函数
export function getCmsComponent(type: SupportedComponentType) {
  const { name, version } = parseBlockType(type);
  const component = COMPONENT_MAP[type];
  if (!component) {
    console.warn(`Component [${type}] not found in map.`);
    //     const origin = window.location.origin
    //   const url = `${origin}/blocks/au/${name}/index.js`
    if (typeof window !== "undefined") {
      // 只在浏览器端加载远程模块  https://raw.githubusercontent.com/lulumeng0217-max/wwt/refs/heads/main/blocks/au/block/index.js

      return defineAsyncComponent(
        () =>
          import(
            /* @vite-ignore */
            `http://127.0.0.1:9999/blocks/au/${name}/index.js?`
          ),
      );
    } else {
      // SSR 返回空组件
      return { template: "<div></div>" };
    }
  }
  // 2️⃣ Remote block（🔥 未来）

  return component;
}
