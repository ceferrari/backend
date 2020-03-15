using System;
using Backend.Domain.Core.Queries;

namespace Backend.Domain.Models.ProductModel.Queries
{
    public sealed class ProductGetByIdQuery : Query<Product, Product>
    {
        public ProductGetByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
