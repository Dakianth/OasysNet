using System;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace OasysNet.Domain.Core.Models
{
    public abstract class Entity<T> : AbstractValidator<T>
           where T : Entity<T>
    {
        public Guid Id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; } = new ValidationResult();

        public virtual bool IsValid() => true;

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (compareTo is null)
                return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id={Id}]";
    }
}