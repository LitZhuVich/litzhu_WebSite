using Microsoft.Extensions.Configuration;
using System.IO;

namespace LitZhu.Infrastructure.EFCore;

public class ConnectionString
{
    private static IConfiguration? _configuration;

    /// <summary>
    /// 获取应用程序配置信息
    /// </summary>
    /// <returns>返回 IConfiguration 实例</returns>
    public static IConfiguration GetConfiguration()
    {
        if (_configuration == null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        return _configuration;
    }
    /// <summary>
    /// 获取SqlServer连接字符串
    /// </summary>
    public static string? SqlServerConnection => GetConfiguration()["ConnectionStrings:SqlServerConnection"];

    /// <summary>
    /// 获取MySql连接字符串
    /// </summary>
    public static string? MySqlConnection => GetConfiguration()["ConnectionStrings:MySqlServerConnection"];
}
