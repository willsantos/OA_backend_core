namespace OA_Core.Domain.ValueObjects
{
    public class Cpf
    {
        public string Registro { get; private set; }

        public Cpf(string registro)
        {
			//if (!Verificar())
			//{
			//	throw new ArgumentException("CPF inválido.", nameof(registro));
			//}

			Registro = registro;
        }
		public string FormatoCpf => ExibirFormatado();
		public string FormatacaoNumeros => Exibir();
		public override string ToString() => FormatoCpf;	
		public string Exibir()
        {
			var cpf = new string(Registro.Where(char.IsDigit).ToArray());
			return $"{cpf.Substring(0, 3)}{cpf.Substring(3, 3)}{cpf.Substring(6, 3)}{cpf.Substring(9)}";
		}		
		public string ExibirFormatado()
        {			
			var cpf = new string(Registro.Where(char.IsDigit).ToArray());
			return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9)}";
		}		
		public bool Verificar()
        {
			if (string.IsNullOrWhiteSpace(Registro))
			{
				return false;
			}

			var cpf = Exibir();
		
			if (cpf.Length != 11)
			{
				return false;
			}

			if (cpf.Distinct().Count() == 1)
			{
				return false;
			}

			int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digito;
			int soma;
			int resto;
			cpf = cpf.Trim();			
			if (cpf.Length != 11)
				return false;
			tempCpf = cpf.Substring(0, 9);
			soma = 0;

			for (int i = 0; i < 9; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString();
			tempCpf = tempCpf + digito;
			soma = 0;
			for (int i = 0; i < 10; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();
			return cpf.EndsWith(digito);
		}	
	}
}
