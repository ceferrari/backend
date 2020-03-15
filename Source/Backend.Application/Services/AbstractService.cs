using Backend.Application.Results;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Backend.Domain.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Services
{
    public abstract class AbstractService
    {
        protected readonly IBus Bus;
        protected readonly INotificationHandler NotificationHandler;

        protected AbstractService(IBus bus, INotificationHandler notificationHandler)
        {
            Bus = bus;
            NotificationHandler = notificationHandler;
        }

        protected IList<string> GetNotifications() => NotificationHandler.Notifications.Select(x => x.Message.ToString()).ToList();
        protected IActionResult Result<TResult>(HttpStatusCode successHttpStatusCode, IExecutionResult<TResult> result) =>
            NotificationHandler.HasNotifications()
                ? new FailureActionResult(HttpStatusCode.BadRequest, GetNotifications())
                : result.Success
                    ? (IActionResult)new SuccessActionResult(successHttpStatusCode, result.Data)
                    : (IActionResult)new FailureActionResult(HttpStatusCode.InternalServerError, result.Errors);
    }
}
