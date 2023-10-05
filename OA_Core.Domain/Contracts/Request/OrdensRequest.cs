using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Contracts.Request
{
	public class OrdensRequest
	{
		public Guid Id { get; set; }
		public int Ordem { get; set; }

	}
}
