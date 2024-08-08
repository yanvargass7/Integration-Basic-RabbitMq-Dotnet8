using IntegrationRabbitMQ.WebApi.Domain.Interfaces;
using IntegrationRabbitMQ.WebApi.Domain.Models;
using IntegrationRabbitMQ.WebApi.Event;
using MassTransit;

namespace IntegrationRabbitMQ.WebApi.Consumer
{
    internal sealed class SolicitationReportEventConsumer : IConsumer<SolicitationReportEvent>
    {
        private readonly ILogger<SolicitationReportEventConsumer> _logger;
        private readonly IReport _reportService;
        private readonly IReportRepository<Report> _reportRepository;
        public SolicitationReportEventConsumer(ILogger<SolicitationReportEventConsumer> logger, IReport report, IReportRepository<Report> reportRepository)
        {
            _logger = logger;
            _reportService = report;
            _reportRepository = reportRepository;
        }

        public async Task Consume(ConsumeContext<SolicitationReportEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation("Report Processing Id:{Id}, Nome:{Nome}  ...", message.id, message.name);

            //delay simulation processing report
            await Task.Delay(5000);

            Report report = new();
            report = await _reportRepository.GetReportIdAsync(message.id);

            if (report.Status == Status.Processing)
            {
                report.Status = Status.Success;
                report.ProcessedTime = DateTime.Now;

                _reportRepository.Update(report);

                _logger.LogInformation("Report **Processed**OK** Id:{Id}, Nome:{Nome}  ...", message.id, message.name);

            }else
                _logger.LogInformation("Report don't find!", message.id, message.name);
        }
    }
}
