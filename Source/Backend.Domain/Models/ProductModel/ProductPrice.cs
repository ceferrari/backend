using System.Collections.Generic;
using Backend.Domain.Core.Objects;

namespace Backend.Domain.Models.ProductModel
{
    public class ProductPrice : ValueObject
    {
        public long? Amount { get; set; }
        public long? Scale { get; set; }
        public string CurrencyCode { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Scale;
            yield return CurrencyCode;
        }
    }
}