using OA_Core.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Entities
{
	public class CursoProfessor : Entidade
	{

		public CursoProfessor(Guid professorId, bool responsavel)
		{
			Id = Guid.NewGuid();
			ProfessorId = professorId;
			Responsavel = responsavel;
			Validate(this, new CursoProfessorValidator());
		}

		public Guid CursoId { get; set; }
		public Curso Curso { get; set; }

		public Guid ProfessorId { get; set; }
		public Professor Professor { get; set; }

		public bool Responsavel { get; set; }
	}
}
