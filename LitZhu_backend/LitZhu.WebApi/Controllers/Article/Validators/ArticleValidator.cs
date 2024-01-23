using Article.Domain.DTO;
using FluentValidation;

namespace LitZhu.WebApi.Controllers.Article.Validators;

public class ArticleCreateDtoValidator : AbstractValidator<ArticleCreateDto>
{
    public ArticleCreateDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .WithMessage("创建文章时用户ID不能为空");
        RuleFor(x => x.Title).NotNull().MaximumLength(50);
        RuleFor(x => x.Content).NotNull();
    }
}

public class ArticleUpdateDtoValidator : AbstractValidator<ArticleUpdateDto>
{
    public ArticleUpdateDtoValidator()
    {
        RuleFor(x => x.Title).MaximumLength(50);
    }
}