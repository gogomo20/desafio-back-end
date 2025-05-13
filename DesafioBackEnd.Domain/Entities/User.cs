using StockManager.Domain.Basis;

namespace StockManager.Domain.Entities;

public class User : BaseTable
{
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public long? ProfileId { get; set; }
    public ProfileEntity? Profile { get; set; }
}