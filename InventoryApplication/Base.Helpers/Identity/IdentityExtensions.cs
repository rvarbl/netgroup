using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers.Identity;

public static class IdentityExtensions
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

    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        return GetUserId<Guid>(user);
    }

    private static TKeyType GetUserId<TKeyType>(this ClaimsPrincipal user)
    {
        var idClaim = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier));
        if (idClaim?.Value == null)
        {
            throw new NullReferenceException("NameIdentifier claim not found.");
        }

        var result = (TKeyType) TypeDescriptor
            .GetConverter(typeof(TKeyType))
            .ConvertFromInvariantString(idClaim.Value)!;

        if (result == null)
        {
            throw new NullReferenceException("Failure to convert idClaim.Value.");
        }

        return result;
    }
}