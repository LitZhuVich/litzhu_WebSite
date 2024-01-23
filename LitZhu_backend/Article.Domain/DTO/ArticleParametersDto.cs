namespace Article.Domain.DTO;

public record ArticleParametersDto(
    string? Q, 
    int PageIndex = 1, 
    int PageSize = 10,
    string OrderBy = "Likes");