using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Model;
using System.Reflection;

namespace Project.Core.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IReceiveConfigurator<IRabbitMqReceiveEndpointConfigurator> AddReceiveEndpoint<T>(this IReceiveConfigurator<IRabbitMqReceiveEndpointConfigurator> receiveConfigurator,
            string ExchangeName, IBusRegistrationContext context) where T : class, IConsumer
        {
            receiveConfigurator.ReceiveEndpoint(ExchangeName, x =>
            {
                x.ConfigureConsumer<T>(context);

            });
            return receiveConfigurator;
        }
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services, 
            Action<IReceiveConfigurator<IRabbitMqReceiveEndpointConfigurator>, IBusRegistrationContext> receiveConfigurator)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());
                configure.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    configurator.Host(rabbitMQSettings.Host, hostConfigurator =>
                    {
                        hostConfigurator.Username(rabbitMQSettings.UserName);
                        hostConfigurator.Password(rabbitMQSettings.Password);
                    });
                    configurator.UseMessageRetry(retryConfigurator =>
                    {
                        retryConfigurator.Interval(3, TimeSpan.FromMinutes(5));
                    });
                    receiveConfigurator(configurator,context);
                });
            });

            return services;
        }
    }
}
