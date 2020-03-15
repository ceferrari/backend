using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Commands;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.CartModel.Repositories;
using Backend.Shared.Constants;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartDeleteCommandHandler : CommandHandler<CartDeleteCommand, bool>
    {
        private readonly ICartRepository _cartRepository;

        public CartDeleteCommandHandler(IBus bus, ICartRepository cartRepository)
            : base(bus)
        {
            _cartRepository = cartRepository;
        }

        public override async Task<IExecutionResult<bool>> HandleAsync(CartDeleteCommand command, CancellationToken cancellationToken)
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

            var result = await _cartRepository.Delete(command.Id.ToString());

            return new SuccessExecutionResult<bool>(GetType(), result);
        }
    }
}
