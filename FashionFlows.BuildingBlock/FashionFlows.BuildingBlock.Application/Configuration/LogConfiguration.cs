using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

namespace FashionFlows.BuildingBlock.Application.Configuration
{
    public static class LogConfiguration
    {
        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            // Set up logs for server
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.GrafanaLoki("http://loki:3100")
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
