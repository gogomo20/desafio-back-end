using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Aplication.Utils;
using StockManager.Domain.Entities;

namespace StockManager.Persistense.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.UserName).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.UserName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
        
        builder.HasData(
            new User
            {
                Id = 1,
                Name = "admin",
                UserName = "admin",
                Email = "admin@admin",
                Password = StringUtils.GetBcryptHash("a123457z")
            });
    }
}