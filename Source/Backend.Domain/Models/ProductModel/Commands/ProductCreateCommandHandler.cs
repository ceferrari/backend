using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Commands;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.ProductModel.Repositories;
using Backend.Shared.Constants;

namespace Backend.Domain.Models.ProductModel.Commands
{
    public sealed class ProductCreateCommandHandler : CommandHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductCreateCommandHandler(IBus bus, IProductRepository productRepository)
            : base(bus)
        {
            _productRepository = productRepository;
        }

        public override async Task<IExecutionResult<Product>> HandleAsync(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                return ValidationErrors(command);
            }

            if (await _productRepository.ExistsBySku(command.Sku))
            {
                return ExecutionError(command, ErrorMessages.Product.AlreadyExistsBySku);
            }

            var product = new Product(command.Id)
            {
                Sku = command.Sku,
                Name = command.Name,
                ShortDescription = command.ShortDescription,
                LongDescription = command.LongDescription,
                ImageUrl = command.ImageUrl,
                Price = command.Price
            };

            var result = await _productRepository.Add(product);

            return new SuccessExecutionResult<Product>(GetType(), result);
        }
    }
}
