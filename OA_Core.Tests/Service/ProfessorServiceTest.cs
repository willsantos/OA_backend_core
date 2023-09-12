using AutoFixture;
using AutoMapper;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
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

		[Fact(DisplayName = "Cria um Curso Válido")]
		public async Task CriarProfessor()
		{
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var MockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var professorService = new ProfessorService(_mapper, mockProfessorRepository, MockUsuarioRepository, _notifier);

			var usuario = _fixture.Create<Usuario>();
			var professorRequest = _fixture.Create<ProfessorRequest>();

			MockUsuarioRepository.FindAsync(Arg.Any<Guid>()).Returns(usuario);
			mockProfessorRepository.AddAsync(Arg.Any<Professor>()).Returns(Task.CompletedTask);

			var result = await professorService.PostProfessorAsync(professorRequest);
			Assert.NotNull(result);
			Assert.IsType<Guid>(result);
		}

		[Fact(DisplayName = "Obtém todos os Professores com sucesso")]
		public async Task ObterTodosProfessores()
		{
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var cursoService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);

			var professores = _fixture.CreateMany<Professor>(5);
			mockProfessorRepository.ListPaginationAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(professores);

			var result = await cursoService.GetAllProfessoresAsync(1, 5);

			Assert.Equal(5, result.Count());
		}

		[Fact(DisplayName = "Obtém um Professor por Id com sucesso")]
		public async Task ObterProfessorPorId()
		{
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var cursoService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);

			var professor = _fixture.Create<Professor>();
			mockProfessorRepository.FindAsync(Arg.Any<Guid>()).Returns(professor);

			var result = await cursoService.GetProfessorByIdAsync(professor.Id);

			Assert.Equal(professor.Id, result.Id);
		}

		[Fact(DisplayName = "Atualiza professor com sucesso")]
		public async Task AtualizarProfessor()
		{
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var cursoService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);

			var professorRequestPut = _fixture.Create<ProfessorRequestPut>();
			var professor = _fixture.Create<Professor>();

			mockProfessorRepository.FindAsync(Arg.Any<Guid>()).Returns(professor);

			await cursoService.PutProfessorAsync(professor.Id, professorRequestPut);

			await mockProfessorRepository.Received().EditAsync(Arg.Is<Professor>(c => c.Formacao == professorRequestPut.Formacao));
		}

		[Fact(DisplayName = "Deleta um Professor Válido")]
		public async Task DeletarProfessor()
		{
			var mockProfessorRepository = Substitute.For<IProfessorRepository>();
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var cursoService = new ProfessorService(_mapper, mockProfessorRepository, mockUsuarioRepository, _notifier);

			var professor = _fixture.Create<Professor>();
			mockProfessorRepository.FindAsync(Arg.Any<Guid>()).Returns(professor);

			await cursoService.DeleteProfessorAsync(professor.Id);

			await mockProfessorRepository.Received().RemoveAsync(professor);
		}
	}
}
