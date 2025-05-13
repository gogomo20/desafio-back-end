using Microsoft.AspNetCore.Http;
using StockManager.Aplication.JWTRepository;

namespace StockManager.Aplication.MiddleWares;

public class UserContextMiddleWare
{
    private readonly RequestDelegate _next;

    public UserContextMiddleWare(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, UserContext userContext)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var claims = context.User.Claims;
            var userId = long.Parse(claims.FirstOrDefault(x => x.Type == "userId")?.Value ?? "-1");
            userContext.Id = userId;
        }
        await _next(context);
    }
}