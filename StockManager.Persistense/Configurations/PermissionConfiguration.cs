using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Persistense.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(255);

        builder.HasOne(x => x.PermissionGroup).WithMany().HasForeignKey(x => x.PermissionGroupId).OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            new Permission { Id = 1, Name = "CREATE_USER", Description = "Create user", PermissionGroupId = 1 },
            new Permission { Id = 2, Name = "UPDATE_USER", Description = "Update user", PermissionGroupId = 1 },
            new Permission { Id = 3, Name = "GET_USER", Description = "View user", PermissionGroupId = 1 },
            new Permission { Id = 4, Name = "LIST_USER", Description = "List user", PermissionGroupId = 1 },
            new Permission { Id = 5, Name = "DELETE_USER", Description = "Delete user", PermissionGroupId = 1 }
        );
    }
}