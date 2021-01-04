using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PrateleiraLivros.Application.Interfaces;
using PrateleiraLivros.Application.Servicos;
using System.Reflection;

namespace PrateleiraLivros.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAutorAppServico, AutorAppServico>();
            services.AddTransient<IEditoraAppServico, EditoraAppServico>();
            services.AddTransient<ILivroAppServico, LivroAppServico>();
        }
    }
}
