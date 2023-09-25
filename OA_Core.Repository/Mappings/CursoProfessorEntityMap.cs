using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class CursoProfessorEntityMap
    {
        public void Configure(EntityTypeBuilder<CursoProfessor> builder)
        {
			//Ignora prop de validação
			builder.Ignore(a => a.Valid).Ignore(a => a.ValidationResult);

			//Filtro para não buscar entidades deletadas
			builder.HasQueryFilter(c => c.DataDelecao == null);

			//Mapeamento de relações
			builder.HasOne(cp => cp.Curso)
				.WithMany(c => c.CursoProfessores)
				.HasForeignKey(cp => cp.CursoId);

			builder.HasOne(cp => cp.Professor)
				.WithMany(p => p.CursoProfessores)
				.HasForeignKey(cp => cp.ProfessorId);

			builder.HasKey(c => c.Id);
		}
	}
}
