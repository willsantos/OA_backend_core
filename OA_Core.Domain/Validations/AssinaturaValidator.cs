using FluentValidation;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Validations
{
	public class AssinaturaValidator : AbstractValidator<Assinatura>
	{
		public AssinaturaValidator()
		{
			RuleFor(a => a.UsuarioId)
				.NotEmpty()
				.WithMessage("É necessário usuarioId");
		}
	}
}
