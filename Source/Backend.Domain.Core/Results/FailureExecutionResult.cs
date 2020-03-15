using System;
using Backend.Domain.Core.Events;

namespace Backend.Domain.Core.Results
{
    public class FailureExecutionResult<TResult> : Event, IExecutionResult<TResult>
    {
        public FailureExecutionResult(Type type, params string[] result)
            : base(type, result)
        {
            Success = false;
            Data = default;
            Errors = result;
        }

        public bool Success { get; }
        public TResult Data { get; }
        public string[] Errors { get; }
    }
}
