using LitZhu.DomainCommons.Models;

namespace Article.Domain.DTO;

public class TagDto : IBaseEntity
{
    public Guid Id { get; private set; }
    public string TagName { get; private set; } = string.Empty; // 文章标签名
    public int TagCount { get; private set; }
}

public record TagCreateDto(string TagName);

public record TagUpdateDto(string? TagName);