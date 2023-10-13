using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class UsuarioAulaEntityMap
    {
        public void Configure(EntityTypeBuilder<UsuarioAula> builder)
        {
			//Mapeamento de relações
			builder.HasOne(cp => cp.Aula)
				.WithMany(c => c.UsuarioAulas)
				.HasForeignKey(cp => cp.AulaId);

			builder.HasOne(cp => cp.Usuario)
				.WithMany(p => p.UsuarioAulas)
				.HasForeignKey(cp => cp.UsuarioId);

			builder.HasKey(c => new { c.AulaId, c.UsuarioId });
		}
	}
}
