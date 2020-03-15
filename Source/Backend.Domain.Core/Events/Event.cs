using System;
using Backend.Shared.Extensions;

namespace Backend.Domain.Core.Events
{
    public abstract class Event : IEvent
    {
        public string Timestamp { get; }
        public string Type { get; }
        public object Message { get; }
        public string UniqueId { get; }
        public string CorrelationId { get; }
        public string Version { get; }

        protected Event(Type type, object message, int version = 1)
        {
            Timestamp = DateTime.UtcNow.ToString("O");
            Type = type.Name;
            Message = message;
            UniqueId = Comb.NewComb().ToString();
            CorrelationId = Comb.NewComb().ToString();
            Version = version.ToString();
        }
    }
}
