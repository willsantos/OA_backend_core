    using OA_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Interfaces.Repository
{
    public interface IAlunoRepository : IBaseRepository<Aluno>
    {
		Task<Aluno> FindByCpfAsync(string cpf);

	}
}
