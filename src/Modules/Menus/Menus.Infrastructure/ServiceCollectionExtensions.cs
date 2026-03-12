using Menus.Application.Interfaces;
using Menus.Application.UseCases.Commands.CreateMenuCategory;
using Menus.Contracts.Interfaces;
using Menus.Infrastructure.Persistence;
using Menus.Infrastructure.Repositories;
using Menus.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Menus.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMenusModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<MenusDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMenuCategoryRepository, EFMenuCategoryRepository>();
            services.AddScoped<IMenuMealRepository, EFMenuMealRepository>();
            services.AddScoped<IMenuQueryServices, MenuQueryServices>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddNewMealSizeCommand>());
            return services;
        }
    }
}