using System.Security.Claims;

namespace LitZhu.JWT
{
    public interface IJwtService
    {
        /// <summary>
        /// 生成JWT
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="jwtOpt"></param>
        /// <returns></returns>
        string BuildToken(IEnumerable<Claim> claims, JWTOptions jwtOpt);

        /// <summary>
        /// 解析JWT返回名字
        /// </summary>
        /// <param name="token"></param>
        /// <param name="jwtOpt"></param>
        /// <returns></returns>
        string? ParseToken(string token, JWTOptions jwtOpt);
    }
}
