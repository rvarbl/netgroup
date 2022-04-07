using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers.Identity;

public class IdentityExtensions
{
    public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience,
        DateTime expirationDateTime)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: expirationDateTime,
            signingCredentials: signingCredentials
            
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}