using IntegrationRabbitMQ.WebApi.Domain.Interfaces;
using IntegrationRabbitMQ.WebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationRabbitMQ.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReport _reportService;

        public ReportController(IReport reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("newReport")]
        public IActionResult NewMap([FromBody] Report report)
        {
            _reportService.newReport(report);
            return Ok();
        }

        [HttpPost("generateReport/{id}/{name}")]
        public async Task<IActionResult> GenerateReport(int id, string name)
        {
            var result = await _reportService.GenerateReport(id, name);
            return Ok(result);
        }
    }
}