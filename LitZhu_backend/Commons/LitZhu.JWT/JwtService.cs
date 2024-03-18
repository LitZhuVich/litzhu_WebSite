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

    public string? ParseToken(string token, JWTOptions jwtOpt)
    {
        var key = Encoding.ASCII.GetBytes(jwtOpt.Key);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOpt.Issuer,
            ValidAudience = jwtOpt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
        
        var handler = new JwtSecurityTokenHandler();

        SecurityToken securityToken;

        var principal = handler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtToken = securityToken as JwtSecurityToken;

        //if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("无效的令牌");
        }

        return principal.Identity?.Name;
    }

}
