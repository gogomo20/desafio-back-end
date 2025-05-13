using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManager.Domain.Entities;

namespace StockManager.Persistense.Configurations;

public class TransferenceConfiguration : IEntityTypeConfiguration<Transference>
{
    public void Configure(EntityTypeBuilder<Transference> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Sender).WithMany().HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Reciever).WithMany().HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.NoAction);
    }
}