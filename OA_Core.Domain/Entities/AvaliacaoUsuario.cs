using FluentValidation;
using OA_Core.Domain.Validations;
using System;
using FluentValidation.Results;

namespace OA_Core.Domain.Entities
{
	public class AvaliacaoUsuario 
	{
		public AvaliacaoUsuario(Guid avaliacaoId, 
								Avaliacao avaliacao, 
								Guid usuarioId, 
								Usuario usuario, 
								double? notaObtida, 
								bool aprovado, 								 
								DateTime? fim)
		{
			AvaliacaoId = avaliacaoId;
			Avaliacao = avaliacao;
			UsuarioId = usuarioId;
			Usuario = usuario;
			NotaObtida = notaObtida;
			Aprovado = aprovado;
			Inicio = DateTime.Now;
			Fim = fim;
			Validate(this, new AvaliacaoUsuarioValidator());
		}

		public Guid AvaliacaoId{ get; set; }
		public virtual Avaliacao Avaliacao { get; set; }
		public Guid UsuarioId { get; set; }
		public virtual Usuario Usuario { get; set; }
		public double? NotaObtida { get; set; }
		public bool Aprovado { get; set; }
		public DateTime Inicio { get; set; }
		public DateTime? Fim { get; set; }
		public bool Valid { get; set; }
		public ValidationResult ValidationResult { get; set; }

		public bool Validate<T>(T model, AbstractValidator<T> validator)
		{
			ValidationResult = validator.Validate(model);
			return Valid = ValidationResult.IsValid;
		}
	}
}
