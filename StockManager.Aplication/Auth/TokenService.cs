using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockManager.Domain.Entities;

namespace StockManager.Aplication.JWTRepository;

public class TokenService
{
    public static string GenerateToken(User user, String secret)
    {
        var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("name", user.Name),
            new Claim("username", user.UserName),
            new Claim("email", user.Email),
        };
        var key = Encoding.ASCII.GetBytes(secret ?? "SECRET");
        // var tokenConfig = new SecurityTokenDescriptor
        // {
        //     Subject = new System.Security.Claims.ClaimsIdentity(claims: claims),
        //     Expires = DateTime.UtcNow.AddHours(2),
        //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        // };
        var tokenJwt = new JwtSecurityToken(
            "",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        // var token = tokenHandler.CreateToken(tokenConfig);
        var tokenString = tokenHandler.WriteToken(tokenJwt);
        return tokenString;
    }
}