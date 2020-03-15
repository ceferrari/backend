using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Backend.Domain.Core.Notifications
{
    public interface INotificationHandler : IDisposable
    {
        ReadOnlyCollection<INotification> Notifications { get; }
        Task HandleAsync(INotification notification);
        bool HasNotifications();
    }
}
