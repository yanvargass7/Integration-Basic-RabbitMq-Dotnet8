using IntegrationRabbitMQ.WebApi.Application;
using IntegrationRabbitMQ.WebApi.Domain.Interfaces;
using System.Collections.Generic;

namespace IntegrationRabbitMQ.WebApi.Extensions
{
    public static class CustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IReport, ReportApp>();
            services.AddScoped<IRabbitMQ, RabbitMQApp>();
            return services;
        }
    }
}