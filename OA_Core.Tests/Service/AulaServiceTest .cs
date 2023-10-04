using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Domain.Notifications;
using OA_Core.Service;
using OA_Core.Tests.Config;
using System.Linq.Expressions;

namespace OA_Core.Tests.Service
{
	[Trait("Service", "Aula Service")]
    public class AulaServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly INotificador _notifier;
		private readonly IAulaRepository _aulaRepository;
		private readonly ICursoRepository _cursoRepository;
		private readonly AulaService _aulaService;


		public AulaServiceTest()
        {
            _fixture = FixtureConfig.GetFixture();
            _mapper = MapperConfig.Get();
            _notifier = Substitute.For<INotificador>();
			_aulaRepository = Substitute.For<IAulaRepository>();
			_cursoRepository = Substitute.For<ICursoRepository>();
			_aulaService = new AulaService(_mapper, _aulaRepository, _cursoRepository, _notifier);
		}

		[Fact(DisplayName = "Cria uma Aula válida")]
        public async Task AulaService_CriaAula_DeveCriar()
        {
			//Arrange            
            var professor = _fixture.Create<Curso>();
            var aulaRequest = _fixture.Create<AulaRequest>();

			//Act
            _cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            _aulaRepository.AdicionarAsync(Arg.Any<Aula>()).Returns(Task.CompletedTask);
            var result = await _aulaService.CadastrarAulaAsync(aulaRequest);

			//Assert
            result.Should().NotBe(Guid.Empty);       
        }
                
        [Fact(DisplayName = "Deleta uma Aula Válida")]
        public async Task AulaService_DeletaAula_DeveDeletar()
        {
			// Arrange
			var id = Guid.NewGuid();
			var aula = _fixture.Create<AulaOnline>();
			aula.Id = id;
			_aulaRepository.ObterPorIdAsync(id).Returns(aula);

			// Act
			await _aulaService.DeletarAulaAsync(id);

			// Assert
			aula.DataDelecao.Should().NotBeNull();
			await _aulaRepository.Received().EditarAsync(aula);
		}

		[Fact(DisplayName = "Obtém todas as aulas")]
		public async Task AulaService_ObtemTodasAulas_DeveObterTodas()
		{
			// Arrange
			var page = 0;
			var rows = 10;
			var listEntity = _fixture.Create<List<AulaOnline>>();
			_aulaRepository.ObterTodosAsync(page, rows).Returns(listEntity);			

			// Act
			var result = await _aulaService.ObterTodasAulasAsync(page, rows);

			// Assert
			result.Should().HaveCount(listEntity.Count());
		}

		[Fact(DisplayName = "Obtém aula por ID")]
		public async Task AulaService_ObtemAulaPorId_DeveObterPorId()
		{
			// Arrange
			var id = Guid.NewGuid();
			var aula = _fixture.Create<AulaOnline>();
			_aulaRepository.ObterPorIdAsync(id).Returns(aula);

			var aulaResponse = _mapper.Map<AulaResponse>(aula);

			// Act
			var result = await _aulaService.ObterAulaPorIdAsync(id);

			// Assert
			result.Should().BeAssignableTo<AulaResponse>();
			result.Should().BeEquivalentTo(aulaResponse);
		}

		[Fact(DisplayName = "Obtém aulas por Curso ID")]
		public async Task AulaService_ObtemAulasPorCursoId_DeveObterPorCursoId()
		{
			// Arrange
			var cursoId = Guid.NewGuid();
			var listEntity = _fixture.Create<List<AulaOnline>>();
			_aulaRepository.ObterTodosAsync(Arg.Any<Expression<Func<Aula, bool>>>()).Returns(listEntity);

			// Act
			var result = await _aulaService.ObterAulasPorCursoIdAsync(cursoId);

			// Assert
			result.Should().HaveCount(listEntity.Count());
		}

		[Fact(DisplayName = "Edita uma aula")]
		public async Task AulaService_EditaAula_DeveEditar()
		{
			// Arrange
			var aula = _fixture.Build<AulaDownload>()
				.With(a => a.Tipo, TipoAula.AulaDownload)
				.Create();

			var request = _mapper.Map<AulaRequestPut>(aula);

			_aulaRepository.ObterAsync(Arg.Any<Expression<Func<Aula, bool>>>()).Returns(aula);
			_aulaRepository.EditarAsync(Arg.Any<Aula>()).Returns(Task.CompletedTask);

			// Act
			await _aulaService.EditarAulaAsync(aula.Id, request);

			// Assert
			await _aulaRepository.Received().EditarAsync(Arg.Any<AulaDownload>());
		}

		[Fact(DisplayName = "Edita uma aula com titulo invalido")]
		public async Task AulaService_EditaAulaInvalida_DeveRetornarErro()
		{
			// Arrange
			var aula = _fixture.Build<AulaTexto>()
				.With(a => a.Tipo, TipoAula.AulaTexto)
				.With(a => a.Titulo, "")
				.Create();

			var request = _mapper.Map<AulaRequestPut>(aula);

			_aulaRepository.ObterAsync(Arg.Any<Expression<Func<Aula, bool>>>()).Returns(aula);
			_aulaRepository.EditarAsync(Arg.Any<Aula>()).Returns(Task.CompletedTask);

			// Act
			_aulaService.EditarAulaAsync(aula.Id, request);

			// Assert
			_notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Titulo" && e.ErrorMessage == "Titulo precisa ser preenchido")));
		}

