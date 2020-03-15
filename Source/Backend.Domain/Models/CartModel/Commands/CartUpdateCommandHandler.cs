using System.Linq;
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
    public sealed class CartUpdateCommandHandler : CommandHandler<CartUpdateCommand, Cart>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartUpdateCommandHandler(IBus bus, ICartRepository cartRepository, IProductRepository productRepository)
            : base(bus)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public override async Task<IExecutionResult<Cart>> HandleAsync(CartUpdateCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                return ValidationErrors(command);
            }

            var cart = await _cartRepository.GetById(command.Id.ToString());

            if (cart == null)
            {
                return ExecutionError(command, ErrorMessages.Cart.DoesNotExistsById);
            }

            if (DomainValues.Cart.Status.Cancel.Equals(cart.Status))
            {
                return ExecutionError(command, ErrorMessages.Cart.AlreadyCanceled);
            }

            if (DomainValues.Cart.Status.Done.Equals(cart.Status))
            {
                return ExecutionError(command, ErrorMessages.Cart.AlreadyDone);
            }

            var product = await _productRepository.GetBySku(command.Item.Sku);

            if (product == null)
            {
                return ExecutionError(command, ErrorMessages.Product.DoesNoExistsBySku);
            }

            var price = command.Item.Quantity * product.Price.Amount;

            if (cart.Items.Any(x => x.Id == product.Id))
            {
                cart.Items.Single(x => x.Id == product.Id).Price += price;
            }
            else 
            {
                cart.Items.Add(new CartItem(product) { Price = price });
            }

            var result = await _cartRepository.Update(cart);

            return new SuccessExecutionResult<Cart>(GetType(), result);
        }
    }
}
