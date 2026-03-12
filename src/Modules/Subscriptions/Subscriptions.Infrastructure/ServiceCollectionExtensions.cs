using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behaviors;
using Subscriptions.Application.Interfaces;
using Subscriptions.Application.UseCases.Commands.CreateSubscription;
using Subscriptions.Infrastructure.Persistence;
using Subscriptions.Infrastructure.Repositories;

namespace Subscriptions.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSubcriptionsModule(this IServiceCollection services ,IConfiguration configuration) {

            services.AddDbContext<SubscriptionsDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISubscriptionRepository, EFSubscriptionRepository>();

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblyContaining<CreateSubscriptionCommand>();
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(typeof(CreateSubscriptionCommandValidator).Assembly);
            return services;
        }
    }
}