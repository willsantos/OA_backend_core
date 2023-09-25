using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Service;
using OA_Core.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Service
{
	[Trait("Service", "Professor Service")]
	public class ProfessorServiceTest
	{
		private readonly IMapper _mapper;
		private readonly Fixture _fixture;
		private readonly INotificador _notifier;

		public ProfessorServiceTest()
		{
			_fixture = FixtureConfig.GetFixture();
			_mapper = MapperConfig.Get();
			_notifier = Substitute.For<INotificador>();
		}

		[Fact(DisplayName = "Cria um Professor Válido")]
		public async Task CriarProfessor()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var professorService = new ProfessorService(_mapper, mockProfessorRepository, MockUsuarioRepository, _notifier);
			var usuario = _fixture.Create<Usuario>();
			var professorRequest = _fixture.Create<ProfessorRequest>();

			//Act
			MockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			mockProfessorRepository.AdicionarAsync(Arg.Any<Professor>()).Returns(Task.CompletedTask);

			//Assert
			var result = await professorService.PostProfessorAsync(professorRequest);
			result.Should().NotBe(Guid.Empty);
		}

		[Fact(DisplayName = "Obtém todos os Professores com sucesso")]
		public async Task ObterTodosProfessores()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var cursoService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);
			var professores = _fixture.CreateMany<Professor>(5);
			mockProfessorRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(professores);
			//Act
			var result = await cursoService.GetAllProfessoresAsync(1, 5);
			//Assert
			result.Should().HaveCount(5);
		}

		[Fact(DisplayName = "Obtém um Professor por Id com sucesso")]
		public async Task ObterProfessorPorId()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var cursoService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);
			var professor = _fixture.Create<Professor>();
			var professorResponse = _mapper.Map<ProfessorResponse>(professor);
			//Act
			mockProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
			var result = await cursoService.GetProfessorByIdAsync(professor.Id);
			//Assert
			result.Should().BeEquivalentTo(professorResponse);
		}

		[Fact(DisplayName = "Atualiza professor com sucesso")]
		public async Task AtualizarProfessor()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var cursoService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);
			var professorRequestPut = _fixture.Create<ProfessorRequestPut>();
			var professor = _fixture.Create<Professor>();
			//Act
			mockProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
			await cursoService.PutProfessorAsync(professor.Id, professorRequestPut);
			//Assert
			await mockProfessorRepository.Received().EditarAsync(Arg.Is<Professor>(c => c.Formacao == professorRequestPut.Formacao));
		}

		[Fact(DisplayName = "Deleta um Professor Válido")]
		public async Task DeletarProfessor()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var professorService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);

			var professor = _fixture.Create<Professor>();
			//Act
			mockProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
			await professorService.DeleteProfessorAsync(professor.Id);
			//Assert
			await mockProfessorRepository.Received().EditarAsync(professor);
		}

		[Fact(DisplayName = "Cria um Professor com UsuarioId inválido")]
		public async Task CriarProfessorComUsuarioIdInvalido()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var professorService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);
			var professorRequest = _fixture.Create<ProfessorRequest>();
			//Assert
			await Assert.ThrowsAsync<InformacaoException>(() => professorService.PostProfessorAsync(professorRequest));
		}

		[Fact(DisplayName = "Tenta obter Professor por Id inválido")]
		public async Task TentaObterProfessorPorIdInvalido()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var professorService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);
			//Assert
			await Assert.ThrowsAsync<InformacaoException>(() => professorService.GetProfessorByIdAsync(Guid.NewGuid()));
		}

		[Fact(DisplayName = "Tenta Deletar Professor com Id inválido")]
		public async Task TentaDeletarProfessorComIdInvalido()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var professorService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);
			//Assert
			await Assert.ThrowsAsync<InformacaoException>(() => professorService.DeleteProfessorAsync(Guid.NewGuid()));
		}

		[Fact(DisplayName = "Tenta Criar Professor com Campos inválidos")]
		public async Task TentaCriarProfessorComCamposInvalidos()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var professorService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);
			var usuario = _fixture.Create<Usuario>();
			var professorRequest = _fixture.Create<ProfessorRequest>();
			professorRequest.Formacao = string.Empty;
			//Act
			mockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			await professorService.PostProfessorAsync(professorRequest);
			//Assert
			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Formacao" && e.ErrorMessage == "Formacao precisa ser preenchida")));
		}

		[Fact(DisplayName = "Tenta atualizar Professor com Campos inválidos")]
		public async Task TentaAtualizarProfessorComCamposInvalidos()
		{
			//Arrange
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var professorService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);
			var professorRequestPut = _fixture.Create<ProfessorRequestPut>();
			professorRequestPut.Biografia = string.Empty;
			var professor = _fixture.Create<Professor>();
			//Act
			mockProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
			await professorService.PutProfessorAsync(professor.Id, professorRequestPut);
			//Assert
			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Biografia" && e.ErrorMessage == "Biografia precisa ser preenchida")));
		}
	}
}
