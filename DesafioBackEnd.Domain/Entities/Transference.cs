using StockManager.Domain.Basis;

namespace StockManager.Domain.Entities;

public class Transference : BaseTable
{
    public required long SenderId { get; set; }
    public required long ReceiverId { get; set; }
    public decimal Value { get; set; }
    public virtual User Reciever { get; set; }
    public virtual User Sender { get; set; }
}