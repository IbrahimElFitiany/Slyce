using Food.Application.Interfaces;
using Food.Application.Services;
using Food.Contracts;
using Food.Infrastructure.Persistence;
using Food.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Food.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFoodModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<FoodDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFoodRepository, EFFoodRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SearchFoodsQuery>());
            return services;
        }
    }
}