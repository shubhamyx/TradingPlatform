# Coding Standards & Conventions

> Living document — update this as the team makes new decisions. Every dev should read this before their first PR.

---

## Git & Commits

**Branch naming:** `type/short-description`, e.g. `feature/order-entity`, `fix/wallet-race-condition`, `chore/update-packages`

**Commit message prefixes** (Conventional Commits style):
| Prefix | Use for |
|---|---|
| `feat:` | New feature or capability |
| `fix:` | Bug fix |
| `chore:` | Tooling, config, dependencies, non-code changes |
| `docs:` | Documentation only |
| `refactor:` | Code change that doesn't add a feature or fix a bug |
| `test:` | Adding or fixing tests |
| `ci:` | CI/CD pipeline changes |

Format: `type: short description in present tense` (e.g. `feat: add order matching engine`, not `feat: added` or `feat: adding`).

**Pull Requests:**
- Every change goes through a PR into `main` — no direct pushes to `main`, ever
- **At least 1 teammate review required before merge** (once team is 2-3 devs; solo work can self-merge after CI passes)
- CI build check must pass (green ✅) before merge
- Delete the branch after merging
- PR description should say *what* changed and *why*, not just repeat the commit message

---

## C# Conventions

**Naming:**
| Element | Convention | Example |
|---|---|---|
| Classes, Methods, Properties | PascalCase | `OrderService`, `PlaceOrder()`, `TotalValue` |
| Private fields | camelCase, no underscore prefix | `orderRepository`, `logger` |
| Local variables, parameters | camelCase | `orderId`, `userWallet` |
| Interfaces | `I` prefix + PascalCase | `IOrderRepository` |
| Constants | PascalCase | `MaxOrderQuantity` |

**Namespaces:** Block-scoped for now (`namespace X { }`), matches what's already in the codebase. May revisit as a team-wide switch to file-scoped later.

**File organization:**
- One public class per file, file name matches class name
- Folder structure within each project should mirror logical grouping (e.g. `Entities/`, `Persistence/`, `Commands/`, `Queries/`)

---

## Architecture Rules (non-negotiable)

Dependencies point inward only:
```
Web / Api  →  Infrastructure  →  Application  →  Domain
```
- **Domain** references nothing else in the solution
- **Application** references only Domain — never Infrastructure, never Web/Api
- **Infrastructure** implements interfaces defined in Application (e.g. `IOrderRepository`) — it does not define business logic
- **Web / Api** wire everything together via dependency injection in `Program.cs`; business logic belongs in Application via MediatR commands/queries, not in controllers

If a PR adds a reference that violates this direction, it should be rejected in review, no exceptions.

---

## Secrets & Configuration

- **Never commit real secrets, passwords, or connection strings to `appsettings.json`** — use `dotnet user-secrets` for local dev
- Production secrets are handled separately (environment variables / secret manager) — never hardcoded, never committed
- If you accidentally commit a secret, don't just delete it in a new commit — flag it immediately, since it stays in Git history until rewritten

---

## Environment Gotchas (learned the hard way — check here before debugging for an hour)

- **OneDrive-synced folders corrupt build output.** Keep the repo outside any OneDrive-synced path (e.g. `C:\Dev\...`, not `C:\Users\<you>\...` if that's synced).
- **Docker Desktop requires WSL2** to be properly installed on Windows (`wsl --install --no-distribution` + reboot) — if `docker ps` fails with a pipe/daemon error, check Docker Desktop is actually running first, then check WSL2 status.
- **Docker Desktop doesn't always survive a restart or VS update** — if you get a "cannot connect to Docker daemon" error, just reopen Docker Desktop and wait for "Running," then `docker compose up -d` again (safe to re-run).
- **`dotnet ef` commands need two things:** `--project` (where the DbContext lives — Infrastructure) and `--startup-project` (the runnable app with config/connection string — Web).
- **`Microsoft.EntityFrameworkCore.Design`** needs to be installed in both Infrastructure (build-time) and the startup project used by `dotnet ef` tooling (Web).
- **PowerShell vs cmd.exe matters** — cmd doesn't understand PowerShell cmdlets like `Get-ChildItem`. Confirm your prompt starts with `PS` before running PowerShell-specific commands.

---

## Building & Running Locally

```powershell
# 1. Clone the repo
git clone https://github.com/shubhamyx/TradingPlatform.git
cd TradingPlatform

# 2. Restore local secrets (connection string)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=TradingPlatformDb;User Id=sa;Password=<ask team for password>;TrustServerCertificate=True" --project src/TradingPlatform.Web

# 3. Start SQL Server + Redis containers
docker compose up -d

# 4. Apply database migrations
dotnet ef database update --project src/TradingPlatform.Infrastructure --startup-project src/TradingPlatform.Web

# 5. Build
dotnet build
```
