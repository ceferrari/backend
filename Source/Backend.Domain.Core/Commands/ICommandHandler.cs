using Backend.Domain.Core.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Domain.Core.Commands
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<in TCommand, TResult> : ICommandHandler
    {
        Task<IExecutionResult<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
