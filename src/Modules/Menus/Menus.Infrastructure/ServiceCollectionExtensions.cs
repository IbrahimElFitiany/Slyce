using FluentValidation;
using MediatR;
using Menus.Application.Interfaces;
using Menus.Application.UseCases.Commands.AddMealSize;
using Menus.Application.UseCases.Commands.CreateMenuCategory;
using Menus.Contracts.Interfaces;
using Menus.Infrastructure.Caching;
using Menus.Infrastructure.Persistence;
using Menus.Infrastructure.Queries;
using Menus.Infrastructure.Repositories;
using Menus.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behaviors;

namespace Menus.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMenusModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<MenusDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMenuCategoryRepository, EFMenuCategoryRepository>();
            services.AddScoped<IMenuMealRepository, EFMenuMealRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IMenuQueryServices, MenuQueryServices>();

            services.AddStackExchangeRedisCache(op =>
            {
                op.Configuration = configuration.GetConnectionString("RedisConnection");
            });

            services.AddValidatorsFromAssemblyContaining<AddMealSizeCommandValidator>();

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblyContaining<CreateMenuCategoryCommand>();
                cfg.RegisterServicesFromAssemblyContaining<GetMealByIdQueryHandler>();
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}