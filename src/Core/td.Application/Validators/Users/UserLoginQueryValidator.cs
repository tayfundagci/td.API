using FluentValidation;
using td.Application.Features.Users.Queries;

namespace td.Application.Validators.Users
{
    public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(v => v.Email).NotEmpty().NotNull().WithMessage("Mail can not be null or empty.");
            RuleFor(v => v.Email).EmailAddress().MinimumLength(7).MaximumLength(55).WithMessage("Mail address must contain @ and minimum 7 max 55 characters");
            RuleFor(v => v.Email).EmailAddress().NotEqual("E mail adress not equal");
            RuleFor(v => v.Password).NotNull().NotEmpty().WithMessage("Password can not be null or empty");
            RuleFor(v => v.Password).MinimumLength(8).WithMessage("Password must contains at least 8 characters");
        }
    }
}
