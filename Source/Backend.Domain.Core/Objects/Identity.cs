using System;
using System.Collections.Generic;

namespace Backend.Domain.Core.Objects
{
    public abstract class Identity<TIdentity> : ValueObject, IIdentity<TIdentity>
    {
        protected Identity(TIdentity id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (Guid.TryParse(id.ToString(), out var guid) && guid == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            Id = id;
        }

        public TIdentity Id { get; private set; }
        object IIdentity.Id
        {
            get => Id;
            set => Id = (TIdentity)value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString()
        {
            return $"{GetType().Name.ToLowerInvariant()} [{Id}]";
        }
    }
}
