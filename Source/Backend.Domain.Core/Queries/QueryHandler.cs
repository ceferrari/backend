using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Core.Results;

namespace Backend.Domain.Core.Queries
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        //protected readonly IReadOnlyRepository ReadOnlyRepository;

        //protected QueryHandler(IReadOnlyRepository readOnlyRepository)
        //{
        //    ReadOnlyRepository = readOnlyRepository;
        //}

        public abstract Task<IExecutionResult<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);

        //public void Dispose()
        //{
        //    ReadOnlyRepository.Dispose();
        //}
    }
}
