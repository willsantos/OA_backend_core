using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class AssinaturaEntityMap
	{
		public void Configure(EntityTypeBuilder<Assinatura> builder)
		{
			builder.Ignore(a => a.Valid)
				   .Ignore(a => a.ValidationResult)
				   .Ignore(a => a.DataAlteracao)
				   .Ignore(a => a.DataDelecao);	
					

			//Mapeamento de relações
			builder.HasOne(a => a.Usuario)
				.WithMany()
				.HasForeignKey(a => a.UsuarioId);
		}
	}
}
