using Article.Infrastructure;
using LitZhu.JWT;
using LitZhu.WebApi;
using Microsoft.AspNetCore.Builder;
using NLog.Extensions.Logging;
using StackExchange.Redis;
using User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    // 忽略循环引用
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// 配置日志记录，将控制台记录级别设置为Error或更高级别
builder.Services.AddLogging(builder =>
{
    builder.AddNLog();
    builder.SetMinimumLevel(LogLevel.Debug);
    builder.AddFilter("Microsoft.EntityFrameworkCore.Model.Validation", LogLevel.None); // 过滤掉特定的警告消息
    builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None); // 过滤SQL信息
    //builder.AddFilter("Microsoft", LogLevel.None);
    //builder.AddFilter("System", LogLevel.None);
    builder.AddFilter("LitZhu", LogLevel.Debug); // 将您的命名空间替换为您自己的命名空间
});

// 添加AutoMapper依赖
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// 缓存
builder.Services.AddDistributedMemoryCache();
// NoSql Redis  : private readonly ConnectionMultiplexer _Redis;
builder.Services.AddSingleton(provider =>
{
    string conn = builder.Configuration.GetConnectionString("RedisConnection")!;

    var configuration = ConfigurationOptions.Parse(conn);
    return ConnectionMultiplexer.Connect(configuration);
});
// 添加依赖注入
builder.Services.AddArticleDomainServices(); // 文章模块
builder.Services.AddUserDomainServices(); // 用户模块
builder.Services.AddJwtServices(); // JWT
builder.Services.AddCoreServices(); // Core

// 读取配置文件中 Jwt 的信息，然后通过 Configuration 配置 给Controller使用
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("Jwt"));
// 读取配置文件中的JWT配置
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JWTOptions>();
// 添加JWT认证
builder.Services.AddJWTAuthentication(jwtOptions);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCoreServices(); // Core

// 鉴权
app.UseAuthentication();
// 授权
app.UseAuthorization();


app.MapControllers();

app.Run();
