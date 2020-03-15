using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Queries;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.ProductModel.Repositories;

namespace Backend.Domain.Models.ProductModel.Queries
{
    public sealed class ProductGetBySkuQueryHandler : QueryHandler<ProductGetBySkuQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductGetBySkuQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override async Task<IExecutionResult<Product>> HandleAsync(ProductGetBySkuQuery query, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetBySku(query.Sku);

            return new SuccessExecutionResult<Product>(GetType(), result);
        }
    }
}
