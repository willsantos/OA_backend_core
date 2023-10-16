using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class AvaliacaoEntityMap
	{
		public void Configure(EntityTypeBuilder<Avaliacao> builder)
		{
			//Ignora prop de validação
			builder.Ignore(a => a.Valid).Ignore(a => a.ValidationResult);

			//Filtro para não buscar entidades deletadas
			builder.HasQueryFilter(c => c.DataDelecao == null);	

		}
	}
}
