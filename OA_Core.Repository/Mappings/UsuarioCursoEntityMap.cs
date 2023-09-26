using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class UsuarioCursoEntityMap
    {
        public void Configure(EntityTypeBuilder<UsuarioCurso> builder)
        {
			//Ignora prop de validação
			builder.Ignore(a => a.Valid).Ignore(a => a.ValidationResult).Ignore(a => a.DataDelecao);

			//Mapeamento de relações
			builder.HasOne(cp => cp.Curso)
				.WithMany(c => c.UsuarioCursos)
				.HasForeignKey(cp => cp.CursoId);

			builder.HasOne(cp => cp.Usuario)
				.WithMany(p => p.UsuarioCursos)
				.HasForeignKey(cp => cp.UsuarioId);

			builder.HasKey(c => c.Id);
		}
	}
}
