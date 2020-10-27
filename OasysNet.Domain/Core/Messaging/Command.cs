using System;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;

namespace OasysNet.Domain.Core.Messaging
{
    public abstract class Command<TResponse> : Message, IRequest<TResponse>
    {
        public Guid Id { get; set; }

        public DateTime Timestamp { get; protected set; } = DateTime.Now;

        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; } = new ValidationResult();

        public virtual bool IsValid() => ValidationResult.IsValid;
    }

    public abstract class Command : Command<ValidationResult>
    {
    }
}