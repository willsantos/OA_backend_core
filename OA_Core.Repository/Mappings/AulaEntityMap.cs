using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Repository.Mappings
{
    public class AulaEntityMap
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
			//Ignora prop de validação
            builder.Ignore(a => a.Valid).Ignore(a => a.ValidationResult);

			//Filtro para não buscar entidades deletadas
			builder.HasQueryFilter(a => a.DataDelecao == null);

			//Mapeamento de relações
			builder.HasOne(a => a.curso)
				.WithMany()
				.HasForeignKey(a => a.CursoId);
		}
	}
}
