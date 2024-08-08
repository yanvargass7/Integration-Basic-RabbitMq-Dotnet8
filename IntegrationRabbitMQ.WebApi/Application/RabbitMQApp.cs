using IntegrationRabbitMQ.WebApi.Domain.Interfaces;
using MassTransit;
using MassTransit.Futures.Contracts;

namespace IntegrationRabbitMQ.WebApi.Application
{
    public class RabbitMQApp : IRabbitMQ
    {
        private readonly IServiceCollection _service;
        public void AddMassTransit()
        {
            _service.AddMassTransit(busConfigurator =>
            {
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
