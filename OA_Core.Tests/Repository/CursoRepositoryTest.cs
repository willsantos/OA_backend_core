using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using OA_Core.Repository.Repositories;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Repository
{
    [Trait("Repository", "CursoReposistory")]
    public class CursoRepositoryTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly CoreDbContext _context;
        private readonly CursoRepository _repository;
        private readonly IDbContextTransaction _transaction;

        public CursoRepositoryTest()
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.GetFixture();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var connectionString = configuration.GetSection("AppConfig").GetValue<string>("ConnectionString");

            var options = new DbContextOptionsBuilder<CoreDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;

            _context = new CoreDbContext(options);

            _repository = new CursoRepository(_context);
        }
        public void Dispose()
        {
            _transaction.Rollback();
        }

        [Fact(DisplayName ="Adiciona um curso")]
        public async Task TesteCriarCurso()
        {

            using var transactionToAdd = await _context.Database.BeginTransactionAsync();

            try
            {
                var cursoRequest = new CursoRequest
                {
                    Nome = "TestEntity",
                    Descricao = "TestEntity",
                    Categoria = "TestEntity",
                    PreRequisito = "TestEntity",
                    Preco = 100,
                    ProfessorId = new Guid("cff4e2f5-f132-4a66-969c-dcc76c5ba585"),
                };

                var entity = _mapper.Map<Curso>(cursoRequest);

                await _repository.AddAsync(entity);

                // Faz um commit da transação para salvar as alterações no banco de dados
                await transactionToAdd.CommitAsync();

                var cursoAdicionado = await _context.Curso.FindAsync(entity.Id);
                Assert.NotNull(cursoAdicionado);
                Assert.Equal(entity.Nome, cursoAdicionado.Nome);

                _context.Curso.Remove(cursoAdicionado);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Faz um rollback da transação para desfazer todas as alterações feitas no banco de dados durante o teste
                await transactionToAdd.RollbackAsync();
                throw;
            }
        }
    }
}
