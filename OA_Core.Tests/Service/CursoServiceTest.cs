using AutoFixture;
using AutoMapper;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
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
        public async Task CriarCurso()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var MockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, MockProfessorRepository, _notifier);

            var professor = _fixture.Create<Professor>();
            var cursoRequest = _fixture.Create<CursoRequest>();

            MockProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            mockCursoRepository.AdicionarAsync(Arg.Any<Curso>()).Returns(Task.CompletedTask);

            var result = await cursoService.PostCursoAsync(cursoRequest);
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
        }

        [Fact(DisplayName = "Deleta um Curso Válido")]
        public async Task DeletarCurso()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            var curso = _fixture.Create<Curso>();
            mockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

            await cursoService.DeleteCursoAsync(curso.Id);

			await mockCursoRepository.Received().EditarAsync(curso);
        }

        [Fact(DisplayName = "Obtém todos os Cursos")]
        public async Task ObterTodosCursos()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            var cursos = _fixture.CreateMany<Curso>(5);
            mockCursoRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(cursos);

            var result = await cursoService.GetAllCursosAsync(1, 5);

            Assert.Equal(5, result.Count());
        }

        [Fact(DisplayName = "Obtém um Curso pelo Id")]
        public async Task ObterCursoPorId()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            var curso = _fixture.Create<Curso>();
            mockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

            var result = await cursoService.GetCursoByIdAsync(curso.Id);

            Assert.Equal(curso.Id, result.Id);
        }

        [Fact(DisplayName = "Atualiza um Curso")]
        public async Task AtualizarCurso()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            var cursoRequestPut = _fixture.Create<CursoRequestPut>();
            var curso = _fixture.Create<Curso>();

            mockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

            await cursoService.PutCursoAsync(curso.Id, cursoRequestPut);

            await mockCursoRepository.Received().EditarAsync(Arg.Is<Curso>(c => c.Nome == cursoRequestPut.Nome));
        }

        [Fact(DisplayName = "Atualiza um Curso com Id inválido")]
        public async Task AtualizarCursoComIdInvalido()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            var cursoRequestPut = _fixture.Create<CursoRequestPut>();

            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.PutCursoAsync(Guid.NewGuid(), cursoRequestPut));
        }

        [Fact(DisplayName = "Cria um Curso com ProfessorId inválido")]
        public async Task CriarCursoComProfessorIdInvalido()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            var cursoRequest = _fixture.Create<CursoRequest>();

            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.PostCursoAsync(cursoRequest));
        }

        [Fact(DisplayName = "Obtém um Curso pelo Id inválido")]
        public async Task ObterCursoPorIdInvalido()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.GetCursoByIdAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Deleta um Curso com Id inválido")]
        public async Task DeletarCursoComIdInvalido()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.DeleteCursoAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Cria um Curso com Campos inválidos")]
        public async Task CriarCursoComCamposInvalidos()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var MockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, MockProfessorRepository, _notifier);

            var professor = _fixture.Create<Professor>();
            var cursoRequest = _fixture.Create<CursoRequest>();
            cursoRequest.Categoria = string.Empty;

            MockProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);

            await cursoService.PostCursoAsync(cursoRequest);

            _notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Categoria" && e.ErrorMessage == "Categoria precisa ser preenchida")));
        }

        [Fact(DisplayName = "Atualiza um Curso com Campos inválidos")]
        public async Task AtualizarCursoComCamposInvalidos()
        {
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var mockProfessorRepository = Substitute.For<IProfessorRepository>();
            var cursoService = new CursoService(_mapper, mockCursoRepository, mockProfessorRepository, _notifier);

            var cursoRequestPut = _fixture.Create<CursoRequestPut>();
            cursoRequestPut.Categoria = string.Empty;
            var curso = _fixture.Create<Curso>();

            mockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

            await cursoService.PutCursoAsync(curso.Id, cursoRequestPut);

            _notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Categoria" && e.ErrorMessage == "Categoria precisa ser preenchida")));
        }

    }
}
