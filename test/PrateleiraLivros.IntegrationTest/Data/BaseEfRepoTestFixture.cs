using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrateleiraLivros.Data;
using PrateleiraLivros.Data.Repositorios;
using PrateleiraLivros.Dominio.Entidades;
using PrateleiraLivros.Dominio.Interfaces;
using System;
using System.Threading.Tasks;

namespace PrateleiraLivros.IntegrationTest.Data
{
    public class BaseEfRepoTestFixture<T> where T : Entity, IAggregateRoot
    {
        protected PrateleiraLivrosContext DbContext;

        protected static DbContextOptions<PrateleiraLivrosContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<PrateleiraLivrosContext>();
            builder.UseInMemoryDatabase($"dbtest-{Guid.NewGuid()}")
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected async Task<EfRepository<T>> GetRepository(bool seed = false)
        {
            var options = CreateNewContextOptions();
            DbContext = new PrateleiraLivrosContext(options);

            if (seed)
            {
                await PrateleiraLivrosContextSeed
                    .SeedAsync(DbContext);
            }

            return new EfRepository<T>(DbContext);
        }
    }
}
