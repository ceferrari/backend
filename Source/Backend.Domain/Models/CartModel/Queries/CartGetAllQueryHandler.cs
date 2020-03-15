using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Queries;
using Backend.Domain.Core.Results;
using Backend.Domain.Models.CartModel.Repositories;

namespace Backend.Domain.Models.CartModel.Queries
{
    public sealed class CartGetAllQueryHandler : QueryHandler<CartGetAllQuery, List<Cart>>
    {
        private readonly ICartRepository _cartRepository;

        public CartGetAllQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public override async Task<IExecutionResult<List<Cart>>> HandleAsync(CartGetAllQuery query, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.GetAll();

            return new SuccessExecutionResult<List<Cart>>(GetType(), result);
        }
    }
}
