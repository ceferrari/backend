using Backend.Domain.Core.Commands;
using Backend.Shared.Constants;
using FluentValidation;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartCheckoutCommandValidator : CommandValidator<CartCheckoutCommand>
    {
        public CartCheckoutCommandValidator(CartCheckoutCommand command)
        {
            ValidateId();
            ValidateXTeamControl();
            ValidateCurrencyCode();

            ValidationResult = Validate(command);
        }

        public void ValidateId()
        {
            RuleFor(x => x.Id)
                .Must(ValidateGuid).WithMessage(ErrorMessages.Validations.InvalidId);
        }

        public void ValidateXTeamControl()
        {
            RuleFor(x => x.XTeamControl)
                .NotNull()
                .NotEmpty();
        }

        public void ValidateCurrencyCode()
        {
            var acceptedCurrencies = DomainValues.AcceptedCurrencies.List;
            RuleFor(x => x.CurrencyCode)
                .Must(x => acceptedCurrencies.Contains(x)).WithMessage("'currencyCode' must be one of ['" + string.Join("', '", acceptedCurrencies) + "']");
        }
    }
}
