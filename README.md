# EfCoreRelationshipDemo

A .NET 9 console application demonstrating the three core Entity Framework Core relationship patterns with SQL Server.

## Relationship Patterns

| Pattern | Entities | Strategy |
|---------|----------|----------|
| **One-to-One** | `Person` ↔ `Passport` | Shared primary key (`PersonId`) |
| **One-to-Many** | `Blog` → `Posts` | Foreign key (`BlogId`) |
| **Many-to-Many** | `Student` ↔ `Course` | Explicit join entity (`Enrollment`) with payload (`EnrolledOn`) |

## Project Structure

```
├── Data/
│   └── AppDbContext.cs              # DbContext with Fluent API configuration
├── Models/
│   ├── OneToOne/                    # Person, Passport
│   ├── OneToMany/                   # Blog, Post
│   └── ManyToMany/                  # Student, Course, Enrollment
├── Migrations/                      # EF Core migrations
├── Program.cs                       # Entry point with host builder
├── appsettings.json                 # Configuration (credentials via User Secrets)
├── Dockerfile                       # Multi-stage Docker build
├── docker-compose.yml               # SQL Server + app orchestration
└── EfCoreRelationshipDemo.csproj    # .NET 9 project file
```

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (local or Docker)

## Getting Started

### Local Development

1. Set up the connection string using User Secrets:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=EfCore_Db;User Id=sa;Password=YourPassword;TrustServerCertificate=true;"
```

2. Run the application:

```bash
dotnet run
```

### Docker

1. Build and run with Docker Compose:

```bash
docker compose up --build
```

This starts SQL Server 2022 and the application. Migrations are applied automatically on startup with retry logic.

## Key Design Decisions

- **Encapsulation**: All entity properties use `private set` with explicit constructors
- **Shared Primary Key**: `Passport.PersonId` serves as both PK and FK for true one-to-one
- **Explicit Join Entity**: `Enrollment` carries payload data (`EnrolledOn`) for many-to-many
- **Auto-Migration**: The app applies pending migrations on startup with retry logic for Docker environments

## Branches

| Branch | Description |
|--------|-------------|
| `master` | Base implementation with all three relationship patterns |
| `fix/security-and-domain-model` | User Secrets, shared PK Passport, public constructors |
| `fix/db-context-and-schema` | DbContext cleanup, string length constraints, unique index |
| `feat/docker-support` | Dockerfile, docker-compose, auto-migration with retry |

## License

[MIT](LICENSE)
