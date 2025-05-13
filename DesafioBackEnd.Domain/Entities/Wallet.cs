using StockManager.Domain.Basis;

namespace StockManager.Domain.Entities;

public class Wallet : BaseTable
{
    public long UserId { get; set; }
    public decimal Balance { get; set; }
    public virtual User User { get; set; }
}