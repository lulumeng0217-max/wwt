# 环境变量配置说明

本项目支持多个环境配置，分别对应不同地区和不同环境。

## 📁 环境文件列表

- `.env.au.dev` - 澳大利亚开发环境
- `.env.uk.dev` - 英国开发环境
- `.env.au.product` - 澳大利亚生产环境
- `.env.uk.product` - 英国生产环境
- `.env.example` - 环境变量模板

## 🚀 使用方法

### 方法一：使用 npm/pnpm 脚本（推荐）

```bash
# 开发环境 - 澳大利亚
pnpm dev:au

# 开发环境 - 英国
pnpm dev:uk

# 构建 - 澳大利亚开发环境
pnpm build:au:dev

# 构建 - 英国开发环境
pnpm build:uk:dev

# 构建 - 澳大利亚生产环境
pnpm build:au:prod

# 构建 - 英国生产环境
pnpm build:uk:prod
```

### 方法二：手动复制环境文件

```bash
# Windows PowerShell
Copy-Item .env.au.dev .env
pnpm dev

# Windows CMD
copy .env.au.dev .env
pnpm dev

# Linux/Mac
cp .env.au.dev .env
pnpm dev
```

**注意**：如果 `pnpm dev:au` 等脚本在 Windows 上不工作，请使用方法二手动复制文件。

### 方法三：使用 dotenv-cli（需要安装）

```bash
# 安装 dotenv-cli
pnpm add -D dotenv-cli

# 使用指定环境文件
dotenv -e .env.au.dev -- pnpm dev
```

## 📝 环境变量说明

| 变量名 | 说明 | 示例值 |
|--------|------|--------|
| `API_BASE_URL` | API 基础地址 | `http://localhost:3000/api` |
| `API_PROXY_TARGET` | API 代理目标 | `http://localhost:3000` |
| `API_TIMEOUT` | API 超时时间（毫秒） | `10000` |
| `API_DEBUG` | 是否启用调试日志 | `true` / `false` |
| `API_SECRET` | API 密钥（服务端私有） | `your-secret-key` |
| `NODE_ENV` | Node 环境 | `development` / `production` |
| `REGION` | 地区标识 | `AU` / `UK` |
| `ENVIRONMENT` | 环境标识 | `dev` / `product` |

## ⚠️ 注意事项

1. **不要提交敏感信息**：`.env.*` 文件已在 `.gitignore` 中，不会被提交到 Git
2. **生产环境密钥**：请在生产环境的 `.env.*.product` 文件中使用强密钥
3. **API 地址**：请根据实际部署情况修改生产环境的 `API_BASE_URL`
4. **环境切换**：切换环境后需要重启开发服务器

## 🔧 自定义配置

如果需要修改环境变量，请编辑对应的 `.env.*` 文件，然后重启应用。

## 📚 相关文档

- [Nuxt 环境变量文档](https://nuxt.com/docs/guide/going-further/runtime-config#environment-variables)
- [项目 API 使用指南](./API_USAGE_GUIDE.md)
