using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Queries;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.CartModel.Repositories;

namespace Backend.Domain.Models.CartModel.Queries
{
    public sealed class CartGetByCustomerIdQueryHandler : QueryHandler<CartGetByCustomerIdQuery, Cart>
    {
        private readonly ICartRepository _cartRepository;

        public CartGetByCustomerIdQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public override async Task<IExecutionResult<Cart>> HandleAsync(CartGetByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.GetById(query.CustomerId);

            return new SuccessExecutionResult<Cart>(GetType(), result);
        }
    }
}
