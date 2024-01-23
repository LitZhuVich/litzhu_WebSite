using FluentValidation;
using User.Domain.DTO;

namespace LitZhu.WebApi.Controllers.User.Validator;

public class RoleCreateDtoValidator : AbstractValidator<RoleCreateDto>
{
    public RoleCreateDtoValidator()
    {
        RuleFor(x => x.RoleName).NotNull().NotEmpty().MaximumLength(10);
        RuleFor(x => x.RoleDesc).MaximumLength(100);
    }
}

public class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
{
    public RoleUpdateDtoValidator()
    {
        RuleFor(x => x.RoleName).NotNull().NotEmpty().MaximumLength(10);
        RuleFor(x => x.RoleDesc).MaximumLength(100);
    }
}
