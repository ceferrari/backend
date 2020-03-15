using System;
using System.Collections.Generic;
using Backend.Domain.Core.Objects;
using Backend.Domain.Models.ProductModel;

namespace Backend.Domain.Models.CartModel
{
    public class CartItem : Entity<Guid>
    {
        public CartItem(Guid id)
            : base(id)
        {

        }

        public CartItem(Product product)
            : this(product.Id)
        {
            Scale = product.Price.Scale;
            CurrencyCode = product.Price.CurrencyCode;
        }

        public long? Price { get; set; }
        public long? Scale { get; set; }
        public string CurrencyCode { get; set; }
        //[JsonIgnore(NullValueHandling = NullValueHandling.Ignore)]
        public Product Product { get; set; }
    }

    public class CartCreateUpdateItem : ValueObject
    {
        public string Sku { get; set; }
        public long? Quantity { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Sku;
            yield return Quantity;
        }
    }
}
