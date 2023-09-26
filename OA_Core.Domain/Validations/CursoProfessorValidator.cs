using FluentValidation;
using OA_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Validations
{
    public class CursoProfessorValidator : AbstractValidator<CursoProfessor>
    {
        public CursoProfessorValidator()
        {
            RuleFor(u => u.ProfessorId)
                .NotEmpty()
                .WithMessage("ProfessorId não pode ser nulo");
        }
    }
}
