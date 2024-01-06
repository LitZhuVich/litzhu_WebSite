namespace LitZhu.Infrastructure.EFCore;

public class ConnectionString
{
    public string SqlServerConnection { get; set; }

    public ConnectionString()
    {
        SqlServerConnection 
            = "Data Source=.;Initial Catalog=LitZhu;Integrated Security=True;Trust Server Certificate=True";
    }

    public ConnectionString(string connectionStr)
    {
        SqlServerConnection = connectionStr;
    }

}
