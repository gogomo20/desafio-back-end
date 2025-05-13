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
            new Permission { Id = 5, Name = "DELETE_USER", Description = "Delete user", PermissionGroupId = 1 },
            new Permission { Id = 6, Name = "ADD_BALANCE", Description = "Add balance", PermissionGroupId = 2 },
            new Permission { Id = 7, Name = "REMOVE_BALANCE", Description = "Remove balance", PermissionGroupId = 2 },
            new Permission { Id = 8, Name = "GET_BALANCE", Description = "Get balance", PermissionGroupId = 2 },
            new Permission { Id = 9, Name = "CREATE_TRANSFERENCE", Description = "Create transference", PermissionGroupId = 2 },
            new Permission { Id = 10, Name = "GET_TRANSFERENCE", Description = "Get transference", PermissionGroupId = 2 },
            new Permission { Id = 11, Name = "LIST_TRANSFERENCE", Description = "List transference", PermissionGroupId = 2 }
        );
    }
}