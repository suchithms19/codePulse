# codePulse

Lightweight blogging API and data project.

Overview
- `codePulse.API` — ASP.NET Core Web API exposing blog and category endpoints.
- `codePulse.AI.Data` — EF Core `DbContext` and migrations.
- `codePulse.AI.Models.Domain` — domain model POCOs.
- `Repositories` — repository interfaces and implementations.

Quick start
- Requirements: .NET 10 SDK, database server (e.g., SQL Server, PostgreSQL).
- Restore & build:
  - `dotnet restore`
  - `dotnet build`
- Run:
  - `dotnet run --project codePulse.API`

Configuration
- Provide a connection string in `codePulse.API` appsettings or environment variables under `ConnectionStrings:DefaultConnection`.
- Use environment-specific configuration for secrets.