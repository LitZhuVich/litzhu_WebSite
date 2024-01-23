using System.Security.Cryptography;
using System.Text;

namespace User.Domain;

public class HashHelper
{
    /// <summary>
    /// 加密密码
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string HashPassword(string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = SHA256.HashData(passwordBytes);
        string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        return hashedPassword;
    }

    /// <summary>
    /// 验证密码
    /// </summary>
    /// <param name="password"></param>
    /// <param name="hashedPwd"></param>
    /// <returns></returns>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        string hashedInput = HashPassword(password);

        bool isPasswordValid = hashedInput.Equals(hashedPassword);
        return isPasswordValid;
    }
}
