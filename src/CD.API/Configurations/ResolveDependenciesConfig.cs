using CD.API.Extensions;
using CD.Domain.Interfaces;
using CD.Domain.Notificacoes;
using CD.Domain.Services;
using CD.Infra.Data;
using CD.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CD.API.Configurations
{
    public static class ResolveDependenciesConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IEstabelecimentoRepository, EstabelecimentoRepository>();
            services.AddScoped<IEstabelecimentoService, EstabelecimentoService>();

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
