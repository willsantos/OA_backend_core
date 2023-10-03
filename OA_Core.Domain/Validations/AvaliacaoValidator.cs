using FluentValidation;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Validations
{
	public class AvaliacaoValidator : AbstractValidator<Avaliacao>
	{
		public AvaliacaoValidator()
		{	
			RuleFor(u => u.Nome)
				.NotEmpty()
				.WithMessage("Nome precisa ser preenchido");
		}
	
	}
}
