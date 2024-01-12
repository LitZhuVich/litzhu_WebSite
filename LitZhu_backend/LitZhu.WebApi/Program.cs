using Article.Infrastructure;
using Comment.Infrastructure;
using LitZhu.JWT;
using StackExchange.Redis;
using User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    // 忽略循环引用
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// 添加AutoMapper依赖
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// 缓存
builder.Services.AddDistributedMemoryCache();
// NoSql Redis  : private readonly ConnectionMultiplexer _Redis;
builder.Services.AddSingleton(provider =>
{
    string? conn = builder.Configuration.GetConnectionString("RedisConnection");

    var configuration = ConfigurationOptions.Parse(conn);
    return ConnectionMultiplexer.Connect(configuration);
});

// 添加依赖注入
builder.Services.AddArticleDomainServices(); // 文章模块
builder.Services.AddUserDomainServices(); // 用户模块
builder.Services.AddCommentDomainServices(); // 评论模块
builder.Services.AddJwtServices(); // JWT 模块

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

// 鉴权
app.UseAuthentication();
// 授权
app.UseAuthorization();

app.MapControllers();

app.Run();
