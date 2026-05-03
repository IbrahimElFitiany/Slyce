using Customers.Application.Interfaces;
using Customers.Application.Services;
using Customers.Application.UseCases.Queries.ListCustomerAddresses;
using Customers.Contracts.Interfaces;
using Customers.Infrastructure.Persistence;
using Customers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Customers.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<CustomersDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, EFCustomerRepository>();

            services.AddScoped<ICustomerServices, CustomerServices>();

            services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(
                typeof(CustomersDbContext).Assembly));


            return services;
        }
    }
}
