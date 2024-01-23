using System.Security.Claims;

namespace LitZhu.JWT
{
    public interface IJwtService
    {
        string BuildToken(IEnumerable<Claim> claims, JWTOptions jwtOpt);
    }
}
