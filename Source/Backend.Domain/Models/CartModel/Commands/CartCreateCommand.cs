using System;
using Backend.Domain.Core.Commands;
using Backend.Domain.ViewModels.CartViewModel;
using Backend.Shared.Constants;
using Backend.Shared.Extensions;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartCreateCommand : Command<Guid, Cart>
    {
        public CartCreateCommand(CartViewModel cart)
            : base(Comb.NewComb())
        {
            CustomerId = cart.CustomerId;
            Status = DomainValues.Cart.Status.Pending;
            Item = new CartCreateUpdateItem
            {
                Sku = cart.Item.Sku,
                Quantity = cart.Item.Quantity
            };

            Validator = new CartCreateCommandValidator(this);
        }

        public string CustomerId { get; }
        public string Status { get; }
        public CartCreateUpdateItem Item { get; }
    }
}
