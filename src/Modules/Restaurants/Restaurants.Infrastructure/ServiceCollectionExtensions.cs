using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Services;
using Restaurants.Application.UseCases.Commands.CreateRestaurantApplication;
using Restaurants.Contracts.Interfaces;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Shared.Application.Behaviors;

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


            services.AddValidatorsFromAssemblyContaining(typeof(CreateRestaurantApplicationCommand), includeInternalTypes: true);

            services.AddScoped<IRestaurantServices, RestaurantServices>();
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblyContaining<CreateRestaurantApplicationCommand>();
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
