using OA_Core.Domain.ValueObjects;


namespace OA_Core.Tests.ValueObject
{
    public class CpfVOTest
    {
        [Theory(DisplayName = "Verifica cpf com numeros repetidos", Skip = "TDD")]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        [InlineData("44444444444")]
        [InlineData("55555555555")]
        [InlineData("66666666666")]
        [InlineData("77777777777")]
        [InlineData("88888888888")]
        [InlineData("99999999999")]
        [InlineData("00000000000")]
        public void DeveRejeitarCpfComNumeroRepetido(string cpf)
        {
            bool resultado = Cpf.Verificar(cpf);
            Assert.False(resultado);
        }

        [Theory(DisplayName = "Verifica cpf com numeros repetidos", Skip = "TDD")]
        [InlineData("1111111111111")]
        [InlineData("222222222222")]
        [InlineData("6986484550")]
        [InlineData("444444444")]
        [InlineData("673530515900")]
        [InlineData("6281095207")]
        [InlineData("519553572859")]
        public void DeveRejeitarCpfComQuantidadeNumerosIncorreta(string cpf)
        {
            bool resultado = Cpf.Verificar(cpf);
            Assert.False(resultado);
        }

        [Theory(DisplayName = "Verifica CPF com caracteres inválidos", Skip = "TDD")]
        [InlineData("471B0316623")]
        [InlineData("660879960C7")]
        [InlineData("62841A95257")]
        [InlineData("67353051560")]
        [InlineData("6485227603$")]
        [InlineData("519.553.572-35")]
        [InlineData("50*31400141")]
        [InlineData("460(9123258")]
        [InlineData("5941)377497")]
        [InlineData("4143-589600")]
        public void DeveRejeitarCpfComCaracteresInvalidos(string cpf)
        {
            bool resultado = Cpf.Verificar(cpf);
            Assert.False(resultado);
        }

        [Theory(DisplayName ="Verifica CPF com verificador inválidos",Skip ="TDD")]
        [InlineData("47160316623")] 
        [InlineData("66087996047")] 
        [InlineData("62841095257")] 
        [InlineData("67353051560")] 
        [InlineData("64852276033")] 
        [InlineData("51955357235")] 
        [InlineData("50531400141")] 
        [InlineData("46089123258")] 
        [InlineData("59414377497")] 
        [InlineData("41438589600")]
        public void DeveRejeitarCpfInvalidosNoCalculoVerificador(string cpf)
        {
            bool resultado = Cpf.Verificar(cpf);
            Assert.False(resultado);
        }

        [Theory(DisplayName = "Verifica CPF com verificador valido", Skip = "TDD")]
        [InlineData("47160316673")]
        [InlineData("69864845500")]
        [InlineData("62841095207")]
        [InlineData("67353051590")]
        [InlineData("64852276013")]
        [InlineData("51955357285")]
        [InlineData("50531400140")]
        [InlineData("46089123218")]
        [InlineData("59414377492")]
        [InlineData("41438589670")]
        public void DeveAceitarTodosCpfValidos(string cpf)
        {
            bool resultado = Cpf.Verificar(cpf);
            Assert.True(resultado);
        }
        [Theory(DisplayName = "Retorna CPf com Formatação", Skip = "TDD")]
        [InlineData("47160316673", "471.603.166-73")]
        [InlineData("69864845500", "698.648.455-00")]
        [InlineData("62841095207", "628.410.952-07")]
        [InlineData("67353051590", "673.530.515-90")]
        [InlineData("64852276013", "648.522.760-13")]
        [InlineData("51955357285", "519.553.572-85")]
        [InlineData("50531400140", "505.314.001-40")]
        [InlineData("46089123218", "460.891.232-18")]
        [InlineData("59414377492", "594.143.774-92")]
        [InlineData("41438589670", "414.385.896-70")]
        public void DeveRetornarCpfComFormatacao(string cpf,string formatado)
        {
            var resultado = Cpf.ExibirFormatado(cpf);
            Assert.Equal(formatado, resultado);
        }

        [Theory(DisplayName = "Retorna CPf Sem Formatação", Skip = "TDD")]
        [InlineData("47160316673")]
        [InlineData("69864845500")]
        [InlineData("62841095207")]
        [InlineData("67353051590")]
        [InlineData("64852276013")]
        [InlineData("51955357285")]
        [InlineData("50531400140")]
        [InlineData("46089123218")]
        [InlineData("59414377492")]
        [InlineData("41438589670")]
        public void DeveRetornarCpfSemFormatacao(string cpf)
        {
            var resultado = Cpf.Exibir(cpf);
            Assert.Equal(cpf, resultado);
        }


    }
}
