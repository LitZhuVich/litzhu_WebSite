using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LitZhu.JWT;

public class JwtService : IJwtService
{
    public string BuildToken(IEnumerable<Claim> claims, JWTOptions jwtOpt)
    {
        TimeSpan ExpiryDuration = TimeSpan.FromSeconds(jwtOpt.ExpireSeconds);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            jwtOpt.Issuer, 
            jwtOpt.Audience, 
            claims,
            expires: DateTime.Now.Add(ExpiryDuration),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
