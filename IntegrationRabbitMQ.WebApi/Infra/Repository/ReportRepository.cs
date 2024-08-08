using IntegrationRabbitMQ.WebApi.Domain.Interfaces;
using IntegrationRabbitMQ.WebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationRabbitMQ.WebApi.Infra.Repository
{
    public class ReportRepository : Repository<Report>, IReportRepository<Report>
    {
        public async Task<Report> GetReportIdAsync(int reportId)
        {
            using DataContext context = new();
            return await context.Reports
                                .Where(m => m.Id == reportId)
                                .FirstOrDefaultAsync();
        }
    }
}