using Serilog;
using System.Text.Json.Serialization;
using Customers.Infrastructure;
using Orders.Application.Interfaces;
using Orders.Infrastructure;
using Restaurants.Infrastructure;
using WebAPI.Extensions;
using WebAPI.Hubs;
using WebAPI.Notifications;


namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddExceptionHandling()
                .AddCORS()
                .AddAuth()
                .AddAuthorization()
                .AddAPIVersioning()
                .AddSignalR();

            builder.Services.AddCustomerModule(builder.Configuration);
            builder.Services.AddOrdersModule(builder.Configuration);
            builder.Services.AddRestaurantModule(builder.Configuration);

            builder.Services.AddScoped<IOrderNotifier, SignalROrderNotifier>();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            var app = builder.Build();

            app.UseCors("AllowFrontend");
            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandler();
            app.UseHttpsRedirection();

            app.MapControllers();
            app.MapHub<OrderHub>("/hubs/orders");

            app.Run();
        }
    }
}