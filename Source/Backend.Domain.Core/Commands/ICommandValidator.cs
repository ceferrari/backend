using System;
using FluentValidation;
using FluentValidation.Results;

namespace Backend.Domain.Core.Commands
{
    public interface ICommandValidator
    {
        ValidationResult ValidationResult { get; set; }
        bool ValidateGuid(Guid guid);
    }

    public interface ICommandValidator<in TCommand> : IValidator<TCommand>, ICommandValidator
    {

    }
}
