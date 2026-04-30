using MediatR;
using FluentValidation;
using Food.Application.Interfaces;
using Food.Application.Services;
using Food.Application.UseCases.Queries;
using Food.Contracts;
using Food.Infrastructure.ExternalServices.FatSecretService;
using Food.Infrastructure.Persistence;
using Food.Infrastructure.Queries;
using Food.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behaviors;

namespace Food.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFoodModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<FoodDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.Configure<FatSecretSettings>(configuration.GetSection("FatSecretSettings"));
            services.AddHttpClient("FatSecret");

            services.AddScoped<IExternalFoodService, FatSecretFoodService>();
            services.AddSingleton<FatSecretTokenService>();

            services.AddScoped<IFoodServices, FoodContractServices>();
            services.AddScoped<IFoodRepository, EFFoodRepository>();

            services.AddValidatorsFromAssemblyContaining<SearchFoodSummaryValidator>();

            services.AddMediatR(cfg => {
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.RegisterServicesFromAssemblyContaining<SearchFoodSummaryQuery>();
                cfg.RegisterServicesFromAssemblyContaining<SearchFoodSummaryQueryHandler>();
            });
            
            return services;
        }
    }
}