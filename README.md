# TradingPlatform

A production-grade paper trading platform built with ASP.NET Core MVC, using Clean Architecture, CQRS, and a real price-time-priority matching engine.

## Status

🚧 Early development — Phase 0 (project scaffolding) in progress.

## Architecture

- **TradingPlatform.Domain** — core entities (Order, Trade, Wallet, Instrument), no external dependencies
- **TradingPlatform.Application** — CQRS commands/queries (MediatR), business logic, depends only on Domain
- **TradingPlatform.Infrastructure** — EF Core, Redis, external price feed clients; implements interfaces defined in Application
- **TradingPlatform.Web** — ASP.NET Core MVC frontend + SignalR hubs
- **TradingPlatform.Api** — REST API layer

## Tech Stack

ASP.NET Core MVC/API · EF Core · SQL Server · Redis · SignalR · MediatR · FluentValidation · xUnit · Docker

## Getting Started

```powershell
dotnet restore
dotnet build
```

More setup instructions (Docker, database, running locally) coming in later phases.

## Branching

We use GitHub Flow: `main` is always deployable. Create a feature branch (`feature/xyz`), open a PR, get it reviewed, then merge.
