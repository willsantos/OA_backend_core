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

        [Fact(DisplayName ="Adiciona um curso")]
        public async Task TesteCriarCurso()
        {

            using var transactionToAdd = await _context.Database.BeginTransactionAsync();

            // Para funcionamento do teste, é necessário que no banco de dados, haja um professor com guid válido.
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

                await transactionToAdd.CommitAsync();

                var cursoAdicionado = await _context.Curso.FindAsync(entity.Id);
                Assert.NotNull(cursoAdicionado);
                Assert.Equal(entity.Nome, cursoAdicionado.Nome);

                _context.Curso.Remove(cursoAdicionado);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                await transactionToAdd.RollbackAsync();
                throw;
            }
        }

        [Fact(DisplayName = "Edita um curso criado")]
        public async Task TesteEditarCurso()
        {

            using var transactionToAdd = await _context.Database.BeginTransactionAsync();

            // Para funcionamento do teste, é necessário que no banco de dados, haja um professor com guid válido.
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

                await transactionToAdd.CommitAsync();

                string alteracao = "nomeAlterado";
                entity.Nome = alteracao;
                await _repository.EditAsync(entity);
                var cursoEditado = await _context.Curso.FindAsync(entity.Id);

                Assert.Equal(cursoEditado.Nome, alteracao);

                _context.Curso.Remove(cursoEditado);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                await transactionToAdd.RollbackAsync();
                throw;
            }
        }

        [Fact(DisplayName = "Busca um curso criado por ID")]
        public async Task TesteBuscarCursoPorId()
        {
            using var transactionToAdd = await _context.Database.BeginTransactionAsync();

            // Para funcionamento do teste, é necessário que no banco de dados, haja um professor com guid válido.
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
                await transactionToAdd.CommitAsync();

                var cursoDB = await _repository.FindAsync(entity.Id);

                Assert.NotNull(cursoDB);
                Assert.Equal(cursoDB.Nome, entity.Nome);

                _context.Curso.Remove(cursoDB);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                await transactionToAdd.RollbackAsync();
                throw;
            }
        }

    }
}
