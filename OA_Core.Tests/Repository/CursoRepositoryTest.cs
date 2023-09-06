using AutoFixture;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using OA_Core.Domain.Entities;
using OA_Core.Repository.Context;
using OA_Core.Repository.Repositories;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Repository
{
    public class CursoRepositoryTest
    {
        private readonly Fixture _fixture;
        private readonly CoreDbContext _context;

        public CursoRepositoryTest()
        {
            _fixture = FixtureConfig.GetFixture();
        }

        [Fact]
        public void TesteCriarCurso()
        {
        }
    }
}
