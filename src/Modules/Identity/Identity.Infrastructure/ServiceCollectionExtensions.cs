using Identity.Application.Interfaces;
using Identity.Application.Services;
using Identity.Application.UseCases.Commands.RegisterCustomer;
using Identity.Contract.Interfaces;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Identity.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<IdentityDbContext>(options => 
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable("__IdentityMigrationsHistory", "Identity")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddMediatR(cfg =>  cfg.RegisterServicesFromAssemblies(typeof(RegisterCustomerCommand).Assembly));


            return services;
        }
    }
}