using FashionFlows.BuildingBlock.Application.Configuration;
using FashionFlows.BuildingBlock.Infrastructure.GenericInstaller;
using FashionFlows.BuildingBlock.Infrastructure.Tracing;
using FashionFlows.Product.Infrastructure.Installers;
using Serilog;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Product_Service", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddOpenTelemetryTracing(builder.Configuration);
builder.Services.InstallServicesInAssembly<IProductAssembly>(builder.Configuration);

//builder.Services.AddServiceDiscovery(o => o.UseConsul());

var app = builder.Build();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.UseSwagger();
app.UseSwaggerUI();
app.UseSerilogRequestLogging();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
