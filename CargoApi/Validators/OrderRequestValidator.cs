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
                .NotNull().NotEmpty().WithMessage("Count should not be empty");

            RuleFor(x => x.Price)
                .NotNull().NotEmpty().WithMessage("Price should not be empty");

            RuleFor(x => x.CargoPrice)
                .NotNull().NotEmpty().WithMessage("Cargo price should not be empty");

            RuleFor(x => x.Size)
                .NotNull().NotEmpty().WithMessage("Size should not be empty");

            RuleFor(x => x.Note)
                .NotNull().NotEmpty().WithMessage("Note should not be empty");
        }
    }
}
