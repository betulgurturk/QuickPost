using Application.Posts.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.PostValidators
{
    public class GetPostsByUserNameValidator : AbstractValidator<GetPostsByUserNameQuery>
    {
        public GetPostsByUserNameValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required.");
            RuleFor(x => x.UserName).MinimumLength(6).WithMessage("User Name must be at least 6 characters.");
            RuleFor(x => x.UserName).MaximumLength(50).WithMessage("User Name must be at most 50 characters.");
        }
    }
}
