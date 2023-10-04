using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class AvaliacaoUsuarioEntityMap
	{
		public void Configure(EntityTypeBuilder<AvaliacaoUsuario> builder)
		{
			//Ignora prop de validação
			builder.Ignore(a => a.Valid).Ignore(a => a.ValidationResult);
			builder.HasKey(a => new { a.AvaliacaoId, a.UsuarioId});		

			//Mapeamento de relações
			builder.HasOne(a => a.Usuario)
				.WithMany()
				.HasForeignKey(a => a.UsuarioId);
			builder.HasOne(a => a.Avaliacao)
				.WithMany()
				.HasForeignKey(a => a.AvaliacaoId);
		}
	}
}
