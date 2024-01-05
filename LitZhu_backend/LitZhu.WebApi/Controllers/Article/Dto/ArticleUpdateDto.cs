using FluentValidation;

namespace Article.WebApi.Dto;

public record ArticleUpdateDto(string Title, string Content);

public class ArticleUpdateDtoValidator : AbstractValidator<ArticleUpdateDto>
{
    public ArticleUpdateDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().MaximumLength(50);
        RuleFor(x => x.Content).NotNull();
    }
}