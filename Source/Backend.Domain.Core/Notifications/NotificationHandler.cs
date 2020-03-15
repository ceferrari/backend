using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.Core.Notifications
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly List<INotification> _notifications = new List<INotification>();
        public ReadOnlyCollection<INotification> Notifications => _notifications.AsReadOnly();

        public async Task HandleAsync(INotification notification)
        {
            _notifications.Add(notification);
            await Task.CompletedTask;
        }

        public bool HasNotifications()
        {
            return Notifications.Any();
        }

        public void Dispose()
        {
            _notifications.Clear();
        }
    }
}
