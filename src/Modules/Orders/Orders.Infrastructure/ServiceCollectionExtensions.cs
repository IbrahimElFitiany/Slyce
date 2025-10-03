using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Interfaces;
using Orders.Infrastructure.Persistence;
using Orders.Infrastructure.Repositores;


namespace Orders.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrdersModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddScoped<CreateOrder>();
            services.AddScoped<UpdateOrderStatus>();

            services.AddDbContext<OrdersDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IOrderRepository, EFOrderRepository>();

            return services;
        }
    }
}