using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Interfaces;
using Restaurants.Application.UseCases;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestaurantModule(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddDbContext<RestaurantDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRestaurantRepository, EFRestaurantRepository>();

            services.AddScoped<RegisterRestaurantUseCase>();
            return services;
        }
    }
}
