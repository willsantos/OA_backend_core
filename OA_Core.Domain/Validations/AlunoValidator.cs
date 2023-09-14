using FluentValidation;
using OA_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Validations
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        public AlunoValidator()
        {
            //RuleFor(u => u.UsuarioId)
            //    .NotEmpty()
            //    .WithMessage("Id de usuário não pode ser vazio");

			RuleFor(a => a.Foto)
				.NotEmpty()
				.WithMessage("É necessário anexar a foto");

			RuleFor(a => a.Cpf)
				.NotEmpty()
				.WithMessage("Cpf é obrigatório");
		}
    }
}
