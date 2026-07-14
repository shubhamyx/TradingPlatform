using Microsoft.EntityFrameworkCore;
using TradingPlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TradingPlatform.Infrastructure.Identity;

namespace TradingPlatform.Infrastructure.Persistence;

public class TradingPlatformDbContext : IdentityDbContext<ApplicationUser>
{
    public TradingPlatformDbContext(DbContextOptions<TradingPlatformDbContext> options)
        : base(options)
    {  
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Trade> Trades => Set<Trade>();
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<PriceTick> PriceTicks => Set<PriceTick>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TradingPlatformDbContext).Assembly);
    }
}