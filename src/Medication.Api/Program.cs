using FluentValidation;
using Medication.Api.Application.Behaviors;
using Medication.Api.Extensions;
using Medication.Api.Middleware;
using Medication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();

    builder.Services.AddMediatR(cfg => {
        cfg.RegisterServicesFromAssemblyContaining<Program>();
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });
    builder.Services.AddValidatorsFromAssemblyContaining<Program>();
    builder.Services.AddMedicationInfrastructureServices(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();

    app.MapApiEndpoints();

    app.PrepareDatabase();

    app.Run();
}

