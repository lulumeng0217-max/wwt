# AGENTS.md - Development Guidelines for WWT CMS

This file contains development guidelines and commands for agentic coding agents working on the Wendy Wu Tours CMS (Nuxt 4 application).

## Project Overview

This is a **Nuxt 4 CMS frontend** for Wendy Wu Tours, a travel/tourism company. The application consumes API data and renders dynamic CMS pages using Vue 3 Composition API with TypeScript.

**Architecture**: Component-based CMS block rendering system with three-tier API architecture
**Framework**: Nuxt 4.2.2 + Vue 3.5.27 + TypeScript
**Styling**: UnoCSS (atomic utility-first CSS)
**Package Manager**: pnpm 10.28.1

## Development Commands

### Core Commands
```bash
pnpm dev              # Start development server
pnpm build            # Build for production
pnpm generate         # Generate static site
pnpm preview          # Preview production build
pnpm postinstall      # Nuxt preparation (runs automatically)
```

### Multi-Environment Commands
```bash
# Australia development
pnpm dev:au          # AU development environment
pnpm build:au:dev    # AU dev build
pnpm build:au:prod   # AU production build

# UK development
pnpm dev:uk          # UK development environment
pnpm build:uk:dev    # UK dev build
pnpm build:uk:prod   # UK production build
```

### Testing
**Current Status**: No test framework configured
**Recommended Setup**: 
```bash
# Unit and component testing
pnpm add -D vitest @vue/test-utils

# E2E testing
pnpm add -D playwright

# Run tests
pnpm test              # Run unit tests
pnpm test:unit         # Run single test file
pnpm test:e2e          # Run E2E tests
```

### Code Quality
**Current Status**: No linting/formatting configured
**Recommended Setup**:
```bash
pnpm add -D @nuxt/eslint eslint prettier
pnpm lint              # Run ESLint
pnpm lint:fix          # Auto-fix ESLint issues
pnpm format            # Run Prettier
```

## Code Style Guidelines

### TypeScript & Vue Patterns
- **Use Composition API with `<script setup lang="ts">`**
- **Explicit TypeScript interfaces for all data structures**
- **Auto-imports enabled** - Don't import Vue composables manually
- **Component props with interface definitions and `withDefaults()`**

#### Vue Component Structure
```typescript
<script setup lang="ts">
interface Props {
  title: string
  description?: string
}

// Props with defaults
const props = withDefaults(defineProps<Props>(), {
  description: ''
})

// Auto-imported composables
const route = useRoute()
const { get } = useHttp()  // ✅ Correct
</script>
```

### Import Patterns
```typescript
// Explicit imports for components and types
import GlobalAlertBar from '~/components/blocks/GlobalAlertBar.vue'
import type { CMSPageData, ApiResponse } from '~/types/cms'

// Auto-imported composables (don't import these)
const route = useRoute()      // ✅ Auto-imported
const { get } = useHttp()    // ✅ Auto-imported
```

### Naming Conventions
- **Components**: PascalCase (e.g., `GlobalAlertBar`, `CmsPageRenderer`)
- **Files**: kebab-case for components (e.g., `global-alert-bar.vue`)
- **Types**: PascalCase with descriptive prefixes (e.g., `CMSPageData`, `ApiResponse`, `RequestConfig`)
- **Composables**: camelCase with `use` prefix (e.g., `useHttp`, `useAPI`, `useComponentMap`)
- **CSS Classes**: UnoCSS utility classes with custom color values

### Component Architecture
- **CMS Blocks**: Place in `app/components/blocks/`
- **Dynamic Rendering**: Register components in composables using component mapping
- **Type Safety**: Create TypeScript interfaces for all CMS data
- **Self-contained**: Each CMS block should be independent

#### CMS Block Pattern
```typescript
<script setup lang="ts">
interface Props {
  data: CmsBlockData
}

const props = defineProps<Props>()
</script>

<template>
  <div class="p-4 bg-white rounded">
    <h2>{{ data.title }}</h2>
    <!-- Component content -->
  </div>
</template>
```

