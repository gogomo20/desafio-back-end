using Microsoft.AspNetCore.Http;

namespace StockManager.Aplication.MiddleWares;

public class UserContextMiddleWare
{
    private readonly RequestDelegate _next;

    public UserContextMiddleWare(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var claims = context.User.Claims;
            var userId = long.Parse(claims.FirstOrDefault(x => x.Type == "userId").Value);
        }
        await _next(context);
    }
}