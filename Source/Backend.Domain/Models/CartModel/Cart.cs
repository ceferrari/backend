using System;
using System.Collections.Generic;
using Backend.Domain.Core.Objects;

namespace Backend.Domain.Models.CartModel
{
    public class Cart : Entity<Guid>
    {
        public Cart(Guid id)
            : base(id)
        {
            Items = new List<CartItem>();
        }

        public string CustomerId { get; set; }
        public string Status { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
