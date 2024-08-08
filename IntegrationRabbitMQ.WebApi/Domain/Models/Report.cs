namespace IntegrationRabbitMQ.WebApi.Domain.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status? Status { get; set; }
        public DateTime? ProcessedTime { get; set; }
    }
    public enum Status
    {
        Success,
        Processing
    }
}
