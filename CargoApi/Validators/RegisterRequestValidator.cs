﻿using CargoApi.Models.AccountModels;
using FluentValidation;

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

            RuleFor(x => x.Password)
                .NotNull().NotEmpty().WithMessage("Password should not be empty")
                .MinimumLength(5).WithMessage("Minimum length should be 5");

            RuleFor(x => x.ConfirmPassword)
                .NotNull().NotEmpty().WithMessage("Password should not be empty")
                .MinimumLength(5).WithMessage("Minimum length should be 5")
                .Equal(x => x.Password).WithMessage("Password doesn't match");

            RuleFor(x => x.Email)
                .NotNull().NotEmpty().WithMessage("Email should not be empty")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.PhoneNumber)
                .NotNull().NotEmpty().WithMessage("Phone number should not be empty")
                .Length(10).WithMessage("Phone number must be 10 characters")
                .Matches(@"^[0-9]+$").WithMessage("Phone number is not valid");

            RuleFor(x => x.PinCode)
               .NotNull().NotEmpty().WithMessage("PIN Code should not be empty")
               .Length(7).WithMessage("PIN Code must be 7 characters");

            RuleFor(x => x.Address)
               .NotNull().NotEmpty().WithMessage("Address should not be empty")
               .MaximumLength(100).WithMessage("Maximum length must be 100 characters");
        }
    }
}
