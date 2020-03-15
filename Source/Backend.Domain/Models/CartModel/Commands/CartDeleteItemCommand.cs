using System;
using Backend.Domain.Core.Commands;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartDeleteItemCommand : Command<Guid, bool>
    {
        public CartDeleteItemCommand(Guid id, Guid itemId)
            : base(id)
        {
            ItemId = itemId;

            Validator = new CartDeleteItemCommandValidator(this);
        }

        public Guid ItemId { get; }
    }
}
