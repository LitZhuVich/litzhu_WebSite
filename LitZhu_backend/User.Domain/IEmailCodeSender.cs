namespace User.Domain;

/// <summary>
/// 防腐层
/// 用于隔离和保护本地系统与发送电子邮件的功能之间的交互。
/// </summary>
public interface IEmailCodeSender
{
    /// <summary>
    /// 发送电子邮件验证码
    /// </summary>
    /// <param name="email"></param>
    /// <returns>返回验证码</returns>
    Task<string> SendEmailCodeAsync(string email);

    /// <summary>
    /// 保存电子邮件验证码
    /// </summary>
    /// <param name="email">电子邮件地址</param>
    /// <param name="code">验证码</param>
    Task SaveEmailCodeAsync(string email, string code);

    /// <summary>
    /// 查找电子邮件验证码
    /// </summary>
    /// <param name="email">电子邮件地址</param>
    /// <returns>找到的验证码，如果未找到则为 null</returns>
    Task<string?> FindEmailCodeAsync(string email);
}
