using Article.Infrastructure;
using Comment.Infrastructure;
using StackExchange.Redis;
using User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    // ����ѭ������
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// ���AutoMapper����
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// ����
builder.Services.AddDistributedMemoryCache();
// NoSql Redis  : private readonly ConnectionMultiplexer _Redis;
builder.Services.AddSingleton(provider =>
{
    string? conn = builder.Configuration.GetConnectionString("RedisConnection");

    var configuration = ConfigurationOptions.Parse(conn);
    return ConnectionMultiplexer.Connect(configuration);
});

// �������ע��
builder.Services.AddArticleDomainServices(); // ����ģ��
builder.Services.AddUserDomainServices(); // �û�ģ��
builder.Services.AddCommentDomainServices(); // ����ģ��

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
