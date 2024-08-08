using IntegrationRabbitMQ.WebApi.Domain.Interfaces;
using IntegrationRabbitMQ.WebApi.Domain.Models;
using IntegrationRabbitMQ.WebApi.Event;
using MassTransit;

namespace IntegrationRabbitMQ.WebApi.Application
{
    public class ReportApp : IReport
    {
        private readonly IBus _busService;
        private readonly IReportRepository<Report> _reportRepository;
        private readonly List<Report> _reports = new List<Report>();

        public ReportApp(IBus busService, IReportRepository<Report> reportRepository)
        {
            _busService = busService;
            _reportRepository = reportRepository;
        }
        public List<Report> GetListReports()
        {
            return _reports;
        }

        public void AddListReport(Report report)
        {
            _reports.Add(report);
        }

        public void newReport(Report report)
        {
            report.Status = Status.Processing;
            _reportRepository.Insert(report);
        }

        public async Task<string> GenerateReport(int id, string name)
        {
            Report report = new()
            {
                Id = id,
                Name = name,
                Status = Status.Processing,
                ProcessedTime = null
            };
            AddListReport(report);

            var eventRequest = new SolicitationReportEvent(id, name);

            // Manda para o MassTransit o report
            await _busService.Publish(eventRequest);

            return "Processing report " + id + " " + name + " ......";
        }

    }
}