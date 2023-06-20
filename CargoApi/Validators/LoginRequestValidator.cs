using CargoApi.Models.AccountModels;
using FluentValidation;

namespace CargoApi.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Username should not be empty");
            RuleFor(x => x.PasswordHash).NotNull().NotEmpty().WithMessage("Username should not be empty");
        }
    }
}
