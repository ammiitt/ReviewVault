# 🎬 ReviewVault — Movie & Anime Blog Platform

A full-stack blog platform for movie, anime, and series reviews. Built with **.NET 8** and **Angular 17+**, following **Onion Architecture** principles with JWT authentication, deployed on **Microsoft Azure**.

🔗 **Live Site:** [ReviewVault](https://zealous-ocean-0cec1c710.azurestaticapps.net)
🔗 **API Docs:** [Swagger](https://reviewvault-api-c8cabmamg0brachb.centralindia-01.azurewebsites.net/swagger)

---

## 📸 Screenshots

### Home Page (Dark Mode)
![Home Dark](docs/screenshots/home-dark.png)

### Home Page (Light Mode)
![Home Light](docs/screenshots/home-light.png)

### Post Detail
![Post Detail](docs/screenshots/post-detail.png)

### Trending
![Trending](docs/screenshots/trending.png)

### Category Filter
![Category](docs/screenshots/category.png)

### Admin Dashboard
![Dashboard](docs/screenshots/dashboard.png)

### Create Post
![Create Post](docs/screenshots/create-post.png)

### Login
![Login](docs/screenshots/login.png)

---

## 🏗️ Architecture

This project follows **Onion Architecture** with clear separation of concerns:

```
┌─────────────────────────────────────────────────┐
│                  API Layer                       │
│          Controllers, Middleware, Program.cs     │
├─────────────────────────────────────────────────┤
│             Infrastructure Layer                 │
│    EF Entities, DbContext, Repositories,         │
│    EntityMapper, JwtService                      │
├─────────────────────────────────────────────────┤
│              Application Layer                   │
│    Services, DTOs, AutoMapper, Validators        │
├─────────────────────────────────────────────────┤
│               Domain Layer (Core)                │
│       Pure Models, Enums, Repo Interfaces        │
│              ZERO dependencies                   │
└─────────────────────────────────────────────────┘
```

**Key architectural decisions:**
- Domain Models are separate from EF Entities (true layer independence)
- Repository interfaces defined in Domain, implemented in Infrastructure
- Service interfaces and implementations in Application layer
- Manual EntityMapper in Infrastructure for Entity ↔ Domain Model conversion
- AutoMapper in Application for Domain Model ↔ DTO conversion
- Base API service pattern in Angular for DRY HTTP calls

---

## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| .NET 8 | Web API framework |
| Entity Framework Core | ORM (Code First) |
| SQL Server | Database (Azure SQL) |
| JWT + Refresh Tokens | Authentication |
| AutoMapper | Object mapping (Application layer) |
| FluentValidation | Request validation |
| BCrypt | Password hashing |
| Swagger / OpenAPI | API documentation |

### Frontend
| Technology | Purpose |
|---|---|
| Angular 17+ | SPA framework (Standalone Components) |
| TypeScript | Type-safe JavaScript |
| Bootstrap 5.3 | UI framework (Dark/Light mode) |
| Bootstrap Icons | Icon library |
| RxJS | Reactive programming (Observables) |

### Cloud & DevOps
| Technology | Purpose |
|---|---|
| Azure App Service | API hosting (Free F1) |
| Azure SQL Database | Cloud database (Basic tier) |
| Azure Static Web Apps | Frontend hosting (Free) |
| GitHub Actions | CI/CD pipeline |

---

## ✨ Features

### Public
- 🎬 Browse movie, anime & series reviews
- 🔥 Trending page with rating-based sorting
- 🏷️ Filter posts by category with browse pills
- 🌙 Dark / Light mode toggle (persists in localStorage)
- 📱 Fully responsive (mobile, tablet, desktop)
- 🔗 SEO-friendly URLs with slugs (`/post/attack-on-titan-review`)

### Admin
- 🔐 JWT authentication with refresh tokens
- 📝 Create, edit, and delete blog posts
- 📊 Admin dashboard with post stats and filters
- 🖼️ Cover image preview in post form
- 📋 Draft / Publish toggle system
- ✅ Form validation with real-time feedback

### API
- 🏛️ Onion Architecture (4 layers)
- 🔄 Full CRUD operations
- 📄 Pagination support
- 🔍 Filter by category and media type
- 🛡️ Role-based authorization
- ⚠️ Global exception handling with proper HTTP status codes
- 📖 Swagger documentation with JWT support

---

## 📁 Project Structure

```
ReviewVault/
├── ReviewVault.Domain/              ← Core (zero dependencies)
│   ├── Models/                      Pure business models
│   ├── Enums/                       Rating enum
│   └── Interfaces/                  Repository contracts
│
├── ReviewVault.Application/         ← Business Logic
│   ├── DTOs/
│   │   ├── RequestDTOs/             Incoming data shapes
│   │   └── ResponseDTOs/            Outgoing data shapes
│   ├── Interfaces/                  Service contracts + IJwtService
│   ├── Services/                    Business logic implementation
│   ├── Mappings/                    AutoMapper profiles
│   └── Validators/                  FluentValidation rules
│
├── ReviewVault.Infrastructure/      ← Data Access & External Services
│   ├── Entities/                    EF Core entities
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── Configurations/          Fluent API configs per entity
│   ├── Mappings/                    EntityMapper (Entity ↔ Domain)
│   ├── Repositories/               Repository implementations
│   └── ExternalServices/           JwtService implementation
│
├── ReviewVault.Api/                 ← Entry Point
│   ├── Controllers/                 API endpoints
│   ├── Middleware/                  Exception handler, Validation filter
│   └── Program.cs                   DI wiring, pipeline config
│
└── ReviewVault.Client/              ← Angular Frontend
    └── src/app/
        ├── core/
        │   ├── models/              TypeScript interfaces
        │   ├── services/            API services + base service
        │   ├── interceptors/        JWT auto-attach
        │   └── guards/              Route protection
        ├── shared/
        │   ├── navbar/              Navigation + user menu
        │   ├── footer/
        │   ├── post-card/           Reusable card component
        │   └── theme-toggle/        Dark/Light switch
        └── pages/
            ├── home/                Cards grid + hero
            ├── post-detail/         Full article view
            ├── category/            Filter by category
            ├── trending/            Top rated posts
            ├── login/               Admin login
            └── admin/
                ├── dashboard/       Post management
                ├── create-post/     New post form
                └── edit-post/       Edit existing post
```

---

## 🔐 Authentication Flow

```
1. Login with email + password
2. Server validates → returns Access Token (30 min) + Refresh Token (7 days)
3. Access Token sent with every API request via HTTP Interceptor
4. Token expires → Interceptor detects 401 → auto-logout
5. Refresh Token can generate new Access Token without re-login
6. Refresh Token stored in database → revocable by admin
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- Angular CLI (`npm install -g @angular/cli`)
- SQL Server (LocalDB or Express)

### Backend Setup

```bash
# Clone the repo
git clone https://github.com/ammiitt/ReviewVault.git
cd ReviewVault

# Update connection string in ReviewVault.Api/appsettings.Development.json
# "Server=YOUR_SERVER;Database=ReviewVaultDb;Trusted_Connection=true;TrustServerCertificate=true;"

# Run migrations
dotnet ef database update --project ReviewVault.Infrastructure --startup-project ReviewVault.Api

# Run the API
cd ReviewVault.Api
dotnet run
```

API runs at: `https://localhost:7048/swagger`

### Frontend Setup

```bash
cd ReviewVault.Client

# Install dependencies
npm install

# Run dev server
ng serve
```

App runs at: `http://localhost:4200`

---

## 🗄️ Database Schema

```
┌─────────────┐     ┌──────────────────┐     ┌───────────────┐
│ Users        │     │ Posts             │     │ MediaTypes     │
├─────────────┤     ├──────────────────┤     ├───────────────┤
│ Id (PK)     │──┐  │ Id (PK)          │  ┌──│ Id (PK)       │
│ Username    │  │  │ Title            │  │  │ Name          │
│ Email       │  │  │ Slug             │  │  │ Description   │
│ PasswordHash│  └─→│ AuthorId (FK)    │  │  │ IsActive      │
│ Role        │     │ MediaTypeId (FK) │←─┘  └───────────────┘
│ Bio         │     │ Body             │
└─────────────┘     │ Rating           │     ┌───────────────┐
                    │ IsPublished      │     │ Categories     │
┌──────────────┐    │ PublishedAt      │     ├───────────────┤
│ RefreshTokens│    └────────┬─────────┘     │ Id (PK)       │
├──────────────┤             │               │ Name          │
│ Id (PK)      │             │               └───────┬───────┘
│ Token        │    ┌────────┴─────────┐             │
│ UserId (FK)  │    │ PostCategories   │             │
│ ExpiresAt    │    │ (Join Table)     │─────────────┘
│ RevokedAt    │    │ PostId (FK)      │
└──────────────┘    │ CategoryId (FK)  │
                    └──────────────────┘
```

---

## 🎯 Design Patterns Used

| Pattern | Where |
|---|---|
| Onion Architecture | Overall project structure |
| Repository Pattern | Data access abstraction |
| Dependency Injection | All layers (constructor injection) |
| DTO Pattern | Request/Response separation |
| Mapper Pattern | EntityMapper (manual) + AutoMapper |
| Strategy Pattern | IJwtService (swappable implementation) |
| Middleware Pattern | Global exception handler, Validation filter |
| Base Service Pattern | Angular ApiBaseService for DRY HTTP calls |
| Observer Pattern | BehaviorSubject for reactive auth/theme state |

---

## 📦 API Endpoints

### Auth
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/Auth/register` | Register new user | ❌ |
| POST | `/api/Auth/login` | Login | ❌ |
| POST | `/api/Auth/refresh` | Refresh access token | ❌ |
| POST | `/api/Auth/revoke` | Revoke refresh token | ❌ |

### Posts
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Post` | Get all published posts | ❌ |
| GET | `/api/Post/{slug}` | Get post by slug | ❌ |
| GET | `/api/Post/id/{id}` | Get post by ID | ❌ |
| GET | `/api/Post/category/{id}` | Get posts by category | ❌ |
| POST | `/api/Post` | Create new post | ✅ Admin |
| PUT | `/api/Post/{id}` | Update post | ✅ Admin |
| DELETE | `/api/Post/{id}` | Delete post | ✅ Admin |

### Categories
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Category` | Get all categories | ❌ |
| POST | `/api/Category` | Create category | ✅ Admin |

### Media Types
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/MediaType` | Get all media types | ❌ |
| POST | `/api/MediaType` | Create media type | ✅ Admin |
| PUT | `/api/MediaType/{id}` | Update media type | ✅ Admin |
| DELETE | `/api/MediaType/{id}` | Deactivate media type | ✅ Admin |

---

## 🔮 Future Roadmap

- [ ] Search functionality (by title, keyword)
- [ ] Rich text editor for posts
- [ ] Image upload to Azure Blob Storage
- [ ] User registration (multi-author support)
- [ ] Comments system
- [ ] Like / Bookmark posts
- [ ] Email notifications for subscribers
- [ ] About page
- [ ] Custom domain integration
- [ ] Unit and integration tests
- [ ] Redis caching for popular posts

---

## 👨‍💻 Author

**Amit** — Full-Stack Developer

- 🛠️ Tech: C#, .NET, Angular, TypeScript, SQL Server, Azure
- 🎬 Interests: Movies, Anime, K-Drama
- 🔗 GitHub: [@ammiitt](https://github.com/ammiitt)

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
