using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Queries;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.CartModel.Repositories;

namespace Backend.Domain.Models.CartModel.Queries
{
    public sealed class CartGetByIdQueryHandler : QueryHandler<CartGetByIdQuery, Cart>
    {
        private readonly ICartRepository _cartRepository;

        public CartGetByIdQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public override async Task<IExecutionResult<Cart>> HandleAsync(CartGetByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.GetById(query.Id.ToString());

            return new SuccessExecutionResult<Cart>(GetType(), result);
        }
    }
}
