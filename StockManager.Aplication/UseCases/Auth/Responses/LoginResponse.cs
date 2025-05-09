namespace StockManager.Aplication.UseCases.Auth.Responses;

public class LoginResponse
{
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Token { get;  set; }
    public ICollection<string> Permissions { get; set; } = [];
}