### API Integration
#### Three-Tier API Architecture
1. **useHttp.ts** - Base HTTP client with ofetch wrapper
2. **useAPI.ts** - Nuxt useFetch wrappers (useAPI, useLazyAPI, useAsyncAPI)
3. **useHttpAPI.ts** - Traditional HTTP method wrappers

```typescript
// Base HTTP client
const { get, post } = useHttp()

// Nuxt-optimized useFetch
const { data } = await useAPI('/api/page')

// Method-style calls
const result = await post('/api/submit', formData)
```

### Styling Guidelines
- **Utility-first**: Use UnoCSS classes for styling
- **Responsive**: Mobile-first approach with responsive prefixes
- **Custom colors**: Use defined brand colors from UnoCSS config
- **Component styling**: Prefer utility classes over CSS modules

#### Responsive Design
```html
<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
  <!-- Content -->
</div>
```

### Error Handling
- **HTTP Errors**: Handled by `useHttp` composable interceptors
- **Component Errors**: Use try-catch for async operations
- **Validation**: TypeScript interfaces provide compile-time safety

#### Multi-Layer Error Handling
```typescript
// 1. HTTP Error Interceptor (useHttp.ts)
onResponseError({ response }) {
  if (code === 401) {
    token.value = ''
    navigateTo('/login')
  }
  throw error
}

// 2. Business Error Interceptor
onResponse({ response }) {
  if (res.code !== 200) {
    const error = new Error(res.message)
    error.name = 'BusinessError'
    throw error
  }
  return res.result
}

// 3. Component-Level Error Handling
try {
  const data = await get<CMSPageData>('/api/page')
  return data
} catch (error) {
  console.error('Failed to load page:', error)
  throw error
}
```

## File Structure

```
app/
├── components/
│   ├── blocks/          # CMS component blocks
│   ├── layouts/         # Layout components
│   └── common/          # Common components
├── composables/          # Vue composables
│   ├── useHttp.ts        # Base HTTP client
│   ├── useAPI.ts         # Nuxt useFetch wrappers
│   ├── useAsyncAPI.ts    # Async data handlers
│   └── useComponentMap.ts # Component registration
├── pages/               # Nuxt pages
├── types/              # TypeScript definitions
│   ├── api.ts          # API response types
│   └── cms.ts          # CMS data types
├── plugins/            # Nuxt plugins
│   └── api.ts          # Custom $fetch instance
├── server/api/            # Nuxt server API routes
├── assets/css/         # Custom styling
└── utils/              # Utility functions
```

## CMS Component Development

### Creating New CMS Blocks
1. Create component in `app/components/blocks/[name].vue`
2. Define TypeScript interface for block data in `app/types/cms.ts`
3. Register component in `app/composables/useComponentMap.ts`
4. Test with CMS data in development

### Component Registration
```typescript
// In useComponentMap.ts
import { defineComponentMap } from '#imports'

const COMPONENT_MAP = {
  GlobalAlertBar,
  TextFeatureSection,
  // Add new components here
} as const

export const COMPONENT_MAP = defineComponentMap(COMPONENT_MAP)
```

## Important Notes

### API Security
- **Token Management**: Tokens stored in `useStorage('admin.net:access-token', '')`
- **Authentication**: Automatic Bearer token injection via interceptors
- **Environment Variables**: Use `useRuntimeConfig()` for sensitive data

### Build Optimization
- **SSR**: Enabled with payload extraction
- **Compression**: Enabled for production assets
- **Caching**: Intelligent caching for API responses
- **Code Splitting**: Automatic for optimal loading

## Common Patterns

### Async Data Loading
```typescript
// With automatic refetching
const { data, refresh, error } = await useAPI('/api/posts', {
  query: { category: 'tech' },
  watch: [category]
})

// With transformation
const { data: transformedData } = await useAPI('/api/raw-data', {
  transform: (data) => {
    return { ...data, processed: true }
  }
})
```

### Dynamic Component Rendering
```typescript
// Component resolution
const { getComponent } = useComponentMap()

// Dynamic rendering with props
const component = getComponent(block.type)
return h(component, { data: blockData, ...block.props })
```

This guide should be updated as the project evolves. Always prioritize type safety, performance, and maintainability when working with this codebase.