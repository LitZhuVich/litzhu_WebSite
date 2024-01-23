using Article.Domain.DTO;
using FluentValidation;

namespace LitZhu.WebApi.Controllers.Article.Validators;

public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
{
    public CommentCreateDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.ArticleId).NotNull();
        RuleFor(x => x.Content).NotNull().MaximumLength(1000);
    }
}

public class CommentUpdateDtoValidator : AbstractValidator<CommentUpdateDto>
{
    public CommentUpdateDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.ArticleId).NotNull();
        RuleFor(x => x.Content).NotNull().MaximumLength(1000);
    }
}