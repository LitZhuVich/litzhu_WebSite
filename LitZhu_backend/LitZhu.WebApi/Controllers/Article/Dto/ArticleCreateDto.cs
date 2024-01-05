using FluentValidation;

namespace Article.WebApi.Dto;

public record ArticleCreateDto(string Title, string Content);

public class ArticleCreateDtoValidator : AbstractValidator<ArticleCreateDto>
{
    public ArticleCreateDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().MaximumLength(50);
        RuleFor(x => x.Content).NotNull();
    }
}
