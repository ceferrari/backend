using Backend.Domain.Core.Commands;
using Backend.Shared.Constants;
using FluentValidation;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartDeleteCommandValidator : CommandValidator<CartDeleteCommand>
    {
        public CartDeleteCommandValidator(CartDeleteCommand command)
        {
            ValidateId();

            ValidationResult = Validate(command);
        }

        public void ValidateId()
        {
            RuleFor(x => x.Id)
                .Must(ValidateGuid).WithMessage(ErrorMessages.Validations.InvalidId);
        }
    }
}
