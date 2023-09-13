namespace OA_Core.Domain.ValueObjects
{
    public class Cpf
    {
        public string Registro { get; private set; }

        public Cpf(string registro)
        {
			if (!Verificar(registro))
			{
				throw new ArgumentException("CPF inválido.", nameof(registro));
			}

			Registro = registro;
        }
		public string Formatted => ExibirFormatado(Registro);		
		//Sobrescreve método toString() formatando para cpf
		public override string ToString() => Formatted;
		//Exibir() exibe o valor de CPF 
		public string Exibir(string cpf)
        {
			cpf = new string(cpf.Where(char.IsDigit).ToArray());
			return $"{cpf.Substring(0, 3)}{cpf.Substring(3, 3)}{cpf.Substring(6, 3)}{cpf.Substring(9)}";
		}

		//ExibirFormatado() exibe o valor de CPF formatado com pontuação e hífen.
		public string ExibirFormatado(string cpf)
        {
			//remove caracteres nao numericos
			cpf = new string(cpf.Where(char.IsDigit).ToArray());
			return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9)}";
		}

		//Verificar() (verifica se o CPF é valido) 
		public bool Verificar(string cpf)
        {
			if (string.IsNullOrWhiteSpace(cpf))
			{
				return false;
			}
			
			cpf = Exibir(cpf);
		
			if (cpf.Length != 11)
			{
				return false;
			}

			// Se todos os digitos forem iguais, retorna falso
			if (cpf.Distinct().Count() == 1)
			{
				return false;
			}

			// Cálculo do dígito verificador
			int[] numeros = cpf.Select(c => int.Parse(c.ToString())).ToArray();
			int soma1 = 0, soma2 = 0;

			for (int i = 0; i < 9; i++)
			{
				soma1 += numeros[i] * (10 - i);
				soma2 += numeros[i] * (11 - i);
			}

			int digito1 = (soma1 * 10) % 11;
			int digito2 = (soma2 * 10) % 11;

			if (digito1 == 10) digito1 = 0;
			if (digito2 == 10) digito2 = 0;

			return digito1 == numeros[9] && digito2 == numeros[10];
		}	
	}
}
