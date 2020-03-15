using Backend.Domain.Core.Queries;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.ProductModel.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Domain.Models.ProductModel.Queries
{
    public sealed class ProductGetAllQueryHandler : QueryHandler<ProductGetAllQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public ProductGetAllQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override async Task<IExecutionResult<List<Product>>> HandleAsync(ProductGetAllQuery query, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAll();

            return new SuccessExecutionResult<List<Product>>(GetType(), result);
        }
    }
}
