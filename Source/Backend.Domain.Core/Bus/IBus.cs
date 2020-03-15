using System.Threading.Tasks;
using Backend.Domain.Core.Commands;
using Backend.Domain.Core.Notifications;
using Backend.Domain.Core.Queries;
using Backend.Domain.Core.Results;

namespace Backend.Domain.Core.Bus
{
    public interface IBus
    {
        Task NotifyAsync(INotification notification);
        Task<IExecutionResult<TResult>> RequestAsync<TResult>(IQuery<TResult> query);
        Task<IExecutionResult<TResult>> SubmitAsync<TResult>(ICommand<TResult> command);
    }
}
