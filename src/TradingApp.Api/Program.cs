using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
// using TradingApp.Application; // To be added when Application extension is created
// using TradingApp.Infrastructure; // To be added when Infrastructure extension is created

var builder = WebApplication.CreateBuilder(args);

// 1. Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// 2. Add Layer Dependencies (DI)
// builder.Services.AddApplicationServices(); // To be implemented in next phases
// builder.Services.AddInfrastructureServices(builder.Configuration); // To be implemented in next phases

// 3. Add built-in services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TradingApp API", Version = "v1" });
});

// 4. Health Checks
builder.Services.AddHealthChecks();
// In future phases: .AddNpgSql(...) .AddRedis(...)

var app = builder.Build();

// 5. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TradingApp API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseSerilogRequestLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Map Health Check Endpoint
app.MapHealthChecks("/api/health");

app.Run();
