using IntegrationRabbitMQ.WebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationRabbitMQ.WebApi.Domain.Interfaces
{
    public interface IReport
    {
        public void newReport(Report report);
        public Task<string> GenerateReport(int id, string name);
        void AddListReport(Report report);
        List<Report> GetListReports();
    }
}
