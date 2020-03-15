using Backend.Domain.Core.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Domain.Core.Queries
{
    public interface IQueryHandler
    {

    }

    public interface IQueryHandler<in TQuery, TResult> : IQueryHandler
        where TQuery : IQuery<TResult>
    {
        Task<IExecutionResult<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
