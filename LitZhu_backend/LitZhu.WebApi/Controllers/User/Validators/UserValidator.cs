using FluentValidation;
using User.Domain.DTO;

namespace LitZhu.WebApi.Controllers.User.Validators;

public class UserCreateByUsernameDtoValidator : AbstractValidator<UserCreateByUsernameDto>
{
    public UserCreateByUsernameDtoValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(10);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(20);
    }
}

public class UserCreateByEmailDtoValidator : AbstractValidator<UserCreateByEmailDto>
{
    public UserCreateByEmailDtoValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(10);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(20);
    }
}

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(10);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(20);
    }
}

