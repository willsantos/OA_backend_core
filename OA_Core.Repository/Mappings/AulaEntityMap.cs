using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;
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

            builder.Ignore(c => c.Valid).Ignore(c => c.ValidationResult);
            builder.Property(c => c.CursoId).HasColumnName("curso_id");
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");
            builder.Property(c => c.DataAlteracao).HasColumnName("data_alteracao");
            builder.Property(c => c.DataDelecao).HasColumnName("data_delecao");
        }
    }
}