		[Fact(DisplayName = "Edita uma aula - Aula não encontrada")]
		public async Task AulaService_EditaAula_AulaNaoEncontrada_DeveLancarExcecao()
		{
			// Arrange
			var id = Guid.NewGuid();
			_aulaRepository.ObterAsync(Arg.Any<Expression<Func<Aula, bool>>>()).Returns((Aula)null);

			// Act e Assert
			await Assert.ThrowsAsync<InformacaoException>(() => _aulaService.EditarAulaAsync(id, new AulaRequestPut()));
		}

		[Fact(DisplayName = "Edita uma aula - Tipo inválido")]
		public async Task AulaService_EditaAula_TipoInvalido_DeveLancarExcecao()
		{
			// Arrange
			var id = Guid.NewGuid();
			var request = new AulaRequestPut();
			var aula = _fixture.Build<AulaDownload>()
				.With(a => a.Tipo, TipoAula.AulaDownload)
				.With(a => a.Valid, true)
				.Create();
			_aulaRepository.ObterAsync(Arg.Any<Expression<Func<Aula, bool>>>()).Returns(aula);

			// Act e Assert
			await Assert.ThrowsAsync<InformacaoException>(() => _aulaService.EditarAulaAsync(id, request));
		}

		[Fact(DisplayName = "Edita a ordem de uma aula")]
		public async Task AulaService_EditaOrdemAula_DeveEditarOrdem()
		{
			// Arrange
			var id = Guid.NewGuid();
			var ordemRequest = new OrdemRequest { Ordem = 1 };
			var aula = _fixture.Build<AulaDownload>()
				.With(a => a.Tipo, TipoAula.AulaDownload)
				.With(a => a.Valid, true)
				.Create();
			_aulaRepository.ObterPorIdAsync(id).Returns(aula);

			// Act
			await _aulaService.EditarOrdemAulaAsync(id, ordemRequest);

			// Assert
			aula.Ordem.Should().Be(ordemRequest.Ordem);
			await _aulaRepository.Received().EditarAsync(aula);
		}

		[Fact(DisplayName = "Edita as ordens de aulas de um curso")]
		public async Task AulaService_EditaOrdensAulas_DeveEditarOrdens()
		{
			// Arrange
			var cursoId = Guid.NewGuid();
			var ordens = new[] { new OrdensRequest { Id = Guid.NewGuid(), Ordem = 1 }};

			var aula = _fixture.Build<AulaOnline>()
				.With(a => a.CursoId, cursoId)
				.With(a => a.Id, ordens[0].Id)
				.With(a => a.Tipo, TipoAula.AulaOnline).Create();

			var aulas = new List<AulaOnline>();
			aulas.Add(aula);
			
			_aulaRepository.ObterTodosAsync(Arg.Any<Expression<Func<Aula, bool>>>()).Returns(aulas);

			// Act
			await _aulaService.EditarOrdensAulasAsync(cursoId, ordens);

			// Assert
			aulas[0].Ordem.Should().Be(ordens[0].Ordem);
			await _aulaRepository.Received().EditarVariosAsync(aulas);
		}

		[Fact(DisplayName = "Cria uma Aula com CursoId inválido")]
        public async Task AulaService_CriarAulaComCursoIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var cursoRequest = _fixture.Create<AulaRequest>();

			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => _aulaService.CadastrarAulaAsync(cursoRequest));
        }

        [Fact(DisplayName = "Atualiza uma Aula com Id inválido")]
        public async Task AulaService_AtualizarAulaComIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var aulaRequestPut = _fixture.Create<AulaRequestPut>();

			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => _aulaService.EditarAulaAsync(Guid.NewGuid(), aulaRequestPut));
        }
        

        [Fact(DisplayName = "Obtém uma Aula pelo Id inválido")]
        public async Task AulaService_ObterAulaPorIdInvalido_DeveSerInvalido()
        {
			//Arrange
			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => _aulaService.ObterAulaPorIdAsync(Guid.NewGuid()));
        }

		[Fact(DisplayName = "Deleta uma aula - Aula não encontrada")]
		public async Task AulaService_DeletaAula_AulaNaoEncontrada_DeveLancarExcecao()
		{
			// Arrange
			var id = Guid.NewGuid();
			_aulaRepository.ObterPorIdAsync(id).Returns((Aula)null);

			// Act e Assert
			await Assert.ThrowsAsync<InformacaoException>(() => _aulaService.DeletarAulaAsync(id));
		}

		[Fact(DisplayName = "Cria uma Aula com Campos inválidos")]
        public async Task AulaService_CriarAulaComCamposInvalidos_DeveSerInvalido()
        {
			//Arrange                   
            var professor = _fixture.Create<Curso>();
            var aulaRequest = _fixture.Create<AulaRequest>();
            aulaRequest.Titulo = string.Empty;

			//Act
            _cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            await _aulaService.CadastrarAulaAsync(aulaRequest);

			//Assert
            _notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Titulo" && e.ErrorMessage == "Titulo precisa ser preenchido")));
        }
    }
}
