using FluentValidation;

namespace Timecards.Application.Command.Account
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(c => c.LastName).NotEmpty().MaximumLength(20);
            RuleFor(c => c.UserName).MinimumLength(6).MaximumLength(20).NotEmpty();
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.Password).Equal(c => c.ConfirmPassword).WithMessage("The password doesn't equal.").MinimumLength(4);
            RuleFor(x => x.RoleType).IsInEnum();
        }
    }
}