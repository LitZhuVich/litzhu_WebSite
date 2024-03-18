using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LitZhu.Infrastructure.EFCore;

public class ConnectionString
{
    public string SqlServerConnection { get; set; }
    //public string MySqlConnection { get; set; }  = "Data Source = localhost; Database=LitZhu;User ID = root; Password=admin123;pooling=true;CharSet=utf8;port=3306;sslmode=none";
    public string MySqlConnection { get; set; } = "Server=localhost;Port=3306;Database=LitZhu;Uid=root;Pwd=admin123;";
    
    public ConnectionString(IConfiguration configuration)
    {
        // TODO:3.15需修改
        //SqlServerConnection = "Data Source=.;Initial Catalog=LitZhu;Integrated Security=True;Trust Server Certificate=True";
        //MySqlConnection = configuration.GetConnectionString("ConnectionStrings");
        //MySqlConnection = "server = localhost; port = 3306; database = LitZhu; user = root; password = admin123";
    }

    public ConnectionString()
    {
    }
}
