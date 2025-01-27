using Application.Posts.Commands;
using FluentValidation;

namespace Application.Validators.PostValidators
{
    public class DeletePostValidation : AbstractValidator<DeletePostCommand>
    {
        public DeletePostValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Post Id is required.");
        }
    }
}
