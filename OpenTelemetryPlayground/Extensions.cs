using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OpenTelemetryPlayground
{
    public static class Extensions
    {
        public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
        {
            builder.Logging.AddOpenTelemetry(openTelemetry =>
            {
                openTelemetry.IncludeScopes = true;
                openTelemetry.IncludeFormattedMessage = true;
            });

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService("WeatherForecast"))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();

                    metrics.AddPrometheusExporter();
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();

                    tracing.AddOtlpExporter();
                });

            builder.Logging.AddOpenTelemetry(loging => loging.AddOtlpExporter());
            
            return builder;
        }
    }
}
