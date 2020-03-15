using System.Collections.Generic;
using Backend.Domain.Core.Objects;
using Backend.Domain.Models.CartModel;

namespace Backend.Domain.Models.InvoiceModel
{
    public class InvoiceAggregate : ValueObject
    {
        public InvoiceAggregate(Invoice invoice, Cart cart)
        {
            Invoice = invoice;
            Cart = cart;
        }

        public Invoice Invoice { get; }
        public Cart Cart { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Invoice.XTeamControl;
            yield return Invoice.CurrencyCode;
            yield return Cart.Id;
        }
    }
}
