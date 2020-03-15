using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Commands;
using Backend.Domain.Core.Notifications;
using Backend.Domain.Core.Queries;
using Backend.Domain.Core.Results;
using Backend.Shared.Constants;
using Serilog;

namespace Backend.Domain.Core.Bus
{
    public sealed class Bus : IBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INotificationHandler _notificationHandler;

        public Bus(IServiceProvider serviceProvider, INotificationHandler notificationHandler)
        {
            _serviceProvider = serviceProvider;
            _notificationHandler = notificationHandler;
        }

        public async Task NotifyAsync(INotification notification)
        {
            await _notificationHandler.HandleAsync(notification);
        }

        public async Task<IExecutionResult<TResult>> RequestAsync<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            try
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
                var result = await ExecuteHandlerAsync<TResult>(handlerType, query, queryType);
                if (result.Success)
                {
                    Log.Information(queryType.Name + " {data}", result.Data);
                }
                return result;
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync<TResult>(ex, queryType);
            }
        }

        public async Task<IExecutionResult<TResult>> SubmitAsync<TResult>(ICommand<TResult> command)
        {
            var commandType = command.GetType();
            try
            {
                var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResult));
                var result = await ExecuteHandlerAsync<TResult>(handlerType, command, commandType);
                if (result.Success)
                {
                    Log.Information(commandType.Name + " {data}", result.Data);
                }
                return result;
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync<TResult>(ex, commandType);
            }
        }

        private async Task<IExecutionResult<TResult>> ExecuteHandlerAsync<TResult>(Type handlerType, object argument, Type argumentType)
        {
            var cancellationToken = new CancellationToken();
            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new ArgumentException($"{ErrorMessages.Arguments.HandlerNotRegisteredForType} {argumentType.Name}");
            }
            var method = handlerType?.GetMethod("HandleAsync", new[] { argumentType, cancellationToken.GetType() });
            if (method == null)
            {
                throw new ArgumentException($"{ErrorMessages.Arguments.MethodNotRegisteredForHandler} {handler}");
            }
            return await (Task<IExecutionResult<TResult>>)method.Invoke(handler, new[] { argument, cancellationToken });
        }

        private async Task<FailureExecutionResult<TResult>> HandleExceptionAsync<TResult>(Exception ex, Type argumentType)
        {
            await Task.CompletedTask;
            Log.Error(ex, $"{ErrorMessages.UnexpectedError} {{data}}");
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            var message = $"{ErrorMessages.UnexpectedError}: {ex.Message}";
            return new FailureExecutionResult<TResult>(argumentType, message);
        }
    }
}
