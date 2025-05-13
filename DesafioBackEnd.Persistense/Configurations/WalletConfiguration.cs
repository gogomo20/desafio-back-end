using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Persistense.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithOne().HasForeignKey<Wallet>(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasData(new Wallet
        {
            Id = 1,
            UserId = 1,
            Balance = 0,
        });
    }
}