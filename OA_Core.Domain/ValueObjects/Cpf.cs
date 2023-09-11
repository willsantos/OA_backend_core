using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.ValueObjects
{
    public class Cpf
    {
        public string Registro { get; private set; }

        public Cpf(string registro)
        {
            Registro = registro;
        }

        public string Exibir()
        {
            throw new NotImplementedException();
        }

        public string ExibirFormatado()
        {
            throw new NotImplementedException();
        }

        public bool Verificar()
        {
            throw new NotImplementedException();
        }
    }
}
