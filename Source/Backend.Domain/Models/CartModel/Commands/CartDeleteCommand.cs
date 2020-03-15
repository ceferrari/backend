using System;
using Backend.Domain.Core.Commands;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartDeleteCommand : Command<Guid, bool>
    {
        public CartDeleteCommand(Guid id)
            : base(id)
        {
            Validator = new CartDeleteCommandValidator(this);
        }
    }
}
