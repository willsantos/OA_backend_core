using FluentValidation;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Validations
{
	public class AvaliacaoUsuarioValidator : AbstractValidator<AvaliacaoUsuario>
	{
		public AvaliacaoUsuarioValidator()
		{
			RuleFor(u => u.AvaliacaoId)
				.NotEmpty()
				.WithMessage("Id de avaliacao precisa ser preenchido");
			RuleFor(u => u.UsuarioId)
				.NotEmpty()
				.WithMessage("Id de usuario precisa ser preenchido");
		}
	}
}
