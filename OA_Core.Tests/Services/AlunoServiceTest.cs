using AutoFixture;
using AutoMapper;
using NSubstitute;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Service;
using OA_Core.Tests.Configs;

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

            Assert.True(result.Any());
        }
    }
}
