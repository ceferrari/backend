using System.Collections.Generic;
using Backend.Domain.Core.Objects;
using FluentValidation.Results;

namespace Backend.Domain.Core.Commands
{
    public abstract class Command<TIdentity, TResult> : Identity<TIdentity>, ICommand<TResult>
    {
        protected Command(TIdentity id) : base(id) { }

        public ICommandValidator Validator { get; set; }
        public bool IsValid() => Validator.ValidationResult.IsValid;
        public IList<ValidationFailure> GetErrors() => Validator.ValidationResult.Errors;
    }
}
