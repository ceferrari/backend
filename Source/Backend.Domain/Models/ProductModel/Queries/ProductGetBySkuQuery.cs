using Backend.Domain.Core.Queries;

namespace Backend.Domain.Models.ProductModel.Queries
{
    public sealed class ProductGetBySkuQuery : Query<Product, Product>
    {
        public ProductGetBySkuQuery(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }
}
