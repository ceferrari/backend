namespace Backend.Domain.Core.Objects
{
    public interface IEntity
    {

    }

    public interface IEntity<out TIdentity> : IEntity
    {

    }
}
