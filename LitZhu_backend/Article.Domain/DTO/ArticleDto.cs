using LitZhu.DomainCommons.Models;

namespace Article.Domain.DTO;

public class ArticleDto : IBaseEntity, IHasCreationTime, IHasModificationTime
{
    public Guid Id { get; private set; }
    public string? Image {  get; private set; }
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public string? Desc { get; private set; } 
    public Guid UserId { get; private set; } 
    public int Likes { get; private set; } 
    public int Views { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime? LastModificationTime { get; private set; }
}

public record ArticleCreateDto(Guid UserId, string Image, string Title, string Content, string Desc);

public record ArticleUpdateDto(string? Image, string? Title, string? Content, string? Desc, int? Likes, int? Views);