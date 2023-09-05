using Microsoft.EntityFrameworkCore;
using OA_Core.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Config
{
    public class DatabaseConfig : IDisposable
    {
        public readonly CoreDbContext context;
        public DatabaseConfig() 
        {
            var builder = new DbContextOptionsBuilder<CoreDbContext>();
            builder.UseInMemoryDatabase(databaseName: "CoreDbInMemory");

            var dbContextOptions = builder.Options;
            context = new CoreDbContext(dbContextOptions);
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
