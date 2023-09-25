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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Service
{
    [Trait("Service", "CursoProfessor Service")]
    public class CursoProfessorServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly INotificador _notifier;
		private readonly IProfessorRepository _professorRepository;
		private readonly ICursoRepository _cursoRepository;
		private readonly ICursoProfessorRepository _cursoProfessorRepository;

        public CursoProfessorServiceTest()
        {
            _fixture = FixtureConfig.GetFixture();
            _mapper = MapperConfig.Get();
            _notifier = Substitute.For<INotificador>();
			_professorRepository = Substitute.For<IProfessorRepository>();
			_cursoRepository = Substitute.For<ICursoRepository>();
			_cursoProfessorRepository = Substitute.For<ICursoProfessorRepository>();
        }

        [Fact(DisplayName = "Cria um CursoProfessor Válido")]
        public async Task CriarCurso()
        {                       
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var professor = _fixture.Create<Professor>();
            var cursoRequest = _fixture.Create<CursoRequest>();

            _professorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            _cursoRepository.AdicionarAsync(Arg.Any<Curso>()).Returns(Task.CompletedTask);

            //var result = await cursoProfessorService.PostCursoAsync(cursoRequest);
            //Assert.NotNull(result);
            //Assert.IsType<Guid>(result);
        }

        [Fact(DisplayName = "Deleta um CursoProfessor Válido")]
        public async Task DeletarCurso()
        {

			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var professor = _fixture.Create<Professor>();
			var cursoProfessor = _fixture.Create<CursoProfessor>();
			cursoProfessor.CursoId = curso.Id;
			cursoProfessor.ProfessorId = professor.Id;

			_cursoProfessorRepository.ObterAsync(Arg.Any<Expression<Func<CursoProfessor, bool>>>()).Returns(cursoProfessor);

            await cursoProfessorService.DeleteCursoProfessorAsync(curso.Id, professor.Id);

			await _cursoProfessorRepository.Received().EditarAsync(cursoProfessor);
        }

        [Fact(DisplayName = "Obtém todos os Cursos")]
        public async Task ObterTodosCursos()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var cursos = _fixture.CreateMany<Curso>(5);
            _cursoRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(cursos);

            //var result = await cursoProfessorService.GetAllCursosAsync(1, 5);

            //Assert.Equal(5, result.Count());
        }

        [Fact(DisplayName = "Obtém um Curso pelo Id")]
        public async Task ObterCursoPorId()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var curso = _fixture.Create<Curso>();
            _cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

            //var result = await cursoProfessorService.GetCursoByIdAsync(curso.Id);

            //Assert.Equal(curso.Id, result.Id);
        }

        [Fact(DisplayName = "Atualiza um Curso")]
        public async Task AtualizarCurso()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var cursoRequestPut = _fixture.Create<CursoRequestPut>();
            var curso = _fixture.Create<Curso>();

            _cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

            //await cursoProfessorService.PutCursoAsync(curso.Id, cursoRequestPut);

            //await _cursoRepository.Received().EditarAsync(Arg.Is<Curso>(c => c.Nome == cursoRequestPut.Nome));
        }

        [Fact(DisplayName = "Atualiza um Curso com Id inválido")]
        public async Task AtualizarCursoComIdInvalido()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var cursoRequestPut = _fixture.Create<CursoRequestPut>();

            //await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.PutCursoAsync(Guid.NewGuid(), cursoRequestPut));
        }

        [Fact(DisplayName = "Cria um Curso com ProfessorId inválido")]
        public async Task CriarCursoComProfessorIdInvalido()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var cursoRequest = _fixture.Create<CursoRequest>();

            //await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.PostCursoAsync(cursoRequest));
        }

        [Fact(DisplayName = "Obtém um Curso pelo Id inválido")]
        public async Task ObterCursoPorIdInvalido()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            //await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.GetCursoByIdAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Deleta um Curso com Id inválido")]
        public async Task DeletarCursoComIdInvalido()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            //await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.DeleteCursoAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Cria um Curso com Campos inválidos")]
        public async Task CriarCursoComCamposInvalidos()
        {
            
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var professor = _fixture.Create<Professor>();
            var cursoRequest = _fixture.Create<CursoRequest>();
            cursoRequest.Categoria = string.Empty;

            _professorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);

            //await cursoProfessorService.PostCursoAsync(cursoRequest);

            //_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Categoria" && e.ErrorMessage == "Categoria precisa ser preenchida")));
        }

        [Fact(DisplayName = "Atualiza um Curso com Campos inválidos")]
        public async Task AtualizarCursoComCamposInvalidos()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var cursoRequestPut = _fixture.Create<CursoRequestPut>();
            cursoRequestPut.Categoria = string.Empty;
            var curso = _fixture.Create<Curso>();

            _cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

            //await cursoProfessorService.PutCursoAsync(curso.Id, cursoRequestPut);

            //_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Categoria" && e.ErrorMessage == "Categoria precisa ser preenchida")));
        }

    }
}
