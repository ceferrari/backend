using System.Collections.Generic;
using FluentValidation.Results;

namespace Backend.Domain.Core.Commands
{
    public interface ICommand
    {
        ICommandValidator Validator { get; set; }
        bool IsValid();
        IList<ValidationFailure> GetErrors();
    }

    public interface ICommand<TResult> : ICommand
    {
        
    }
}
