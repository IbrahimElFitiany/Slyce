using Serilog;
using System.Text.Json.Serialization;
using Customers.Infrastructure;
using Orders.Application.Interfaces;
using Orders.Infrastructure;
using Restaurants.Infrastructure;
using WebAPI.Extensions;
using WebAPI.Hubs;
using WebAPI.Notifications;
using Menus.Infrastructure;
using Food.Infrastructure;
using Menus.Presentation.Controllers;
using Restaurants.Presentation.Controllers;
using Customers.Presentation.Controllers;
using Subscriptions.Presentation.Controllers;
using Subscriptions.Infrastructure;
using Food.Presentation.Controllers;
using Orders.Presentation.Controllers;
using Identity.Infrastructure;
using Identity.Presentation.Controllers;
using Hangfire;
using Subscriptions.Application.Jobs;


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
                .AddAuth(builder.Configuration)
                .AddAuthorization()
                .AddAPIVersioning()
                .AddSignalR();

            builder.Services
                .AddCustomerModule(builder.Configuration)
                .AddOrdersModule(builder.Configuration)
                .AddRestaurantModule(builder.Configuration)
                .AddMenusModule(builder.Configuration)
                .AddFoodModule(builder.Configuration)
                .AddSubcriptionsModule(builder.Configuration)
                .AddIdentityModule(builder.Configuration);


            //will refactor later
            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(CategoriesController).Assembly);

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(RestaurantApplicationsController).Assembly);

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(SubscriptionsController).Assembly);

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(AddressesController).Assembly);

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(FoodController).Assembly);

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(OrdersController).Assembly);

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(UsersController).Assembly);

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

            app.UseHangfireDashboard("/hangfire");
            app.UseSubscriptionsBackgroundJobs();


            if (app.Environment.IsStaging() || app.Environment.IsDevelopment())
            {
                app.MapPost("api/debug/jobs/subscription-orders", async (SubscriptionOrderGenerationJob job) =>
                {
                    await job.ExecuteAsync();
                    return Results.Ok();
                });
            }

            app.Run();
        }
    }
}