using IntegrationRabbitMQ.WebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationRabbitMQ.WebApi.Infra
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"), optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Report>()
                .Property(r => r.Status)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string
                    v => (Status)Enum.Parse(typeof(Status), v) // Convert string to enum
                );
        }


    }



}
