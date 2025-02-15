using FashionFlows.BuildingBlock.Infrastructure.Tracing.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FashionFlows.BuildingBlock.Infrastructure.Tracing;

public static class OpenTelemetryTracingExtension
{
    public static void AddOpenTelemetryTracing(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OpenTelemetrySetting>(configuration.GetSection("OpenTelemetry"));
        var openTelemetryParam = configuration.GetSection("OpenTelemetry").Get<OpenTelemetrySetting>();
        services.AddOpenTelemetry()
            .ConfigureResource(resources => resources.AddService(openTelemetryParam!.ServiceName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(a => a.SetDbStatementForText = true)
                    .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

                tracing.AddOtlpExporter(opt =>
                {
                    opt.Endpoint = new Uri("http://jaeger:4317");
                });
            });
    }
}
