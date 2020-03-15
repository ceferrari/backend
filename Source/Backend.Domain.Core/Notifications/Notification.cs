using System;
using Backend.Domain.Core.Events;

namespace Backend.Domain.Core.Notifications
{
    public class Notification : Event, INotification
    {
        public Notification(Type type, object message, int version = 1)
            : base(type, message, version)
        {
            
        }
    }
}
