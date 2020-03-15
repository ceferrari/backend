using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Commands;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.CartModel.Repositories;
using Backend.Domain.Models.InvoiceModel;
using Backend.Domain.Models.ProductModel.Repositories;
using Backend.Domain.Services;
using Backend.Shared.Constants;
using Backend.Shared.Utilities;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartCheckoutCommandHandler : CommandHandler<CartCheckoutCommand, bool>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IQueueService _queueService;

        public CartCheckoutCommandHandler(IBus bus, ICartRepository cartRepository, IProductRepository productRepository, IQueueService queueService)
            : base(bus)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _queueService = queueService;
        }

        public override async Task<IExecutionResult<bool>> HandleAsync(CartCheckoutCommand command, CancellationToken cancellationToken)
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

            var items = cart.Items.Select(x => new CartItem(x.Id)
            {
                Price = x.Price,
                Scale = x.Scale,
                CurrencyCode = x.CurrencyCode,
                Product = _productRepository.GetById(x.Id.ToString()).Result
            }).ToList();

            cart.Items = items;

            var invoice = new Invoice(command.XTeamControl, command.CurrencyCode);
            var invoiceAggregate = new InvoiceAggregate(invoice, cart);

            await Retry.DoAsync(async () => await _queueService.SendToSqsAsync(invoiceAggregate));

            var result = await _cartRepository.Checkout(command.Id.ToString());

            return new SuccessExecutionResult<bool>(GetType(), result);
        }
    }
}
