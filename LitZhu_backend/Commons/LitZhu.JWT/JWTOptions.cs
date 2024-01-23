namespace LitZhu.JWT;

/// <summary>
/// JWT配置选项
/// </summary>
public class JWTOptions
{
    /// <summary>
    /// JWT的签发者
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// JWT的接收者
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// 用于签署JWT的密钥
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// JWT的过期时间（以秒为单位）
    /// </summary>
    public int ExpireSeconds { get; set; }

    /// <summary>
    /// 刷新令牌的过期时间（以秒为单位）
    /// </summary>
    public int RefreshExpireSeconds { get; set; }
}