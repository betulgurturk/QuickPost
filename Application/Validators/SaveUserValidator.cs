using Application.Users.Commands;
using FluentValidation;

public class SaveUserValidator : AbstractValidator<SaveUserCommand>
{
    public SaveUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required.");
        RuleFor(x=>x.UserName).MinimumLength(6).WithMessage("User Name must be at least 6 characters.");
        RuleFor(x => x.UserName).MaximumLength(50).WithMessage("User Name must be at most 50 characters.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is not valid.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password is required and must be at least 6 characters.");
        RuleFor(x => x.Firstname).MaximumLength(50).WithMessage("First Name must be at most 50 characters.");
        RuleFor(x => x.Lastname).MaximumLength(50).WithMessage("Last Name must be at most 50 characters.");
    }
}