﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class ProfessorEntityMap
	{
		public void Configure(EntityTypeBuilder<Professor> builder)
		{
			builder.Ignore(c => c.Valid).Ignore(c => c.ValidationResult);
			builder.Property(c => c.UsuarioId).HasColumnName("usuario_id");
			builder.Property(c => c.Formacao).HasColumnName("formacao");
			builder.Property(c => c.Experiencia).HasColumnName("experiencia");
			builder.Property(c => c.Foto).HasColumnName("foto");
			builder.Property(c => c.Biografia).HasColumnName("biografia");
			builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");
			builder.Property(c => c.DataAlteracao).HasColumnName("data_alteracao");
			builder.Property(c => c.DataDelecao).HasColumnName("data_delecao");
		}
	}
}
