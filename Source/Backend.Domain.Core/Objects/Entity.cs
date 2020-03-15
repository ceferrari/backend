namespace Backend.Domain.Core.Objects
{
    public abstract class Entity<TIdentity> : Identity<TIdentity>, IEntity
    {
        protected Entity(TIdentity id) : base(id) { }
    }
}
