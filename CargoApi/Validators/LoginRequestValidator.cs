using CargoApi.Models.AccountModels;
using FluentValidation;

namespace CargoApi.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().NotEmpty().WithMessage("Email should not be empty")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password should not be empty");
        }
    }
}
