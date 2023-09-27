using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Service;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Service
{
	[Trait("Service", "Assinatura Service")]
	public class AssinaturaServiceTest
	{
		private readonly IMapper _mapper;
		private readonly Fixture _fixture;
		private readonly INotificador _notifier;

		public AssinaturaServiceTest()
		{
			_fixture = FixtureConfig.GetFixture();
			_mapper = MapperConfig.Get();
			_notifier = Substitute.For<INotificador>();
		}

		[Fact(DisplayName = "Cria um Assinatura Válido")]
		public async Task CriarAssinatura()
		{
			//Arrange
			var mockAssinaturaRepository = Substitute.For<IAssinaturaRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var assinaturaService = new AssinaturaService(_mapper, mockAssinaturaRepository, MockUsuarioRepository, _notifier);
			var usuario = _fixture.Create<Usuario>();
			var assinaturaRequest = _fixture.Create<AssinaturaRequest>();

			//Act
			MockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			mockAssinaturaRepository.AdicionarAsync(Arg.Any<Assinatura>()).Returns(Task.CompletedTask);

			//Assert
			var result = await assinaturaService.AdicionarAssinaturaAsync(assinaturaRequest);
			result.Should().NotBe(Guid.Empty);
		}

		[Fact(DisplayName = "Cancela assinatura com sucesso")]
		public async Task CancelaAssinatura()
		{
			//Arrange
			var mockAssinaturaRepository = Substitute.For<IAssinaturaRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var assinaturaService = new AssinaturaService(_mapper, mockAssinaturaRepository, mockUsuarioRepository, _notifier);
			var assinaturaRequestPut = _fixture.Create<AssinaturaCancelamentoRequest>();
			var professor = _fixture.Create<Assinatura>();
			//Act
			mockAssinaturaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
			await assinaturaService.CancelarAssinaturaAsync(professor.Id, assinaturaRequestPut);
			//Assert
			await mockAssinaturaRepository.Received().EditarAsync(Arg.Is<Assinatura>(c => c.MotivoCancelamento == assinaturaRequestPut.MotivoCancelamento));
		}
	}
}
