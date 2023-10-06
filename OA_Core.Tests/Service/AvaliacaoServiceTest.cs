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
	[Trait("Service", "Avaliacao Service")]
	public class AvaliacaoServiceTest
	{
		private readonly IMapper _mapper;
		private readonly Fixture _fixture;
		private readonly INotificador _notifier;

		public AvaliacaoServiceTest()
		{
			_fixture = FixtureConfig.GetFixture();
			_mapper = MapperConfig.Get();
			_notifier = Substitute.For<INotificador>();
		}
		[Fact(DisplayName = "Cria uma Aula válida")]
		public async Task AvaliacaoService_CriaAvaliacao_DeveCriar()
		{
			//Arrange
			var mockAvaliacaoRepository = Substitute.For<IAvaliacaoRepository>();
			var MockAulaRepository = Substitute.For<IAulaRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var MockAvaliacaoUsuarioRepository = Substitute.For<IAvaliacaoUsuarioRepository>();
			var avaliacaoService = new AvaliacaoService(mockAvaliacaoRepository, MockUsuarioRepository,  _notifier, _mapper, MockAulaRepository, MockAvaliacaoUsuarioRepository);
			var aula = _fixture.Create<Aula>();
			var avaliacaoRequest = _fixture.Create<AvaliacaoRequest>();

			//Act
			MockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);
			mockAvaliacaoRepository.AdicionarAsync(Arg.Any<Avaliacao>()).Returns(Task.CompletedTask);
			var result = await avaliacaoService.CadastrarAvaliacaoAsync(avaliacaoRequest);

			//Assert
			result.Should().NotBe(Guid.Empty);
		}
	}
}
