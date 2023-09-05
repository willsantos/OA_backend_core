using FluentValidation;
using OA_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Validations
{
    public class ProfessorValidator : AbstractValidator<Professor>
    {
        public ProfessorValidator()
        {
            RuleFor(u => u.Formacao)
               .NotEmpty()
               .WithMessage("Formacao precisa ser preenchida");

            RuleFor(u => u.Experiencia)
                .NotEmpty()
                .WithMessage("Experiencia precisa ser preenchida");

            RuleFor(u => u.Biografia)
            .NotEmpty()
            .WithMessage("Experiencia precisa ser preenchida");

            RuleFor(u => u.Foto)
            .NotEmpty()
            .WithMessage("É necessário anexar a foto");

        }
    }
}
