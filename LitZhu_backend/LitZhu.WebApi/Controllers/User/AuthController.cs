using LitZhu.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using User.Domain;
using User.Domain.Entities;
using User.Domain.EnumResult;

namespace LitZhu.WebApi.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IUserRepository _userRepository,
    IEmailCodeSender _emailCodeSender,
    UserDomainService _userDomainService,
    ILogger<AuthController> _logger,
    ConnectionMultiplexer _cache) : ControllerBase
{
    /// <summary>
    /// 发送邮箱验证码
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost("SendEmailCode")]
    public async Task<ActionResult<string>> SendEmailCode(SendEmailCodeRequest req)
    {
        Users? user = await _userRepository.FindUserByEmailAsync(req.Email);
        if (user == null)
        {
            return BadRequest(R.Fail("找不到邮箱"));
        }
        string code = await _emailCodeSender.SendEmailCodeAsync(req.Email);
        return Ok(R.Success(code));
    }

    /// <summary>
    /// 验证邮箱验证码
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost("VerifyEmailCode")]
    public async Task<ActionResult<string>> VerifyEmailCode(VerifyEmailCodeRequest req)
    {
        VerifyEmailCodeResult result = await _userDomainService.VerifyLoginEmailByCodeAsync(req.Email, req.Code);


        switch (result)
        {
            case VerifyEmailCodeResult.Ok:
                return Ok(R.Success("验证成功"));
            case VerifyEmailCodeResult.EmailNotFound:
            case VerifyEmailCodeResult.CodeError:
                return BadRequest(R.Fail("验证失败"));
            case VerifyEmailCodeResult.Lockout:
                return BadRequest(R.Fail("被锁定"));
            default:
                return BadRequest(R.Fail("未知值"));
        }
    }

    /// <summary>
    /// 验证用户名密码
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<ActionResult<string>> VerifyLoginUsernamePassword(VerifyLoginUsernamePasswordRequest req)
    {
        (VerifyUsernamePasswordResult result, string? token) = await _userDomainService.VerifyLoginUsernameByPasswordAsync(req.Username, req.Password);

        switch (result)
        {
            case VerifyUsernamePasswordResult.Ok:
                string key = $"LitZhu_{req.Username}_Token";
                await _cache.GetDatabase(1).StringSetAsync(key, token); // 添加 token 到缓存 Redis
                return Ok(R.Success(token));
            case VerifyUsernamePasswordResult.UsernameNotFound:
            case VerifyUsernamePasswordResult.PasswordError:
                return BadRequest(R.Fail("登陆失败"));
            case VerifyUsernamePasswordResult.Lockout:
                return BadRequest(R.Fail("被锁定"));
            default:
                return BadRequest(R.Fail("未知值"));
        }
    }

    /// <summary>
    /// 注销用户
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpDelete("Logout/{token}")]
    [Authorize]
    public async Task<IActionResult> Logout(string token)
    {
        string key = $"LitZhu_{User.Identity?.Name}_Token";
        string? cachedToken = await _cache.GetDatabase(1).StringGetAsync(key);
        if (cachedToken == null || cachedToken != token)
        {
            return BadRequest(R.Fail("令牌验证失败"));
        }

        // 删除缓存
        await _cache.GetDatabase(1).KeyDeleteAsync(key);
        return Ok(R.Success("注销成功"));
    }
}

public record SendEmailCodeRequest(string Email);
public record VerifyEmailCodeRequest(string Email, string Code);
public record VerifyLoginUsernamePasswordRequest(string Username, string Password);