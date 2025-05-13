using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Persistense.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<ProfileEntity>
{
    public void Configure(EntityTypeBuilder<ProfileEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(255);
        
        builder.HasData(new ProfileEntity
        {
            Id = 1,
            Name = "Admin",
            Description = "Admin profile",
        });
        
        builder.HasMany(x => x.Permissions).WithMany().UsingEntity<ProfilePermissions>(x =>
        {
            x.HasOne<Permission>()
                .WithMany()
                .HasForeignKey(y => y.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);
            x.HasOne<ProfileEntity>()
                .WithMany()
                .HasForeignKey(y => y.ProfileEntityId)
                .OnDelete(DeleteBehavior.NoAction);
            x.HasData(
                new ProfilePermissions { Id = 1, ProfileEntityId = 1, PermissionId = 1 },
                new ProfilePermissions { Id = 2, ProfileEntityId = 1, PermissionId = 2 },
                new ProfilePermissions { Id = 3, ProfileEntityId = 1, PermissionId = 3 },
                new ProfilePermissions { Id = 4, ProfileEntityId = 1, PermissionId = 4 },
                new ProfilePermissions { Id = 5, ProfileEntityId = 1, PermissionId = 5 },
                new ProfilePermissions { Id = 6, ProfileEntityId = 1, PermissionId = 6 },
                new ProfilePermissions { Id = 7, ProfileEntityId = 1, PermissionId = 7 },
                new ProfilePermissions { Id = 8, ProfileEntityId = 1, PermissionId = 8 },
                new ProfilePermissions { Id = 9, ProfileEntityId = 1, PermissionId = 9 },
                new ProfilePermissions { Id = 10, ProfileEntityId = 1, PermissionId = 10 },
                new ProfilePermissions { Id = 11, ProfileEntityId = 1, PermissionId = 11 }
            );
        });
        
    }
}