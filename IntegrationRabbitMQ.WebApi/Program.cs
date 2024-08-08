using IntegrationRabbitMQ.WebApi.Application;
using IntegrationRabbitMQ.WebApi.Domain.Interfaces;
using IntegrationRabbitMQ.WebApi.Domain.Models;
using IntegrationRabbitMQ.WebApi.Extensions;
using IntegrationRabbitMQ.WebApi.Infra;
using IntegrationRabbitMQ.WebApi.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

string postegresqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(postegresqlConnection)
);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuring rabbitMQ
builder.Services.AddRabbitMQService();
builder.Services.AddCustomServices();

//services
builder.Services.AddScoped<IRabbitMQ, RabbitMQApp>();
builder.Services.AddScoped<IReport, ReportApp>();
builder.Services.AddScoped<IReportRepository<Report>, ReportRepository>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
