using FluentValidation;
using OasysNet.Domain.Core.Models;

namespace OasysNet.Domain.Models
{
    public class Client : Entity<Client>
    {
        public string Name { get; set; }

        public override bool IsValid()
        {
            RuleFor(c => c.Name)
                .MaximumLength(200)
                .NotEmpty();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}