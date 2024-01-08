using Article.Infrastructure;
using Comment.Infrastructure;
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
