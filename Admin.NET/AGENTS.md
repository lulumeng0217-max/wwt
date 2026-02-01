# AGENTS.md

Guidelines for agentic coding agents working in the Admin.NET repository.

## Project Overview

Admin.NET is a .NET8/10 universal permission development framework based on Furion/SqlSugar with Vue3+Element-plus+Vite5 frontend. Modular plugin-style development with multi-tenant support, caching, data validation, authentication, event bus, dynamic API, and more.

## Repository Structure

```
Admin.NET/
├── Admin.NET/                    # .NET Backend Solution
│   ├── Admin.NET.Core/          # Core Framework Layer
│   ├── Admin.NET.Application/   # Application Layer
│   ├── Admin.NET.Web.Core/      # Web Core Layer
│   ├── Admin.NET.Web.Entry/     # Web Entry Point (main API)
│   ├── Admin.NET.Test/          # Test Projects (xUnit + Selenium)
│   └── Plugins/                 # Plugin Projects
└── Web/                         # Vue3 Frontend
    ├── src/
    │   ├── api-services/        # API Services and Models
    │   ├── components/          # Vue Components
    │   └── views/              # Page Views
    └── package.json
```

## Build, Lint, and Test Commands

### .NET Backend (in Admin.NET/ directory)

```bash
# Build entire solution
dotnet build Admin.NET.sln

# Build specific project
dotnet build Admin.NET.Web.Entry/Admin.NET.Web.Entry.csproj

# Run the web application
dotnet run --project Admin.NET.Web.Entry/Admin.NET.Web.Entry.csproj

# Watch for changes and auto-restart
dotnet watch run --project Admin.NET.Web.Entry/Admin.NET.Web.Entry.csproj

# Run all tests
dotnet test Admin.NET.Test/Admin.NET.Test.csproj

# Run a single test by name
dotnet test --filter "TestMethodName"

# Run tests with verbosity
dotnet test --verbosity normal
```

### Frontend (in Web/ directory)

```bash
# Install dependencies
pnpm install

# Start development server
pnpm run dev

# Build for production
pnpm run build

# Run lint and fix
pnpm run lint-fix

# Format code with Prettier
pnpm run format

# Build API services (after backend changes)
pnpm run build-api
pnpm run build-all-api

# Translation utility
pnpm run translate
```

## Code Style Guidelines

### C# (.NET Backend)

#### File Headers

Every C# file must start with the copyright header:

```csharp
// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！
```

#### Naming Conventions

- **Classes**: PascalCase (e.g., `UserService`, `DemoOpenApi`)
- **Interfaces**: PascalCase with `I` prefix (e.g., `IDynamicApiController`)
- **Methods**: PascalCase (e.g., `GetUserList`, `HelloWord`)
- **Properties**: PascalCase (e.g., `FirstName`, `CreatedAt`)
- **Fields**: CamelCase with underscore prefix for private fields (`_userManager`)
- **Constants**: PascalCase (e.g., `ConfigConst`, `CacheConst`)

#### Global Usings

Use `GlobalUsings.cs` files for common imports. See existing files in Core and Application projects for examples.

#### Class Structure

1. Copyright header
2. Namespace declaration
3. XML documentation comments (`/// <summary>`)
4. Class attributes
5. Class declaration
6. Fields and dependencies
7. Constructor
8. Methods

#### API Controllers

- Inherit from `IDynamicApiController`
- Use `[ApiDescriptionSettings]` attribute for API grouping
- Use `[Authorize]` attribute for authentication
- Use dependency injection via constructors

#### Entity Classes

- Use `[SugarTable]` attribute for database mapping
- Include XML documentation comments for all properties

### TypeScript/Vue Frontend

#### File Naming

- **Components**: PascalCase (e.g., `UserManagement.vue`, `TagSwitch.vue`)
- **Views**: PascalCase (e.g., `SystemUser.vue`)
- **Services**: camelCase with kebab-case files (e.g., `user-service.ts`)

#### Formatting (Prettier)

- Tab width: 2 spaces (uses tabs)
- Print width: 200 characters
- Single quotes: yes
- Semicolons: yes
- Trailing commas: es5

#### Component Structure

```vue
<template>
  <!-- Template content -->
</template>

<script setup lang="ts">
// 1. External libraries
// 2. Internal utilities and stores
// 3. Components and services
// 4. Types and interfaces
</script>

<style scoped>
/* Component styles */
</style>
```

#### TypeScript Guidelines

- Use strict TypeScript settings
- Prefer interfaces over types for object shapes
- Use `| null` for optional properties in interfaces
- Include JSDoc comments for auto-generated API models

#### ESLint Configuration

The project uses a permissive ESLint configuration with most rules disabled:

- TypeScript strict mode is disabled (`@typescript-eslint/no-explicit-any: 'off'`)
- Vue template rules are mostly disabled for flexibility
- Console usage is allowed (`no-console: 'off'`)
- Unused variables are warnings, not errors

### Testing

#### .NET Tests

- Use xUnit framework with `[Fact]` attribute
- Inherit from `BaseTest` for Selenium-based UI tests
- Test class names should end with `Test` (e.g., `UserTest`)
- Test method names should describe the action being tested

#### Frontend Tests

- No specific test framework is currently configured
- Follow standard Vue testing practices when adding tests

## Development Workflow

1. **Backend Development**:
   - Make changes to appropriate layer (Core, Application, or Web)
   - Use `dotnet watch run` for hot reloading
   - Test with Postman or Swagger UI

2. **Frontend Development**:
   - Use `pnpm run dev` for hot reloading
   - API models are auto-generated, do not edit manually
   - Use `pnpm run build-api` after backend API changes

3. **Plugin Development**:
   - Create new projects in `Plugins/` directory
   - Follow the existing plugin structure with `Startup.cs`
   - Use dependency injection and modular design

## Important Notes

- **Database**: Uses SqlSugar ORM with automatic migrations
- **Authentication**: JWT-based with configurable schemes
- **Frontend Build**: Uses Vite with TypeScript and Vue 3 Composition API
- **Multi-tenancy**: Built-in support for tenant isolation
- **Caching**: Redis integration with fallback to memory cache
- **File Storage**: Configurable (local, Aliyun OSS, Tencent COS)
- **Logging**: Structured logging with multiple providers

When making changes, always test both backend and frontend components, and ensure all builds pass before committing.
