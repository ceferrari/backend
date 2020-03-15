using System;
using Backend.Domain.Core.Commands;

namespace Backend.Domain.Models.CartModel.Commands
{
    public sealed class CartCheckoutCommand : Command<Guid, bool>
    {
        public CartCheckoutCommand(Guid id, string currencyCode, string xTeamControl)
            : base(id)
        {
            XTeamControl = xTeamControl;
            CurrencyCode = currencyCode;

            Validator = new CartCheckoutCommandValidator(this);
        }

        public string XTeamControl { get; }
        public string CurrencyCode { get; }
    }
}
