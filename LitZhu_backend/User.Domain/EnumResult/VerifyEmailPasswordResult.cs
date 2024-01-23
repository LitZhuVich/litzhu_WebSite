namespace User.Domain.EnumResult;

public enum VerifyEmailPasswordResult
{
    /// <summary>
    /// 返回成功
    /// </summary>
    Ok,
    /// <summary>
    /// 邮箱不存在
    /// </summary>
    EmailNotFound,
    /// <summary>
    /// 用户被锁定
    /// </summary>
    Lockout,
    /// <summary>
    /// 密码错误
    /// </summary>
    PasswordError
}
