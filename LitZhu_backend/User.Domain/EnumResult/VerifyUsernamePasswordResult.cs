namespace User.Domain.EnumResult;

public enum VerifyUsernamePasswordResult
{
    /// <summary>
    /// 返回成功
    /// </summary>
    Ok,
    /// <summary>
    /// 用户名不存在
    /// </summary>
    UsernameNotFound,
    /// <summary>
    /// 用户被锁定
    /// </summary>
    Lockout,
    /// <summary>
    /// 密码错误
    /// </summary>
    PasswordError
}
