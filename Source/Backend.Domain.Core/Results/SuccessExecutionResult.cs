using System;
using Backend.Domain.Core.Events;

namespace Backend.Domain.Core.Results
{
    public class SuccessExecutionResult<TResult> : Event, IExecutionResult<TResult>
    {
        public SuccessExecutionResult(Type type, TResult result)
            : base(type, result)
        {
            Success = true;
            Data = result;
            Errors = null;
        }

        public bool Success { get; }
        public TResult Data { get; }
        public string[] Errors { get; }
    }
}
