using Menus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Menus.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMenusModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<MenusDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<>());
            return services;
        }
    }
}
