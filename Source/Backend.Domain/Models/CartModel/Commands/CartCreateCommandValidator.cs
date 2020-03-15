using Backend.Domain.Core.Commands;
using Backend.Shared.Constants;
using FluentValidation;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartCreateCommandValidator : CommandValidator<CartCreateCommand>
    {
        public CartCreateCommandValidator(CartCreateCommand command)
        {
            ValidateId();
            ValidateCustomerId();
            //ValidateStatus();
            ValidateItem();

            ValidationResult = Validate(command);
        }

        public void ValidateId()
        {
            RuleFor(x => x.Id)
                .Must(ValidateGuid).WithMessage(ErrorMessages.Validations.InvalidId);
        }

        public void ValidateCustomerId()
        {
            RuleFor(x => x.CustomerId)
                .NotNull()
                .NotEmpty();
        }

        public void ValidateStatus()
        {
            var acceptedStatuses = DomainValues.Cart.Status.List;
            RuleFor(x => x.Status)
                .Must(x => acceptedStatuses.Contains(x)).WithMessage("'status' must be one of ['" + string.Join("', '", acceptedStatuses) + "']");
        }

        public void ValidateItem()
        {
            RuleFor(x => x.Item.Sku)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Item.Quantity)
                .GreaterThan(0);
        }
    }
}
