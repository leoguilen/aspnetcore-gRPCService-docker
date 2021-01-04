using Microsoft.Extensions.DependencyInjection;
using PrateleiraLivros.Dominio.Interfaces;
using PrateleiraLivros.Infra.Logging;
using PrateleiraLivros.Infra.Servicos;

namespace PrateleiraLivros.Infra
{
    public static class DependencyInjection
    {
        public static void AddInfra(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<ICsvServico, CsvServico>();
        }
    }
}
