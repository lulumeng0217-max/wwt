// types/cms.ts
// 1. 页面配置类型
export interface PageConfig {
  enable_header: boolean;
  enable_footer: boolean;
  background_color: string;
  custom_css_class: string;
}
// 2. SEO 数据类型
export interface SeoData {
  title: string;
  description: string;
  keywords: string;
  canonical_url: string;
  robots: string;
  og_title: string;
  og_image: string;
  og_type: string;
  json_ld: Record<string, any>;
}
// 3. 组件块基础接口
export interface CMSComponentBlock {
  id: string;
  type: "GlobalAlertBar" | "TextFeatureSection" | "OffersSection" | string; // 扩展为 string 以兼容未来新组件
  sort_order: number;
  is_visible: boolean;
  // UnoCSS 类名字符串 (e.g. "mt-4 hidden md:flex")
  class?: string;

  // 业务参数 & 布局参数
  props: Record<string, any>;
  // 内联样式字符串 (e.g. "border: 1px solid red;")
  styles?: Record<string, any>;
  children?: CMSComponentBlock[];
}
// 4. 核心：页面数据接口 (完整字段版)
export interface CMSPageData {
  // --- 原 page_info 字段 ---
  id: number;
  name: string;
  status: "published" | "draft" | "archived";
  template: string;
  last_updated: string; // ISO 8601 格式时间
  // --- 原 route_info 字段 ---
  request_path: string;
  real_path: string;
  is_dynamic: boolean;
  // --- 原 seo 字段 ---
  title: string;
  description: string;
  keywords: string;
  canonical_url: string;
  robots: string;
  og_title: string;
  og_image: string;
  og_type: string;
  json_ld: Record<string, any>;
  // --- 原 page_config 字段 ---
  page_config: PageConfig;
  // --- 动态组件列表 ---
  components: CMSComponentBlock[];
}
