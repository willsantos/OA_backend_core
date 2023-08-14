using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Entities
{
    public class Usuario
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public DateOnly data_nascimento { get; set; }
        public string telefone { get; set; }
        public string endereco { get; set; }
    }
}
