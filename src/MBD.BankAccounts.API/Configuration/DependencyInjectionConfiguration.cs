using System.Reflection;
using MBD.BankAccounts.Application.DomainEvents;
using MBD.BankAccounts.Application.Interfaces;
using MBD.BankAccounts.Application.Services;
using MBD.BankAccounts.Domain.Events;
using MBD.BankAccounts.Domain.Interfaces.Repositories;
using MBD.BankAccounts.Domain.Interfaces.Services;
using MBD.BankAccounts.Domain.Services;
using MBD.BankAccounts.Infrastructure;
using MediatR;
using MeuBolsoDigital.Core.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MBD.BankAccounts.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomaindServices()
                    .AddAppServices()
                    .AddRepositories()
                    .AddConsumers()
                    .AddConfigurations(configuration)
                    .AddMessageBus()
                    .AddDomainEvents()
                    .AddIntegrationEventLogsService()
                    .AddOutBoxTransaction();

            services.AddHttpContextAccessor();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.Load("MBD.BankAccounts.Application"));

            return services;
        }

        public static IServiceCollection AddDomaindServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionManagementService, TransactionManagementService>();

            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountAppService, AccountAppService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddMessageBus(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DescriptionChangedDomainEvent>, DescriptionChangedDomainEventHandler>();
            services.AddScoped<INotificationHandler<AccountCreatedDomainEvent>, AccountCreatedDomainEventHandler>();

            return services;
        }

        public static IServiceCollection AddIntegrationEventLogsService(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddOutBoxTransaction(this IServiceCollection services)
        {
            return services;
        }
    }
}