using Backend.Domain.Core.Commands;
using Backend.Shared.Constants;
using FluentValidation;

namespace Backend.Domain.Models.ProductModel.Commands
{
    public sealed class ProductCreateCommandValidator : CommandValidator<ProductCreateCommand>
    {
        public ProductCreateCommandValidator(ProductCreateCommand command)
        {
            ValidateId();
            ValidateSku();
            ValidateName();
            ValidateShortDescription();
            ValidateLongDescription();
            ValidateImageUrl();
            ValidatePrice();

            ValidationResult = Validate(command);
        }

        public void ValidateId()
        {
            RuleFor(x => x.Id)
                .Must(ValidateGuid).WithMessage(ErrorMessages.Validations.InvalidId);
        }

        public void ValidateSku()
        {
            RuleFor(x => x.Sku)
                .NotNull()
                .NotEmpty();
        }

        public void ValidateName()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);
        }

        public void ValidateShortDescription()
        {
            RuleFor(x => x.ShortDescription)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);
        }

        public void ValidateLongDescription()
        {
            RuleFor(x => x.LongDescription)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);
        }

        public void ValidateImageUrl()
        {
            RuleFor(x => x.ImageUrl)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);
        }

        public void ValidatePrice()
        {
            RuleFor(x => x.Price.Amount)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Price.Scale)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            var acceptedCurrencies = DomainValues.AcceptedCurrencies.List;
            RuleFor(x => x.Price.CurrencyCode)
                .Must(x => acceptedCurrencies.Contains(x)).WithMessage("'price.currencyCode' must be one of ['" + string.Join("', '", acceptedCurrencies) + "']");
        }
    }
}
