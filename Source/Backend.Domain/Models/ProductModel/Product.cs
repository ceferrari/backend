using System;
using Backend.Domain.Core.Objects;

namespace Backend.Domain.Models.ProductModel
{
    public class Product : Entity<Guid>
    {
        public Product(Guid id)
            : base(id)
        {
            Price = new ProductPrice();
        }

        public string Sku { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public ProductPrice Price { get; set; }
    }
}
