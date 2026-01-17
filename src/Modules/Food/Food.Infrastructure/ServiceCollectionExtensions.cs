using Food.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Food.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFoodModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<FoodDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}