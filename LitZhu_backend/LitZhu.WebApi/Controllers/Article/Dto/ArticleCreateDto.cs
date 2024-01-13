using FluentValidation;

namespace LitZhu.WebApi.Controllers.Article.Dto;

public record ArticleCreateDto(Guid UserId, string Title, string Content);

public class ArticleCreateDtoValidator : AbstractValidator<ArticleCreateDto>
{
    public ArticleCreateDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty();
        RuleFor(x => x.Title).NotNull().MaximumLength(50);
        RuleFor(x => x.Content).NotNull();
    }
}
