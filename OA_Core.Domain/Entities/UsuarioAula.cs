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
	public class UsuarioAula 
	{
		public Guid UsuarioId { get; set; }
		public Usuario Usuario { get; set; }
		public Guid AulaId { get; set; }
		public Aula Aula { get; set; }
		public bool Concluida { get; set; }
	}
}
