namespace Backend.Domain.Core.Objects
{
    public interface IIdentity
    {
        object Id { get; set; }
    }

    public interface IIdentity<out TIdentity> : IIdentity
    {
        new TIdentity Id { get; }
    }
}
