using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Commands;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.CartModel.Repositories;
using Backend.Domain.Models.ProductModel.Repositories;
using Backend.Shared.Constants;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartCreateCommandHandler : CommandHandler<CartCreateCommand, Cart>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartCreateCommandHandler(IBus bus, ICartRepository cartRepository, IProductRepository productRepository)
            : base(bus)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public override async Task<IExecutionResult<Cart>> HandleAsync(CartCreateCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                return ValidationErrors(command);
            }

            if (await _cartRepository.ExistsByCustomerId(command.CustomerId))
            {
                return ExecutionError(command, ErrorMessages.Cart.AlreadyExistsByCustomerId);
            }

            var product = _productRepository.GetBySku(command.Item.Sku).Result;

            if (product == null)
            {
                return ExecutionError(command, ErrorMessages.Product.DoesNoExistsBySku);
            }

            var item = new CartItem(product)
            {
                Price = command.Item.Quantity * product.Price.Amount
            };

            var cart = new Cart(command.Id)
            {
                CustomerId = command.CustomerId,
                Status = command.Status,
                Items = new List<CartItem> { item }
            };

            var result = await _cartRepository.Add(cart);

            return new SuccessExecutionResult<Cart>(GetType(), result);
        }
    }
}
