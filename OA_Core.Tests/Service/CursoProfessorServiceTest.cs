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
			var curso = _fixture.Create<Curso>();

			var cursoProfessorRequest = _fixture.Create<CursoProfessorRequest>();

            _professorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            _cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_cursoProfessorRepository.AdicionarAsync(Arg.Any<CursoProfessor>()).Returns(Task.CompletedTask);

			var result = await cursoProfessorService.CadastrarCursoProfessorAsync(cursoProfessorRequest, curso.Id);
			Assert.NotNull(result);
			Assert.IsType<Guid>(result);
		}

		[Fact(DisplayName = "Cria um CursoProfessor com Campos inválidos")]
		public async Task CriarCursoProfessorComCamposInvalidos()
		{
			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var professor = _fixture.Create<Professor>();
			var curso = _fixture.Create<Curso>();
			var cursoProfessorRequest = _fixture.Create<CursoProfessorRequest>();
			cursoProfessorRequest.ProfessorId = Guid.Empty;

			_professorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.CadastrarCursoProfessorAsync(cursoProfessorRequest, Guid.NewGuid()));
		}

		[Fact(DisplayName = "Cria um CursoProfessor com ProfessorId inválido")]
		public async Task CriarCursoProfessorComProfessorIdInvalido()
		{

			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var cursoProfessorRequest = _fixture.Create<CursoProfessorRequest>();

			await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.CadastrarCursoProfessorAsync(cursoProfessorRequest, Guid.NewGuid()));
		}

		[Fact(DisplayName = "Cria um CursoProfessor com ProfessorId inválido")]
		public async Task CriarCursoProfessorComCursoIdInvalido()
		{

			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var professor = _fixture.Create<Professor>();
			var cursoProfessorRequest = _fixture.Create<CursoProfessorRequest>();

			_professorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);


			await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.CadastrarCursoProfessorAsync(cursoProfessorRequest, Guid.NewGuid()));
		}

		[Fact(DisplayName = "Deleta um CursoProfessor Válido")]
		public async Task DeletarCursoProfessor()
		{
			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var professor = _fixture.Create<Professor>();
			var cursoProfessor = _fixture.Create<CursoProfessor>();
			cursoProfessor.CursoId = curso.Id;
			cursoProfessor.ProfessorId = professor.Id;
			cursoProfessor.Responsavel = false;

			_cursoProfessorRepository.ObterAsync(Arg.Any<Expression<Func<CursoProfessor, bool>>>()).Returns(cursoProfessor);

			await cursoProfessorService.DeletarCursoProfessorAsync(curso.Id, professor.Id);

			await _cursoProfessorRepository.Received().EditarAsync(cursoProfessor);
		}

		[Fact(DisplayName = "Deleta um CursoProfessor Responsavel Válido")]
		public async Task DeletarCursoProfessorResponsavel()
		{
			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var professor = _fixture.Create<Professor>();
			var cursoProfessor = _fixture.Create<CursoProfessor>();
			cursoProfessor.CursoId = curso.Id;
			cursoProfessor.ProfessorId = professor.Id;
			cursoProfessor.Responsavel = true;

			_cursoProfessorRepository.ObterAsync(Arg.Any<Expression<Func<CursoProfessor, bool>>>()).Returns(cursoProfessor);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.DeletarCursoProfessorAsync(curso.Id, professor.Id));
		}

		[Fact(DisplayName = "Deleta um CursoProfessor com Id inválido")]
		public async Task DeletarCursoComIdInvalido()
		{
			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.DeletarCursoProfessorAsync(Guid.NewGuid(), Guid.NewGuid()));
		}

		[Fact(DisplayName = "Obtém todos os CursoProfessor")]
        public async Task ObterTodosCursoProfessor()
        {         
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var cursoProfessores = _fixture.CreateMany<CursoProfessor>(5);
            _cursoProfessorRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(cursoProfessores);

			var result = await cursoProfessorService.ObterTodosCursoProfessoresAsync(1, 5);

			Assert.Equal(5, result.Count());
		}

		[Fact(DisplayName = "Obtém todos os CursoProfessor por expressão")]
		public async Task ObterTodosCursoProfessorPorExpressão()
		{
			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var cursosProfessores = _fixture.CreateMany<CursoProfessor>(5);

			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_cursoProfessorRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<CursoProfessor, bool>>>()).Returns(cursosProfessores);

			var result = await cursoProfessorService.ObterProfessoresDeCursoPorIdAsync(Guid.NewGuid());

			Assert.Equal(5, result.Count());
		}

		[Fact(DisplayName = "Obtém todos os CursoProfessor por expressão com curso invalido")]
		public async Task ObterTodosCursoProfessorPorExpressãoCursoInvalido()
		{
			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var cursosProfessores = _fixture.CreateMany<CursoProfessor>(5);

			_cursoProfessorRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<CursoProfessor, bool>>>()).Returns(cursosProfessores);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.ObterProfessoresDeCursoPorIdAsync(Guid.NewGuid()));
		}

		[Fact(DisplayName = "Obtém todos os CursoProfessor por expressão com cursoprofessor invalido")]
		public async Task ObterTodosCursoProfessorPorExpressãoCursoProfessorInvalido()
		{
			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var cursosProfessores = _fixture.CreateMany<CursoProfessor>(0);

			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_cursoProfessorRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<CursoProfessor, bool>>>()).Returns(cursosProfessores);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.ObterProfessoresDeCursoPorIdAsync(Guid.NewGuid()));
		}

		[Fact(DisplayName = "Obtém um CursoProfessor pelo Id")]
        public async Task ObterCursoProfessorPorId()
        {          
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var curso = _fixture.Create<CursoProfessor>();
            _cursoProfessorRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

			var result = await cursoProfessorService.ObterCursoProfessorPorIdAsync(curso.Id);

			Assert.Equal(curso.Id, result.Id);
		}

		[Fact(DisplayName = "Obtém um CursoProfessor pelo Id inválido")]
		public async Task ObterCursoProfessorPorIdInvalido()
		{

			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.ObterCursoProfessorPorIdAsync(Guid.NewGuid()));
		}

		[Fact(DisplayName = "Atualiza um CursoProfessor")]
        public async Task AtualizarCursoProfessor()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var cursoProfessorRequest = _fixture.Create<CursoProfessorRequest>();
            var cursoProfessor = _fixture.Create<CursoProfessor>();
			cursoProfessorRequest.ProfessorId = cursoProfessor.Id;

			_cursoProfessorRepository.ObterAsync(Arg.Any<Expression<Func<CursoProfessor, bool>>>()).Returns(cursoProfessor);

			await cursoProfessorService.EditarCursoProfessorAsync(cursoProfessor.CursoId, cursoProfessorRequest);

			await _cursoProfessorRepository.Received().EditarAsync(cursoProfessor);
		}

        [Fact(DisplayName = "Atualiza um CursoProfessor com Id inválido")]
        public async Task AtualizarCursoComIdInvalido()
        {
            
            var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

            var cursoProfessorRequest = _fixture.Create<CursoProfessorRequest>();

            await Assert.ThrowsAsync<InformacaoException>(() => cursoProfessorService.EditarCursoProfessorAsync(Guid.NewGuid(), cursoProfessorRequest));
        }

		[Fact(DisplayName = "Atualiza um CursoProfessor com Campos inválidos")]
		public async Task AtualizarCursoProfessorComCamposInvalidos()
		{

			var cursoProfessorService = new CursoProfessorService(_mapper, _cursoProfessorRepository, _professorRepository, _cursoRepository, _notifier);

			var cursoProfessorRequest = _fixture.Create<CursoProfessorRequest>();
			cursoProfessorRequest.ProfessorId = Guid.Empty;
			var curso = _fixture.Create<Curso>();

			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);

			await cursoProfessorService.EditarCursoProfessorAsync(Guid.NewGuid(), cursoProfessorRequest);

			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "ProfessorId" && e.ErrorMessage == "ProfessorId não pode ser nulo")));
		}     
    }
}
