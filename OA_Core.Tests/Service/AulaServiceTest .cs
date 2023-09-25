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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Service
{
    [Trait("Service", "Aula Service")]
    public class AulaServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly INotificador _notifier;

        public AulaServiceTest()
        {
            _fixture = FixtureConfig.GetFixture();
            _mapper = MapperConfig.Get();
            _notifier = Substitute.For<INotificador>();
        }

        [Fact(DisplayName = "Cria uma Aula válida")]
        public async Task AulaService_CriaAula_DeveCriar()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var MockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, MockCursoRepository, _notifier);
            var professor = _fixture.Create<Curso>();
            var aulaRequest = _fixture.Create<AulaRequest>();

			//Act
            MockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            mockAulaRepository.AdicionarAsync(Arg.Any<Aula>()).Returns(Task.CompletedTask);
            var result = await aulaService.PostAulaAsync(aulaRequest);

			//Assert
            result.Should().NotBe(Guid.Empty);       
        }
                
        [Fact(DisplayName = "Deleta uma Aula Válida")]
        public async Task AulaService_DeletaAula_DeveDeletar()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);
            var aula = _fixture.Create<Aula>();

			//Act
            mockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);
            await aulaService.DeleteAulaAsync(aula.Id);

			//Assert
            await mockAulaRepository.Received().EditarAsync(aula);
        }

        [Fact(DisplayName = "Obtém todas as Aulas")]
        public async Task AulaService_ObterTodosAulas_DeveObter()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);
            var aulas = _fixture.CreateMany<Aula>(5);

			//Act
            mockAulaRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(aulas);
            var result = await aulaService.GetAllAulasAsync(1, 5);

			//Assert
            result.Should().HaveCount(5);
        }

        [Fact(DisplayName = "Obtém uma Aula pelo Id")]
        public async Task AulaService_ObterAulaPorId_DeveObterUm()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);
            var aula = _fixture.Create<Aula>();
            var aulaResponse = _mapper.Map<AulaResponse>(aula);

			//Act
            mockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);
            var result = await aulaService.GetAulaByIdAsync(aula.Id);

			//Assert
            result.Should().BeEquivalentTo(aulaResponse);
        }

        [Fact(DisplayName = "Atualiza uma Aula")]
        public async Task AulaService_AtualizarAula_DeveAtualizar()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);
            var aulaRequestPut = _fixture.Create<AulaRequestPut>();
            var aula = _fixture.Create<Aula>();

			//Act
            mockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);
            await aulaService.PutAulaAsync(aula.Id, aulaRequestPut);

			//Assert
            await mockAulaRepository.Received().EditarAsync(Arg.Is<Aula>(c => c.Nome == aulaRequestPut.Nome));
        }

        [Fact(DisplayName = "Cria uma Aula com CursoId inválido")]
        public async Task AulaService_CriarAulaComCursoIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var cursoService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);
            var cursoRequest = _fixture.Create<AulaRequest>();

			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.PostAulaAsync(cursoRequest));
        }

        [Fact(DisplayName = "Atualiza uma Aula com Id inválido")]
        public async Task AulaService_AtualizarAulaComIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);
            var aulaRequestPut = _fixture.Create<AulaRequestPut>();

			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => aulaService.PutAulaAsync(Guid.NewGuid(), aulaRequestPut));
        }
        

        [Fact(DisplayName = "Obtém uma Aula pelo Id inválido")]
        public async Task AulaService_ObterAulaPorIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => aulaService.GetAulaByIdAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Deleta uma Aula com Id inválido")]
        public async Task AulaService_DeletarAulaComIdInvalido_DeveSerInvalido()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

			//Act
			//Assert
            await Assert.ThrowsAsync<InformacaoException>(() => aulaService.DeleteAulaAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Cria uma Aula com Campos inválidos")]
        public async Task AulaService_CriarAulaComCamposInvalidos_DeveSerInvalido()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var MockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, MockCursoRepository, _notifier);
            var professor = _fixture.Create<Curso>();
            var aulaRequest = _fixture.Create<AulaRequest>();
            aulaRequest.Caminho = string.Empty;

			//Act
            MockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            await aulaService.PostAulaAsync(aulaRequest);

			//Assert
            _notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Caminho" && e.ErrorMessage == "Caminho precisa ser preenchido")));
        }

        [Fact(DisplayName = "Atualiza uma Aula com Campos inválidos")]
        public async Task AulaService_AtualizarAulaComCamposInvalidos_DeveSerInvalido()
        {
			//Arrange
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);
            var aulaRequestPut = _fixture.Create<AulaRequestPut>();
            aulaRequestPut.Caminho = string.Empty;
            var aula = _fixture.Create<Aula>();

			//Act
            mockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);
            await aulaService.PutAulaAsync(aula.Id, aulaRequestPut);

			//Assert
            _notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Caminho" && e.ErrorMessage == "Caminho precisa ser preenchido")));
        }

    }
}
