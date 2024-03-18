using LitZhu.DomainCommons;
using LitZhu.JWT;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using User.Domain.Entities;
using User.Domain.EnumResult;

namespace User.Domain;

public class UserDomainService(
    IUserRepository _userRepository,
    IOptions<JWTOptions> _options, 
    IJwtService _jwtService, 
    IEmailCodeSender _emailCodeSender,
    ILogger<UserDomainService> _logger)
{
    private readonly JWTOptions _jwtOptions = _options.Value;

    /// <summary>
    /// 验证邮箱对应的密码
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<VerifyEmailPasswordResult> VerifyLoginEmailByPasswordAsync(string email, string password)
    {
        VerifyEmailPasswordResult result;

        var user = await _userRepository.FindUserByEmailAsync(email);

        if (user == null) // 用户邮箱不存在
        {
            result = VerifyEmailPasswordResult.EmailNotFound;
        }
        else if(user.UserAccessFail.IsLockOuted()) // 用户锁定
        {
            result = VerifyEmailPasswordResult.Lockout;
        }
        else if (user.VerifyPassword(password) == false) // 密码错误
        {
            result = VerifyEmailPasswordResult.PasswordError;
        }
        else // 密码正确
        {
            result = VerifyEmailPasswordResult.Ok;
        }

        //如果密码正确则重置错误信息，否则处理一次“登陆失败”
        if (result == VerifyEmailPasswordResult.Ok)
        {
            user!.UserAccessFail.Reset();
        }
        else
        {
            user!.UserAccessFail.Fail();
        }

        // 记录登陆历史
        await _userRepository.AddNewLoginByEmailHistoryAsync(email, result.ToString());
        await _userRepository.SaveUserAsync();
        return result;
    }

    /// <summary>
    /// 验证邮箱对应的验证码
    /// </summary>
    /// <param name="email"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<VerifyEmailCodeResult> VerifyLoginEmailByCodeAsync(string email, string code)
    {
        var user = await _userRepository.FindUserByEmailAsync(email);

        if (user == null) // 用户邮箱不存在
        {
            return VerifyEmailCodeResult.EmailNotFound;
        }
        else if (user.UserAccessFail.IsLockOuted()) // 被锁定
        {
            return VerifyEmailCodeResult.Lockout;
        }

        // 获取服务器上的验证码并删除
        string? codeInServer = await _emailCodeSender.FindEmailCodeAsync(email);
        
        // 服务器上的验证码为空 或 验证码和服务器上的不一致
        if (codeInServer == null || codeInServer != code) 
        {
            user!.UserAccessFail.Fail();
            return VerifyEmailCodeResult.CodeError;
        }
        else
        {
            return VerifyEmailCodeResult.Ok;
        }
    }

    /// <summary>
    /// 验证用户名对应的密码
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<(VerifyUsernamePasswordResult Result, string? Token)> VerifyLoginUsernameByPasswordAsync(string username, string password)
    {
        VerifyUsernamePasswordResult result = VerifyUsernamePasswordResult.Ok;
        string? token = null;

        var user = await _userRepository.FindUserByUsernameAsync(username);
        if (user == null) // 用户不存在
        {
            return (VerifyUsernamePasswordResult.UsernameNotFound, token);
        }

        if (user.UserAccessFail.IsLockOuted()) // 用户被锁定
        {
            result = VerifyUsernamePasswordResult.Lockout;
        }
        else if (!user.VerifyPassword(password)) // 密码错误
        {
            result = VerifyUsernamePasswordResult.PasswordError;
        }

        if (result == VerifyUsernamePasswordResult.Ok)
        {
            // 登陆成功重置错误信息
            user.UserAccessFail.Reset();
            token = BuildToken(user);
        }
        else
        {
            _logger.LogWarning("添加一次登录失败信息，用户名不在数据库中不会操作数据库添加数据");
            user.UserAccessFail.Fail();
        }

        // 添加用户登陆历史
        await _userRepository.AddNewLoginByUsernameHistoryAsync(username, result.ToString());
        await _userRepository.SaveUserAsync();
        return (result, token);
    }

    /// <summary>
    /// 创建令牌
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private string BuildToken(Users user)
    {
        // 声明 payload
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        ];
            
        List<Roles> roles = user.GetRoles();
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
        }

        // 生成 Key
        return _jwtService.BuildToken(claims, _jwtOptions);
    }

    /// <summary>
    /// 解析令牌
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public string? ParseToken(string token)
    {
        // TODO:需要对数据进行操作
        return _jwtService.ParseToken(token, _jwtOptions);
    }
}
