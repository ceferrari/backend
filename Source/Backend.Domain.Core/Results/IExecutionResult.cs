using Backend.Domain.Core.Events;

namespace Backend.Domain.Core.Results
{
    public interface IExecutionResult
    {

    }

    public interface IExecutionResult<out TResult> : IEvent, IExecutionResult
    {
        bool Success { get; }
        TResult Data { get; }
        string[] Errors { get; }
    }
}
