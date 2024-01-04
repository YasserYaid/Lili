//Clase de inyeccion de dependencias del db context "Dependecy Container" o metodos de extension
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Application.Persistence.EspecificClassicRepository;
using Ecommerce.Infrastructure.EspecificClassicRepositories;
using Ecommerce.Application.Persistence;
using Ecommerce.Infrastructure.Repositories;

namespace Ecommerce.Infrastructure
{
    public static class ClassicInfrastructureServiceRegistration
    {
        public static IServiceCollection AddContextRepositoriesSqlServer
            ( this IServiceCollection services, IConfiguration configuration,string connecionString )
        {
            services.AddSqlServer<EcommerceDbContext>(configuration.GetConnectionString(connecionString));
            return services;
        }

        public static IServiceCollection AddClassicRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClassicUnitOfWork, ClassicUnitOfWork>();
            services.AddScoped<IBranchClassicRepository, BranchClassicRepository>();//Probablemente se puedan quitar
            services.AddScoped<IProductClassicRepository, ProductClassicRepository>();//Probablemente se puedan quitar
            services.AddScoped<IEmployeeClassicRepository, EmployeeClassicRepository>();//Probablemente se puedan quitar

            return services;
        }

    }
}
