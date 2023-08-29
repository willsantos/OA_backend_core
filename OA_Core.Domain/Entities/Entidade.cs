using FluentValidation;
using FluentValidation.Results;

namespace OA_Core.Domain.Entities
{
    public class Entidade
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataDelecao { get; set; }
        public bool Valid { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public bool Validate<T>(T model, AbstractValidator<T> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}
