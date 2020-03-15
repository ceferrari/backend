namespace Backend.Domain.Core.Events
{
    public interface IEvent
    {
        string Timestamp { get; }
        string Type { get; }
        object Message { get; }
        string UniqueId { get; }
        string CorrelationId { get; }
        string Version { get; }
    }
}
