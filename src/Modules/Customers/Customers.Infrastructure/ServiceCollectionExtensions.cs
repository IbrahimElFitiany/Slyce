using MediatR;
using FluentValidation;
using Shared.Application.Behaviors;
using Customers.Application.Interfaces;
using Customers.Application.Services;
using Customers.Application.UseCases.Commands.UpdateGender;
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
        public static IServiceCollection AddCustomerModule(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<CustomersDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, EFCustomerRepository>();

            services.AddScoped<ICustomerServices, CustomerServices>();

            services.AddValidatorsFromAssemblyContaining<UpdateGenderCommand>(includeInternalTypes: true);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(CustomersDbContext).Assembly, typeof(UpdateGenderCommand).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}