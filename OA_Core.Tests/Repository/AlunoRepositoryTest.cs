using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Repository.Context;
using OA_Core.Repository.Repositories;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Repository
{
    public class AlunoRepositoryTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly CoreDbContext _context;
        private readonly AlunoRepository _repository;
        private readonly IDbContextTransaction _transaction;

        public AlunoRepositoryTest()
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

            _repository = new AlunoRepository(_context);
        }

        //public void Dispose()
        //{
        //    // Limpa as entidades de teste do banco de dados após toda a rodada de testes
        //    var entidadesDeTeste = _context.Aluno.Where(c => c.Id.ToString() == "79f57403-55cb-40c4-b4fe-7f4f370e4e4a").ToList();
        //    _context.Aluno.RemoveRange(entidadesDeTeste);
        //    _context.SaveChanges();
        //}

        [Fact(DisplayName = "Adiciona um aluno", Skip = "Git não possue acesso ao banco")]
        public async Task TesteCriarAluno()
        {

            using var transactionToAdd = await _context.Database.BeginTransactionAsync();

            // Para funcionamento do teste, é necessário que no banco de dados, haja um usuário com guid válido.
            try
            {
                var alunoRequest = new AlunoRequest
                {
                    UsuarioId = new Guid("70b54cf0-c703-4d1a-8463-9c906961cf02"),
                };

                var entity = _mapper.Map<Aluno>(alunoRequest);

                await _repository.AddAsync(entity);

                await transactionToAdd.CommitAsync();

                var alunoAdd = await _context.Aluno.FindAsync(entity.Id);
                Assert.NotNull(alunoAdd);

                _context.Aluno.Remove(alunoAdd);
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
