using Article.Domain;
using Article.Infrastructure;
using Comment.Infrastructure;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// ���AutoMapper����
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// ���DbContext
builder.Services.AddDbContext<ArticleDbContext>();
builder.Services.AddDbContext<CommentDbContext>();
// �������ע��
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
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

app.MapControllers();

app.Run();
