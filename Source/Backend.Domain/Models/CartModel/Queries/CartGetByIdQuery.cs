using System;
using Backend.Domain.Core.Queries;

namespace Backend.Domain.Models.CartModel.Queries
{
    public sealed class CartGetByIdQuery : Query<Cart, Cart>
    {
        public CartGetByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
