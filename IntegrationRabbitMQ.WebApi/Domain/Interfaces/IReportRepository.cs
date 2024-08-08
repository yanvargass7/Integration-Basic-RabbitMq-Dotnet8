using IntegrationRabbitMQ.WebApi.Domain.Models;

namespace IntegrationRabbitMQ.WebApi.Domain.Interfaces
{
    public interface IReportRepository<T> : IRepository<T> where T : class
    {
        Task<Report> GetReportIdAsync(int reportId);
    }
}
