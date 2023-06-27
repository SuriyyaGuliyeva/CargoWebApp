using CargoApi.Models.AccountModels;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CargoApi.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("Username should not be empty");

            RuleFor(x => x.Surname)
               .NotNull().NotEmpty().WithMessage("Surname should not be empty");

            RuleFor(x => x.PasswordHash)
                .NotNull().NotEmpty().WithMessage("Password should not be empty")
                .MinimumLength(5).WithMessage("Minimum length should be 5");

            RuleFor(x => x.Email)
                .NotNull().NotEmpty().WithMessage("Email should not be empty")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.PhoneNumber)
                .NotNull().NotEmpty().WithMessage("Phone number should not be empty")
                .MinimumLength(10).WithMessage("Phone number must not be less than 10 characters")
                .Matches(@"^\d{10}$").WithMessage("Phone number not valid");
        }
    }
}
