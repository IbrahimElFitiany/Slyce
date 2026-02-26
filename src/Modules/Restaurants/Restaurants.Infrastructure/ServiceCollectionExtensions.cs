using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Interfaces;
using Restaurants.Application.UseCases.Commands.CreateRestaurantApplication;
using Restaurants.Contracts;
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
            services.AddScoped<IRestaurantBranchRepository, EFRestaurantBranchRepository>();
            services.AddScoped<IRestaurantApplicationRepository, EFRestaurantApplicationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IRestaurantServices, RestaurantServices>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateRestaurantApplicationCommand>());

            return services;
        }
    }
}
