using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class ProfessorEntityMap
	{
		public void Configure(EntityTypeBuilder<Professor> builder)
		{
			//Ignora prop de validação
			builder.Ignore(p => p.Valid).Ignore(p => p.ValidationResult);

			//Filtro para não buscar entidades deletadas
			builder.HasQueryFilter(c => c.DataDelecao == null);

			//Mapeamento de relações
			builder.HasOne(p => p.Usuario)
				.WithMany()
				.HasForeignKey(p => p.UsuarioId);
		}
	}
}