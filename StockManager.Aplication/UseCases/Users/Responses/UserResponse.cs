using StockManager.Domain.Entities;

namespace StockManager.UseCases.UseCases.Users.Responses;

public class UserResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required ICollection<string> Permissions { get; set; } = [];
}