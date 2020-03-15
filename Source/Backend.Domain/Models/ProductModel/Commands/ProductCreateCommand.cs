using System;
using Backend.Domain.Core.Commands;
using Backend.Domain.ViewModels.ProductViewModel;
using Backend.Shared.Extensions;

namespace Backend.Domain.Models.ProductModel.Commands
{
    public sealed class ProductCreateCommand : Command<Guid, Product>
    {
        public ProductCreateCommand(ProductViewModel product)
            : base(Comb.NewComb())
        {
            Sku = product.Sku;
            Name = product.Name;
            ShortDescription = product.ShortDescription;
            LongDescription = product.LongDescription;
            ImageUrl = product.ImageUrl;
            Price = new ProductPrice
            {
                Amount = product.Price.Amount,
                Scale = product.Price.Scale,
                CurrencyCode = product.Price.CurrencyCode
            };

            Validator = new ProductCreateCommandValidator(this);
        }

        public string Sku { get; }
        public string Name { get; }
        public string ShortDescription { get; }
        public string LongDescription { get; }
        public string ImageUrl { get; }
        public ProductPrice Price { get; }
    }
}
