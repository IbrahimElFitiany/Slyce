using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Interfaces;
using Orders.Application.UseCases.Commands.AddToCart;
using Orders.Application.UseCases.Commands.CheckoutCart;
using Orders.Infrastructure.Persistence;
using Orders.Infrastructure.Repositores;
using Shared.Application.Behaviors;


namespace Orders.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrdersModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddValidatorsFromAssemblyContaining<CheckoutCartCommand>();

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblyContaining<AddToCartCommand>();
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            services.AddDbContext<OrdersDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddScoped<ICartRepository, EFCartRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            return services;
        }
    }
}