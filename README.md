# 🎬 ReviewVault — Full-Stack Movie & Anime Blog Platform

A production-ready blog platform for movie, anime, and series reviews. Built with **.NET 8** and **Angular 20**, following **Onion Architecture** principles with JWT authentication, role-based access control, and deployed on **Microsoft Azure** with CI/CD.

🔗 **Live Site:** [ReviewVault](https://zealous-ocean-0cec1c710.7.azurestaticapps.net)
🔗 **API Docs:** [Swagger](https://reviewvault-api-c8cabmamg0brachb.centralindia-01.azurewebsites.net/swagger)

---

## 📸 Screenshots

### Home Page (Light Mode)
![Home Light](docs/screenshots/home-light.png)

### Home Page (Dark Mode)
![Home Dark](docs/screenshots/home-dark.png)

### Post Detail — Comments, Likes & Bookmarks
![Post Detail](docs/screenshots/post-detail.png)

### Trending — TMDB & Jikan Integration
![Trending](docs/screenshots/trending.png)

### Search
![Search](docs/screenshots/search.png)

### Admin Dashboard
![Dashboard](docs/screenshots/dashboard.png)

### Create Post
![Create Post](docs/screenshots/create-post.png)

### User Profile
![Profile](docs/screenshots/profile.png)

### Login
![Login](docs/screenshots/login.png)

### Register — Password Strength Indicators
![Register](docs/screenshots/register.png)

### About
![About](docs/screenshots/about.png)
---

## 🏗️ Architecture

This project follows **Onion Architecture** with strict separation of concerns. Inner layers define contracts, outer layers implement them — following the **Dependency Inversion Principle**.

```
┌──────────────────────────────────────────────────────────────┐
│                        API Layer                              │
│         Controllers · Middleware · Program.cs (DI Root)       │
├──────────────────────────────────────────────────────────────┤
│                   Infrastructure Layer                        │
│    EF Entities · DbContext · Configs · Repositories ·         │
│    EntityMapper · JwtService                                  │
├──────────────────────────────────────────────────────────────┤
│                    Application Layer                          │
│      Services · DTOs (Request/Response) · AutoMapper ·        │
│      FluentValidation · Service Interfaces                    │
├──────────────────────────────────────────────────────────────┤
│                   Domain Layer (Core)                         │
│         Pure Models · Enums · Repository Interfaces           │
│                    ZERO dependencies                          │
└──────────────────────────────────────────────────────────────┘
```

### Key Architectural Decisions

- **Domain Models are separate from EF Entities** — Domain layer has zero dependency on EF Core. Infrastructure handles the mapping via a static `EntityMapper`, ensuring true layer independence.
- **Two mapping strategies** — Manual `EntityMapper` (Infrastructure: Entity ↔ Domain) + AutoMapper (Application: Domain ↔ DTO). Each mapping has a clear purpose and boundary.
- **Repository interfaces in Domain**, implementations in Infrastructure — services depend on abstractions, not concrete data access.
- **Base API Service pattern in Angular** — All HTTP services extend `ApiBaseService`, centralizing URL construction, error handling, and HTTP methods (DRY principle).
- **Lookup Tables over Enums for extensible data** — MediaTypes use a database table (can add K-Drama, Manhua without code changes). Rating uses an Enum (fixed 1-5 scale, won't change).

---

## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| .NET 8 | Web API framework |
| Entity Framework Core 8 | ORM — Code First with Migrations |
| SQL Server | Relational database (Azure SQL) |
| JWT + Refresh Tokens | Stateless authentication with revocable refresh |
| BCrypt | Password hashing |
| AutoMapper | Domain ↔ DTO mapping |
| FluentValidation | Request validation with custom rules |
| Swagger / OpenAPI | Interactive API documentation |

### Frontend
| Technology | Purpose |
|---|---|
| Angular 20 | SPA framework — Standalone Components (no NgModule) |
| TypeScript | Type-safe development |
| Bootstrap 5.3 | Responsive UI with native Dark/Light mode |
| Bootstrap Icons | 2000+ free icons |
| RxJS | Reactive state management (BehaviorSubject, Observables) |
| TMDB API | Real-time movie & TV show data |
| Jikan API | Real-time anime & manga data from MyAnimeList |

### Cloud & DevOps
| Service | Purpose |
|---|---|
| Azure App Service (Free F1) | API hosting |
| Azure SQL Database (Basic) | Cloud database |
| Azure Static Web Apps (Free) | Frontend hosting with auto-SSL |
| GitHub Actions | CI/CD — auto-deploys on push to main |

---

## ✨ Features

### 🌐 Public (No Login Required)
- Browse movie, anime & series reviews in a Netflix-style card grid
- Read full articles with cover images, ratings, and reading time
- Filter posts by category with interactive browse pills
- View trending content from TMDB (Movies, TV, K-Drama) and Jikan (Anime, Manga)
- Search posts by title, body, or summary with paginated results
- Dark / Light mode toggle (persists across sessions)
- SEO-friendly URLs with slugs (`/post/attack-on-titan-review`)
- Fully responsive design (mobile, tablet, desktop)

### 👤 User (Login Required)
- Register with password strength validation (live indicators)
- Login with JWT authentication
- Like / Unlike posts (toggle with count)
- Bookmark / Unbookmark posts for later reading
- Comment on posts with real-time updates
- Delete own comments
- View profile with stats (likes, bookmarks, comments)
- Change password from profile page

### 👑 Admin (Admin Role Only)
- Full CRUD for blog posts with rich form (categories, ratings, cover image preview)
- Dashboard with stats cards (total, published, drafts) and filterable posts table
- Edit and delete any post
- Delete any user's comment (moderation)
- Create new categories
- Access via protected routes (adminGuard)

### 🔌 External API Integration
- **TMDB API** — Trending movies, TV shows, and K-Dramas with poster images and ratings
- **Jikan API** — Top anime and manga from MyAnimeList with episode counts and scores
- Dropdown selector to switch between your reviews and external data
- Auth interceptor intelligently skips JWT for external API calls

---

## 📁 Project Structure

```
ReviewVault/
├── ReviewVault.Domain/                     ← Core (zero dependencies)
│   ├── Models/                             Pure C# business models
│   │   ├── User.cs
│   │   ├── Post.cs
│   │   ├── Comment.cs
│   │   ├── Like.cs
│   │   └── Bookmark.cs
│   ├── Enums/                              Rating (1-5)
│   └── Interfaces/                         Repository contracts
│       ├── IPostRepository.cs
│       ├── IUserRepository.cs
│       ├── ICommentRepository.cs
│       ├── ILikeRepository.cs
│       └── IBookmarkRepository.cs
│
├── ReviewVault.Application/                ← Business Logic
│   ├── DTOs/
│   │   ├── RequestDTOs/                    CreatePostRequest, LoginRequest, etc.
│   │   └── ResponseDTOs/                   PostResponse, AuthResponse, LikeInfo, etc.
│   ├── Interfaces/                         Service contracts
│   │   ├── IPostService.cs
│   │   ├── IAuthService.cs
│   │   ├── ICommentService.cs
│   │   ├── ILikeService.cs
│   │   ├── IBookmarkService.cs
│   │   ├── IUserService.cs
│   │   └── IJwtService.cs
│   ├── Services/                           Business logic implementation
│   ├── Mappings/                           AutoMapper profiles
│   └── Validators/                         FluentValidation rules
│
├── ReviewVault.Infrastructure/             ← Data Access & External Services
│   ├── Entities/                           EF Core entities with navigation props
│   ├── Data/
│   │   ├── AppDbContext.cs                 DbContext with DbSets
│   │   └── Configurations/                 IEntityTypeConfiguration per entity
│   ├── Mappings/EntityMapper.cs            Static extension methods (Entity ↔ Domain)
│   ├── Repositories/                       All repository implementations
│   └── ExternalServices/JwtService.cs      JWT token generation
│
├── ReviewVault.Api/                        ← Entry Point & DI Root
│   ├── Controllers/                        7 API controllers
│   │   ├── AuthController.cs               Register, Login, Refresh, Revoke
│   │   ├── PostController.cs               CRUD + Search + Filter
│   │   ├── CategoryController.cs
│   │   ├── MediaTypeController.cs
│   │   ├── CommentController.cs
│   │   ├── LikeController.cs
│   │   ├── BookmarkController.cs
│   │   └── UserController.cs               Profile, Change Password
│   ├── Middleware/
│   │   ├── GlobalExceptionMiddleware.cs    Catches all exceptions
│   │   └── ValidationFilter.cs             Auto-validates with FluentValidation
│   └── Program.cs                          DI wiring, JWT config, CORS, pipeline
│
└── ReviewVault.Client/                     ← Angular 20 Frontend
    └── src/app/
        ├── core/
        │   ├── models/                     TypeScript interfaces (Post, Auth, Comment, Like, etc.)
        │   ├── services/
        │   │   ├── api-base.service.ts     Base HTTP service (DRY pattern)
        │   │   ├── auth.service.ts         Login, register, token management
        │   │   ├── post.service.ts         Posts CRUD + search
        │   │   ├── comment.service.ts      Comments CRUD
        │   │   ├── like.service.ts         Like toggle
        │   │   ├── bookmark.service.ts     Bookmark toggle + list
        │   │   ├── user.service.ts         Profile + password change
        │   │   ├── tmdb.service.ts         External movie/TV API
        │   │   ├── jikan.service.ts        External anime/manga API
        │   │   ├── theme.service.ts        Dark/Light mode
        │   │   └── toast.service.ts        Custom notification system
        │   ├── interceptors/
        │   │   └── auth.interceptor.ts     Auto JWT + skip external APIs
        │   └── guards/
        │       └── auth.guard.ts           authGuard + adminGuard
        ├── shared/
        │   ├── navbar/                     Responsive nav + role-based menu
        │   ├── footer/
        │   ├── post-card/                  Reusable card with clickable categories
        │   ├── pagination/                 Reusable numbered pagination
        │   ├── theme-toggle/               Dark/Light switch
        │   └── toast/                      Custom Bootstrap toast notifications
        └── pages/
            ├── home/                       Hero banner + cards grid + pagination
            ├── post-detail/                Article + comments + like/bookmark
            ├── category/                   Filter by category + browse pills
            ├── trending/                   TMDB + Jikan + your reviews
            ├── search/                     Full-text search with results grid
            ├── about/                      Platform info + tech stack
            ├── login/                      Reactive form with validation
            ├── register/                   Password strength indicators
            ├── profile/                    Stats, bookmarks, change password
            ├── not-found/                  Custom 404 page
            └── admin/
                ├── dashboard/              Stats cards + posts table + filters
                ├── create-post/            Full form with live image preview
                └── edit-post/              Pre-filled form with category checks
```

---

## 🔐 Authentication & Authorization

```
REGISTRATION:
  POST /api/Auth/register        → Creates "User" role (public)
  POST /api/Auth/register-admin  → Creates "Admin" role (protected, Admin only)

LOGIN FLOW:
  1. User sends email + password
  2. Server validates → returns Access Token (30 min) + Refresh Token (7 days)
  3. Angular stores in localStorage + notifies BehaviorSubject subscribers
  4. HTTP Interceptor auto-attaches JWT to every API request
  5. Token expires → Interceptor detects 401 → auto-logout
  6. App startup → checks token expiry → auto-refreshes if possible

ROLE-BASED ACCESS:
  ┌────────────┬──────────────────────────────────────────┐
  │ Role       │ Can Access                               │
  ├────────────┼──────────────────────────────────────────┤
  │ Visitor    │ Browse, read, search, view trending      │
  │ User       │ + Like, bookmark, comment, profile       │
  │ Admin      │ + Dashboard, CRUD posts, delete comments │
  └────────────┴──────────────────────────────────────────┘

ANGULAR GUARDS:
  authGuard   → any logged-in user (profile, like, comment)
  adminGuard  → Admin only (dashboard, create/edit posts)
```

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
│ Bio         │     │ Body, Summary    │
└──────┬──────┘     │ Rating           │     ┌───────────────┐
       │            │ IsPublished      │     │ Categories     │
       │            └───────┬──────────┘     ├───────────────┤
       │                    │                │ Id (PK)       │
       │           ┌────────┴─────────┐      │ Name          │
       │           │ PostCategories   │      └───────┬───────┘
       │           │ (Join Table)     │──────────────┘
       │           └──────────────────┘
       │
       ├──→ ┌──────────────┐
       │    │ Comments      │
       │    ├──────────────┤
       │    │ UserId (FK)  │──→ Posts.Id
       │    │ PostId (FK)  │
       │    │ Body         │
       │    └──────────────┘
       │
       ├──→ ┌──────────────┐
       │    │ Likes         │
       │    ├──────────────┤
       │    │ UserId (FK)  │──→ Posts.Id
       │    │ PostId (FK)  │
       │    │ UNIQUE(User+Post) ← one like per user per post
       │    └──────────────┘
       │
       ├──→ ┌──────────────┐
       │    │ Bookmarks     │
       │    ├──────────────┤
       │    │ UserId (FK)  │──→ Posts.Id
       │    │ PostId (FK)  │
       │    │ UNIQUE(User+Post)
       │    └──────────────┘
       │
       └──→ ┌──────────────┐
            │ RefreshTokens │
            ├──────────────┤
            │ UserId (FK)  │
            │ Token         │
            │ ExpiresAt     │
            │ RevokedAt     │
            └──────────────┘
```

---

## 🎯 Design Patterns

| Pattern | Where | Why |
|---|---|---|
| Onion Architecture | Project structure | Dependency flows inward, core has zero dependencies |
| Repository Pattern | Data access | Abstracts EF Core behind interfaces, swappable |
| Dependency Injection | All layers | Loose coupling, testable, follows DIP |
| DTO Pattern | API boundaries | Request/Response separation, never expose entities |
| Mapper Pattern | Entity↔Domain, Domain↔DTO | Two-stage mapping for clean boundaries |
| Strategy Pattern | IJwtService | Can swap JWT for any token strategy |
| Middleware Pattern | Exception handler, Validation | Pipeline processing, cross-cutting concerns |
| Base Service Pattern | Angular ApiBaseService | DRY HTTP calls, centralized error handling |
| Observer Pattern | BehaviorSubject | Reactive auth/theme state across components |
| Toggle Pattern | Likes, Bookmarks | Single endpoint handles create + delete |

---

## 📦 API Endpoints

### Auth
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/Auth/register` | Register as User | Public |
| POST | `/api/Auth/register-admin` | Register as Admin | Admin |
| POST | `/api/Auth/login` | Login → JWT tokens | Public |
| POST | `/api/Auth/refresh` | Refresh access token | Public |
| POST | `/api/Auth/revoke` | Revoke refresh token | Public |

### Posts
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Post` | All published posts (paginated) | Public |
| GET | `/api/Post/{slug}` | Post by URL slug | Public |
| GET | `/api/Post/id/{id}` | Post by ID | Public |
| GET | `/api/Post/category/{id}` | Posts by category | Public |
| GET | `/api/Post/search?q=` | Search posts | Public |
| POST | `/api/Post` | Create post | Admin |
| PUT | `/api/Post/{id}` | Update post | Admin |
| DELETE | `/api/Post/{id}` | Delete post | Admin |

### Comments
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Comment/post/{postId}` | Comments for a post | Public |
| GET | `/api/Comment/post/{postId}/count` | Comment count | Public |
| POST | `/api/Comment` | Create comment | User |
| DELETE | `/api/Comment/{id}` | Delete comment (own or admin) | User |

### Likes
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Like/post/{postId}` | Like count + user status | Public |
| POST | `/api/Like/toggle/{postId}` | Like/Unlike toggle | User |

### Bookmarks
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Bookmark` | User's bookmarks | User |
| GET | `/api/Bookmark/check/{postId}` | Is bookmarked? | User |
| POST | `/api/Bookmark/toggle/{postId}` | Bookmark/Remove toggle | User |

### User
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/User/profile` | Get profile + stats | User |
| PUT | `/api/User/profile` | Update username/bio | User |
| PUT | `/api/User/change-password` | Change password | User |

### Categories & Media Types
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Category` | All categories | Public |
| POST | `/api/Category` | Create category | Admin |
| GET | `/api/MediaType` | All media types | Public |

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- Angular CLI (`npm install -g @angular/cli`)
- SQL Server (LocalDB or Express)

### Backend Setup

```bash
git clone https://github.com/ammiitt/ReviewVault.git
cd ReviewVault

# Update connection string in ReviewVault.Api/appsettings.Development.json

# Run migrations
dotnet ef database update --project ReviewVault.Infrastructure --startup-project ReviewVault.Api

# Run the API
cd ReviewVault.Api
dotnet run
```

API available at: `https://localhost:7048/swagger`

### Frontend Setup

```bash
cd ReviewVault.Client
npm install
ng serve
```

App available at: `http://localhost:4200`

### Environment Configuration

```
Backend:
  appsettings.json                  → Production (Azure SQL)
  appsettings.Development.json      → Local (SQL Server Express)
  Azure Environment Variables       → Jwt__Secret, Jwt__Issuer, Jwt__Audience

Frontend:
  environments/environment.ts               → Production API URL
  environments/environment.development.ts   → Local API URL
  angular.json fileReplacements             → Auto-swaps on ng build
```

---

## ☁️ Deployment Architecture

```
┌─────────────────────────────────────────────────────┐
│                    GitHub (Source)                    │
│              github.com/ammiitt/ReviewVault          │
└──────────────┬──────────────────────┬────────────────┘
               │ push to main         │ push to main
               ▼                      ▼
┌──────────────────────┐  ┌────────────────────────────┐
│   Azure App Service  │  │  Azure Static Web Apps     │
│   (API - .NET 8)     │  │  (Angular - GitHub Actions)│
│   Free F1 tier       │  │  Free tier + auto SSL      │
└──────────┬───────────┘  └────────────────────────────┘
           │
           ▼
┌──────────────────────┐
│   Azure SQL Database │
│   Basic tier (5 DTU) │
│   Central India      │
└──────────────────────┘
```

---

## 🔮 Future Roadmap

- [ ] Rich text editor for posts (Quill/TinyMCE)
- [ ] Image upload to Azure Blob Storage
- [ ] Email notifications for subscribers
- [ ] Social login (Google OAuth)
- [ ] Admin user management panel
- [ ] Related posts suggestions
- [ ] RSS feed
- [ ] PWA support (installable on mobile)
- [ ] Redis caching for popular posts
- [ ] Unit and integration tests

---

## 👨‍💻 Author

**Amit** — Full-Stack .NET & Angular Developer

- 🛠️ Stack: C#, .NET, Angular, TypeScript, SQL Server, Azure
- 🎬 Passions: Movies, Anime, K-Drama, Manga
- 🔗 GitHub: [@ammiitt](https://github.com/ammiitt)

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
