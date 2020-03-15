using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Queries;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.ProductModel.Repositories;

namespace Backend.Domain.Models.ProductModel.Queries
{
    public sealed class ProductGetByIdQueryHandler : QueryHandler<ProductGetByIdQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductGetByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override async Task<IExecutionResult<Product>> HandleAsync(ProductGetByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetById(query.Id.ToString());

            return new SuccessExecutionResult<Product>(GetType(), result);
        }
    }
}
