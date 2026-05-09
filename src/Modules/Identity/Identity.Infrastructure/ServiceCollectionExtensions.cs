using MediatR;
using FluentValidation;
using Identity.Application.Interfaces;
using Identity.Application.Interfaces.EmailService;
using Identity.Application.UseCases.Commands.Login;
using Identity.Application.UseCases.Commands.RegisterCustomer;
using Identity.Infrastructure.EmailService;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.TokenGeneration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behaviors;


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

            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
            services.AddScoped<ITokenGenerator, TokenService>();
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddSingleton<IEmailSender, EmailQueueWriter>();
            services.AddSingleton<EmailChannel>();
            services.AddSingleton<SmtpEmailSender>();
            services.AddHostedService<EmailWorker>();
            services.Configure<SMTPSettings>(configuration.GetSection("EmailSettings"));

            services.AddScoped<IIdentityServices, IdentityServices>();

            services.AddValidatorsFromAssemblyContaining<LoginCommand>(includeInternalTypes: true);

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(typeof(RegisterCustomerCommand).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });


            return services;
        }
    }
}