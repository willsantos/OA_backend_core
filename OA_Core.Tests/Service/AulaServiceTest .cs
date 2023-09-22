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

        [Fact(DisplayName = "Cria uma Aula Válido")]
        public async Task CriarAula()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var MockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, MockCursoRepository, _notifier);

            var professor = _fixture.Create<Curso>();
            var aulaRequest = _fixture.Create<AulaRequest>();

            MockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);
            mockAulaRepository.AdicionarAsync(Arg.Any<Aula>()).Returns(Task.CompletedTask);

            var result = await aulaService.PostAulaAsync(aulaRequest);

            result.Should().NotBe(Guid.Empty);       
        }
                
        [Fact(DisplayName = "Deleta uma Aula Válido")]
        public async Task DeletarAula()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            var aula = _fixture.Create<Aula>();
            mockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);

            await aulaService.DeleteAulaAsync(aula.Id);

            await mockAulaRepository.Received().RemoverAsync(aula);
        }

        [Fact(DisplayName = "Obtém todas as Aulas")]
        public async Task ObterTodosAulas()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            var aulas = _fixture.CreateMany<Aula>(5);
            mockAulaRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(aulas);

            var result = await aulaService.GetAllAulasAsync(1, 5);

            result.Should().HaveCount(5);
        }

        [Fact(DisplayName = "Obtém uma Aula pelo Id")]
        public async Task ObterAulaPorId()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            var aula = _fixture.Create<Aula>();
            var aulaResponse = _mapper.Map<AulaResponse>(aula);

            mockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);

            var result = await aulaService.GetAulaByIdAsync(aula.Id);

            result.Should().BeEquivalentTo(aulaResponse);
        }

        [Fact(DisplayName = "Atualiza uma Aula")]
        public async Task AtualizarAula()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            var aulaRequestPut = _fixture.Create<AulaRequestPut>();
            var aula = _fixture.Create<Aula>();

            mockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);

            await aulaService.PutAulaAsync(aula.Id, aulaRequestPut);

            await mockAulaRepository.Received().EditarAsync(Arg.Is<Aula>(c => c.Nome == aulaRequestPut.Nome));
        }

        [Fact(DisplayName = "Cria uma Aula com CursoId inválido")]
        public async Task CriarAulaComCursoIdInvalido()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var cursoService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            var cursoRequest = _fixture.Create<AulaRequest>();

            await Assert.ThrowsAsync<InformacaoException>(() => cursoService.PostAulaAsync(cursoRequest));
        }

        [Fact(DisplayName = "Atualiza uma Aula com Id inválido")]
        public async Task AtualizarAulaComIdInvalido()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            var aulaRequestPut = _fixture.Create<AulaRequestPut>();

            await Assert.ThrowsAsync<InformacaoException>(() => aulaService.PutAulaAsync(Guid.NewGuid(), aulaRequestPut));
        }
        

        [Fact(DisplayName = "Obtém uma Aula pelo Id inválido")]
        public async Task ObterAulaPorIdInvalido()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            await Assert.ThrowsAsync<InformacaoException>(() => aulaService.GetAulaByIdAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Deleta uma Aula com Id inválido")]
        public async Task DeletarAulaComIdInvalido()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            await Assert.ThrowsAsync<InformacaoException>(() => aulaService.DeleteAulaAsync(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Cria uma Aula com Campos inválidos")]
        public async Task CriarAulaComCamposInvalidos()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var MockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, MockCursoRepository, _notifier);

            var professor = _fixture.Create<Curso>();
            var aulaRequest = _fixture.Create<AulaRequest>();
            aulaRequest.Caminho = string.Empty;

            MockCursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(professor);

            await aulaService.PostAulaAsync(aulaRequest);

            _notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Caminho" && e.ErrorMessage == "Caminho precisa ser preenchido")));
        }

        [Fact(DisplayName = "Atualiza uma Aula com Campos inválidos")]
        public async Task AtualizarAulaComCamposInvalidos()
        {
            var mockAulaRepository = Substitute.For<IAulaRepository>();
            var mockCursoRepository = Substitute.For<ICursoRepository>();
            var aulaService = new AulaService(_mapper, mockAulaRepository, mockCursoRepository, _notifier);

            var aulaRequestPut = _fixture.Create<AulaRequestPut>();
            aulaRequestPut.Caminho = string.Empty;
            var aula = _fixture.Create<Aula>();

            mockAulaRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(aula);

            await aulaService.PutAulaAsync(aula.Id, aulaRequestPut);

            _notifier.Received().Handle(Arg.Is<FluentValidation.Results.ValidationResult>(v => v.Errors.Any(e => e.PropertyName == "Caminho" && e.ErrorMessage == "Caminho precisa ser preenchido")));
        }

    }
}
