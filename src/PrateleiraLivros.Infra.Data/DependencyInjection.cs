using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrateleiraLivros.Data;
using PrateleiraLivros.Data.Repositorios;
using PrateleiraLivros.Dominio.Interfaces;

namespace PrateleiraLivros.Infra.Data
{
    public static class DependencyInjection
    {
        public static void AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PrateleiraLivrosContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opt =>
                {
                    opt.CommandTimeout(180);
                    opt.MigrationsAssembly("PrateleiraLivros.Infra.Data");
                    opt.EnableRetryOnFailure(2);
                });
                opt.EnableDetailedErrors();
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
        }
    }
}
