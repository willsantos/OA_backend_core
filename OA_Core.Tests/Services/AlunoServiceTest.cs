using AutoFixture;
using AutoMapper;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Service;
using OA_Core.Tests.Configs;
using System.Linq;

namespace OA_Core.Tests.Services
{
    [Trait("Service", "Aluno Service")]
    public class AlunoServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly INotificador _notifier;

        public AlunoServiceTest()
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.Get();
            _notifier = Substitute.For<INotificador>();
        }

        [Fact(DisplayName = "Obter todos os alunos")]
        public async Task ObterTodos()
        {
            var alunos = _fixture.Create<List<Aluno>>();
            var mockRepository = Substitute.For<IAlunoRepository>();

            var service = new AlunoService(mockRepository, _mapper, _notifier);

            var page = 1;
            var rows = 10;

            mockRepository.ListPaginationAsync(page, rows).Returns(alunos);

            var result = await service.GetAllAlunosAsync(page, rows);

            Assert.True(result.Count() > 0);
        }

        [Fact(DisplayName = "Obter alunos por id")]
        public async Task ObterPorId()
        {
            var aluno = _fixture.Create<Aluno>();
            var mockRepository = Substitute.For<IAlunoRepository>();

            mockRepository.FindAsync(aluno.Id).Returns(aluno);

            var service = new AlunoService(mockRepository, _mapper, _notifier);

            var result = await service.GetAlunoByIdAsync(aluno.Id);

            Assert.Equal(result.Id, aluno.Id);
        }

        [Fact(DisplayName = "Obter alunos nulos")]
        public async Task ObterPorIdNull()
        {
            var aluno = _fixture.Create<Aluno>();
            var mockRepository = Substitute.For<IAlunoRepository>();

            mockRepository.FindAsync(aluno.Id).Returns((Aluno)null);

            var service = new AlunoService(mockRepository, _mapper, _notifier);

            var exception = await Record.ExceptionAsync(async () => await service.GetAlunoByIdAsync(aluno.Id));
            Assert.NotNull(exception);
        }

        [Fact(DisplayName = "Cadastra alunos")]
        public async Task CadastrarAluno()
        {
            var aluno = _fixture.Create<Aluno>();
            var alunoRequest = new AlunoRequest { UsuarioId = Guid.NewGuid() };
            var mockRepository = Substitute.For<IAlunoRepository>();

            await mockRepository.AddAsync(aluno);

            var service = new AlunoService(mockRepository, _mapper, _notifier);

            var exception = await Record.ExceptionAsync(async () => await service.PostAlunoAsync(alunoRequest));
            Assert.Null(exception);
        }

        [Fact(DisplayName = "Deleta alunos")]
        public async Task DeletarAluno()
        {
            var aluno = _fixture.Create<Aluno>();
            
            var mockRepository = Substitute.For<IAlunoRepository>();

            mockRepository.FindAsync(aluno.Id).Returns(aluno);
            await mockRepository.RemoveAsync(aluno);

            var service = new AlunoService(mockRepository, _mapper, _notifier);

            await service.DeleteAlunoAsync(aluno.Id);

            var exception = await Record.ExceptionAsync(async () => await service.DeleteAlunoAsync(aluno.Id));
            Assert.Null(exception);
        }
    }
}
