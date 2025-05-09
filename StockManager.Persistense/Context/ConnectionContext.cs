using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StockManager.Domain.Basis;
using StockManager.Domain.Entities;
using StockManager.Persistense.Configurations;


namespace StockManager.Persistense.Context;

public class ConnectionContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ConnectionContext(DbContextOptions<ConnectionContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionGroup> PermissionGroups { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionGroupConfiguration());
    }
    #region AuditChanges
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var userId = long.Parse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value ?? "0");
        foreach (var entry in ChangeTracker.Entries<BaseTable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = userId;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
    }
    #endregion
}