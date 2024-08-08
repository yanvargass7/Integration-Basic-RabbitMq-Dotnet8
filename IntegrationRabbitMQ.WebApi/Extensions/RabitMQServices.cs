using IntegrationRabbitMQ.WebApi.Consumer;
using MassTransit;

namespace IntegrationRabbitMQ.WebApi.Extensions
{
    internal static class RabitMQServices
    {
        public static void AddRabbitMQService(this IServiceCollection services)
        {
            {
                services.AddMassTransit(busConfigurator =>
                {
                    busConfigurator.AddConsumer<SolicitationReportEventConsumer>();

                    busConfigurator.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(new Uri("amqp://localhost:"), host =>
                        {
                            host.Username("");
                            host.Password("");
                        });
                        cfg.ConfigureEndpoints(ctx);
                    });
                });
            }

        }
    }
}