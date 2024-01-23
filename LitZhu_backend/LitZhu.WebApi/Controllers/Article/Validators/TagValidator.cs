using Article.Domain.DTO;
using FluentValidation;

namespace LitZhu.WebApi.Controllers.Article.Validators;

public class TagCreateDtoValidator : AbstractValidator<TagCreateDto>
{
    public TagCreateDtoValidator()
    {
        RuleFor(x => x.TagName).NotNull().NotEmpty().MaximumLength(10);
    }
}


public class TagUpdateDtoValidator : AbstractValidator<TagUpdateDto>
{
    public TagUpdateDtoValidator()
    {
        RuleFor(x => x.TagName).NotNull().NotEmpty().MaximumLength(10);
    }
}