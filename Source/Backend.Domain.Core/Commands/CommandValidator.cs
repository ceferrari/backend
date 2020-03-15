using System;
using Backend.Shared.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace Backend.Domain.Core.Commands
{
    public abstract class CommandValidator<TCommand> : AbstractValidator<TCommand>, ICommandValidator
        where TCommand : ICommand
    {
        protected CommandValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            ValidatorOptions.DisplayNameResolver = (type, member, expression) => member?.Name.ToCamelCase();
        }

        public ValidationResult ValidationResult { get; set; }
        public bool ValidateGuid(Guid guid) => Guid.TryParse(guid.ToString(), out var result) && result != Guid.Empty;
    }
}
