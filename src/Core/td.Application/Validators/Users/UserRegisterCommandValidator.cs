using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Application.Features.Users.Commands;

namespace td.Application.Validators.Users
{
    internal class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {

        public UserRegisterCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("User {PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("User {PropertyName} must be a valid email address.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("User {PropertyName} is required.")
                .NotNull()
                .MinimumLength(6).WithMessage("User {PropertyName} must not be less than 6 characters.");
        }
    }
}
