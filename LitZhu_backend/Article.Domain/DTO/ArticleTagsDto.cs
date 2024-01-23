namespace Article.Domain.DTO;

public record ArticleTagsCreateDto(Guid articleId, Guid tagId);