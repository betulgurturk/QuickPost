using Application.Posts.Commands;
using FluentValidation;

namespace Application.Validators.PostValidators
{
    public class SharePostValidator : AbstractValidator<SharePostCommand>
    {
        public SharePostValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.Content).MinimumLength(6).WithMessage("Content must be at least 6 characters.");
            RuleFor(x => x.Content).MaximumLength(500).WithMessage("Content must be at least 500 characters.");
        }
    }
}
