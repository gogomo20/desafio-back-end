namespace StockManager.UseCases.UseCases.Users.Responses;

public class UserListResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
}