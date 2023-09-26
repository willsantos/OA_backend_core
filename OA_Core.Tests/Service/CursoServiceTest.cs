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

namespace OA_Core.Tests.Service
{
	[Trait("Service", "Curso Service")]
    public class CursoServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly INotificador _notifier;

        public CursoServiceTest()
        {
            _fixture = FixtureConfig.GetFixture();
            _mapper = MapperConfig.Get();
            _notifier = Substitute.For<INotificador>();
        }

        [Fact(DisplayName = "Cria um Curso Válido")]
        public async Task CursoService_CriarCurso_DeveCriar()
        {
			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var MockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, MockProfessorRepository, _notifier);
            var professor = _fixture.Create<Professor>();
            var cursoRequest = _fixture.Create<CursoRequest>();

			//Act
            MockProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            mockCursoRepository.AdicionarAsync(Arg.Any<Curso>()).Returns(Task.CompletedTask);
            var resultado = await cursoService.PostCursoAsync(cursoRequest);

			//Assert
			resultado.Should().NotBe(Guid.Empty, "Guid não pode ser nula");
		}

        [Fact(DisplayName = "Deleta um Curso Válido")]
        public async Task CursoService_DeletarCurso_DeveDeletar()
        {
			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
            var curso = _fixture.Create<Curso>();

			//Act
            mockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
            await cursoService.DeleteCursoAsync(curso.Id);

			//Assert
            await mockCursoRepository.Received().EditarAsync(curso);
        }

        [Fact(DisplayName = "Obtém todos os Cursos")]
        public async Task CursoService_ObterTodosCursos_DeveObter()
        {
			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
            var cursos = _fixture.CreateMany<Curso>(5);

			//Act
            mockCursoRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(cursos);
            var result = await cursoService.GetAllCursosAsync(1, 5);

			//Assert
            result.Should().HaveCount(5);
        }

        [Fact(DisplayName = "Obtém um Curso pelo Id")]
        public async Task CursoService_ObterCursoPorId_DeveObterUm()
        {

			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
            var curso = _fixture.Create<Curso>();
			var cursoResponse = _mapper.Map<CursoResponse>(curso);

			//Act
            mockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
            var result = await cursoService.GetCursoByIdAsync(curso.Id);

			//Assert
			result.Should().BeEquivalentTo(cursoResponse);
		}

        [Fact(DisplayName = "Atualiza um Curso")]
        public async Task CursoService_AtualizarCurso_DeveAtualizar()
        {
			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
            var cursoRequestPut = _fixture.Create<CursoRequestPut>();
            var curso = _fixture.Create<Curso>();

			//Act
            mockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
            await cursoService.PutCursoAsync(curso.Id, cursoRequestPut);

			//Assert
            await mockCursoRepository.Received().EditarAsync(Arg.Is<Curso>(c => c.Nome == cursoRequestPut.Nome));
        }

        [Fact(DisplayName = "Atualiza um Curso com Id inválido")]
        public async Task CursoService_AtualizarCursoComIdInvalido_DeveSerInvalido()
        {

			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
            var cursoRequestPut = _fixture.Create<CursoRequestPut>();

			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.PutCursoAsync(Guid.NewGuid(), cursoRequestPut));
        }

        [Fact(DisplayName = "Cria um Curso com ProfessorId inválido")]
        public async Task CursoService_CriarCursoComProfessorIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
            var cursoRequest = _fixture.Create<CursoRequest>();

			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.PostCursoAsync(cursoRequest));
        }

        [Fact(DisplayName = "Obtém um Curso pelo Id inválido")]
        public async Task CursoService_ObterCursoPorIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.GetCursoByIdAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Deleta um Curso com Id inválido")]
        public async Task CursoService_DeletarCursoComIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.DeleteCursoAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Cria um Curso com Campos inválidos")]
        public async Task CursoService_CriarCursoComCamposInvalidos_DeveSerInvalido()
        {
			//Arrange
			var mockCursoRepository = Substitute.For<ICursoRepository>();
            var MockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, MockProfessorRepository, _notifier);
            var professor = _fixture.Create<Professor>();
            var cursoRequest = _fixture.Create<CursoRequest>();
            cursoRequest.Categoria = string.Empty;
			//Act
            MockProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            await cursoService.PostCursoAsync(cursoRequest);
			//Assert
			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Categoria" && e.ErrorMessage == "Categoria precisa ser preenchida")));
        }

        [Fact(DisplayName = "Atualiza um Curso com Campos inválidos")]
        public async Task CursoService_AtualizarCursoComCamposInvalidos_DeveSerInvalido()
        {
			//Arrange
			var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);
            var cursoRequestPut = _fixture.Create<CursoRequestPut>();
            cursoRequestPut.Categoria = string.Empty;
            var curso = _fixture.Create<Curso>();
			//Act
			mockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
            await cursoService.PutCursoAsync(curso.Id, cursoRequestPut);
			//Assert
			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Categoria" && e.ErrorMessage == "Categoria precisa ser preenchida")));
        }

    }
}
