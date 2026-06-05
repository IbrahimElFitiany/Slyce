using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behaviors;
using Subscriptions.Application.Interfaces;
using Subscriptions.Application.Jobs;
using Subscriptions.Application.UseCases.Commands.CreateSubscription;
using Subscriptions.Domain.Interfaces;
using Subscriptions.Domain.Services;
using Subscriptions.Infrastructure.Persistence;
using Subscriptions.Infrastructure.Queries;
using Subscriptions.Infrastructure.Repositories;

namespace Subscriptions.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSubcriptionsModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddScoped<SubscriptionEligibilityService>();
            services.AddScoped<IDistanceCalculator, PostGISDistanceCalculator>();

            services.AddDbContext<SubscriptionsDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISubscriptionRepository, EFSubscriptionRepository>();

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(typeof(CreateSubscriptionCommand).Assembly, typeof(GetCustomerSubscriptionsQueryHandler).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(typeof(CreateSubscriptionCommand).Assembly, includeInternalTypes: true);

            services.AddHangfire(config => {
                config.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));
            });

            services.AddHangfireServer(config => config.WorkerCount = 5);

            services.AddScoped<SubscriptionOrderGenerationJob>();

            return services;
        }

        public static IApplicationBuilder UseSubscriptionsBackgroundJobs(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate<SubscriptionOrderGenerationJob>(
                recurringJobId: "subscription-order-generation",
                methodCall: job => job.ExecuteAsync(),
                cronExpression: Cron.Daily(hour: 0, minute: 0),
                options: new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Utc
                });

            return app;
        }

    }
}