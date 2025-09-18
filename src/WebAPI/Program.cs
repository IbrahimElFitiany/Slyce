using Asp.Versioning;
using Customers.Application.Interfaces;
using Customers.Application.Services;
using Customers.Domain.Entites;
using Customers.Domain.Interfaces;
using Customers.Infrastructure.Persistence;
using Customers.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "http://localhost:5173") 
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:8080/realms/slyce-realm";
                options.Audience = "slyce-api";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = "realm_access.roles"
                };

                options.Events = new JwtBearerEvents
                {

                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<CustomersDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICustomerRepository, EFCustomerRepository>();

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

            app.UseCors("AllowFrontend");
            //app.UseSerilogRequestLogging();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseExceptionHandler();
            //app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
