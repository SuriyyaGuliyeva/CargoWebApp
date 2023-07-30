using CargoApi.Models.OrderModels;
using FluentValidation;

namespace cargoapi.validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequestModel>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.Link)
                .NotNull().NotEmpty().WithMessage("Link should not be empty");

            RuleFor(x => x.Count)
                .NotNull().WithMessage("Count should not be empty")
                .GreaterThan(0).WithMessage("Count must be greater than zero");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price should not be empty")
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.CargoPrice)
                .NotNull().NotEmpty().WithMessage("Cargo price should not be empty");

            RuleFor(x => x.TotalAmount)
                .NotNull().WithMessage("Total amount should not be empty")
                .GreaterThan(0).WithMessage("Total amount must be greater than zero");

            RuleFor(x => x.Size)
                .NotNull().WithMessage("Size should not be empty");

            RuleFor(x => x.Note)
                .NotNull().NotEmpty().WithMessage("Note should not be empty");
        }
    }
}
