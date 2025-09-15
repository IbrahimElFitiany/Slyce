using Asp.Versioning;
using Customers.Application.Interfaces;
using Customers.Application.Services;
using Customers.Domain.Entites;
using Customers.Domain.Interfaces;
using Customers.Infrastructure.Persistence;
using Customers.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:8080/realms/slyce";
                options.Audience = "account";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    RoleClaimType = "realm_access.roles"
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var claimsIdentity = context.Principal.Identity as System.Security.Claims.ClaimsIdentity;
                        if (claimsIdentity != null)
                        {
                            var realmAccess = context.Principal.FindFirst("realm_access");
                            if (realmAccess != null)
                            {
                                // realm_access is JSON; parse it to add role claims
                                var obj = System.Text.Json.JsonDocument.Parse(realmAccess.Value);
                                if (obj.RootElement.TryGetProperty("roles", out var roles))
                                {
                                    foreach (var role in roles.EnumerateArray())
                                    {
                                        claimsIdentity.AddClaim(new System.Security.Claims.Claim(claimsIdentity.RoleClaimType, role.GetString()));
                                    }
                                }
                            }
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseExceptionHandler();
            //app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
