using OA_Core.Domain.Enums;
using OA_Core.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Entities
{
	public class UsuarioCurso : Entidade
	{

		public UsuarioCurso(Guid cursoId, Guid usuarioId,string status, int progresso)
		{
			Id = Guid.NewGuid();
			CursoId = cursoId;
			UsuarioId = usuarioId;
			Status = status;
			Progresso = progresso;
			DataCriacao = DateTime.Now;
			Validate(this, new UsuarioCursoValidator());
		}

		public Guid UsuarioId { get; set; }
		public Usuario Usuario { get; set; }
		public Guid CursoId { get; set; }
		public Curso Curso { get; set; }
		public string Status { get; set; }
		public int Progresso { get; set; }
	}
}
