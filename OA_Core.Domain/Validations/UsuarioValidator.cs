using FluentValidation;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email invalido");

            RuleFor(u => u.Nome)
                .NotEmpty()
                .WithMessage("Nome precisa ser preenchido");
        }
    }
}
