using System.Linq;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Notifications;
using Backend.Domain.Core.Results;
using System.Threading;
using System.Threading.Tasks;
using Backend.Shared.Constants;
using Serilog;

namespace Backend.Domain.Core.Commands
{
    public abstract class CommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand
    {
        protected CommandHandler(IBus bus)
        {
            Bus = bus;
        }

        protected readonly IBus Bus;

        public abstract Task<IExecutionResult<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken);

        protected FailureExecutionResult<TResult> ValidationErrors(TCommand command)
        {
            var errors = command.GetErrors().Select(x => x.ErrorMessage).ToArray();

            Log.Warning($"{ErrorMessages.ValidationError} {{data}}", errors);

            foreach (var error in errors)
            {
                Bus.NotifyAsync(new Notification(command.GetType(), error));
            }

            return  new FailureExecutionResult<TResult>(command.GetType(), errors);
        }

        protected FailureExecutionResult<TResult> ExecutionError(TCommand command, string message)
        {
            Log.Warning($"{ErrorMessages.ExecutionError} {{data}}", message);

            Bus.NotifyAsync(new Notification(command.GetType(), message));

            return new FailureExecutionResult<TResult>(command.GetType(), message);
        }
    }
}
