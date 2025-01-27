using Application.Authentication.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.model.UserName).NotEmpty().WithMessage("User Name is required.");
            RuleFor(x => x.model.UserName).MinimumLength(6).WithMessage("User Name must be at least 6 characters.");
            RuleFor(x => x.model.UserName).MaximumLength(50).WithMessage("User Name must be at most 50 characters.");
            RuleFor(x => x.model.Password).NotEmpty().MinimumLength(6).WithMessage("Password is required and must be at least 6 characters.");
        }
    }
}
