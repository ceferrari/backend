using System;
using Backend.Domain.Core.Commands;
using Backend.Domain.ViewModels.CartViewModel;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartUpdateCommand : Command<Guid, Cart>
    {
        public CartUpdateCommand(Guid id, CartItemViewModel item)
            : base(id)
        {
            Item = new CartCreateUpdateItem
            {
                Sku = item.Sku,
                Quantity = item.Quantity
            };

            Validator = new CartUpdateCommandValidator(this);
        }

        public CartCreateUpdateItem Item { get; }
    }
}
