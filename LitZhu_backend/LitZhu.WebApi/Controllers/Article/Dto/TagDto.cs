using LitZhu.DomainCommons.Models;

namespace LitZhu.WebApi.Controllers.Article.Dto;

public class TagDto : IBaseEntity
{
    public Guid Id { get; private set; }

    public string TagName { get; private set; } = string.Empty; // 文章标签名
}
