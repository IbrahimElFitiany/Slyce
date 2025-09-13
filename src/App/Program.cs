using Asp.Versioning;
using Customers.Application.Interfaces;
using Customers.Application.Services;
using Customers.Domain.Entites;
using Customers.Domain.Interfaces;
using Customers.Infrastructure.Persistence;
using Customers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Slyce.Infrastructure.ExceptionHandling;


namespace SlyceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddProblemDetails();

            // Register DbContext (Infrastructure)
            builder.Services.AddDbContext<CustomersDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repositories (Infrastructure implements Domain interfaces)
            builder.Services.AddScoped<ICustomerRepository, EFCustomerRepository>();

            // Register Application Services
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddControllers();

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1.0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Version")
                );
                options.ReportApiVersions = true;
            }
            ).AddMvc();

            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            var app = builder.Build();

            //app.UseSerilogRequestLogging();
            app.UseExceptionHandler();
            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
