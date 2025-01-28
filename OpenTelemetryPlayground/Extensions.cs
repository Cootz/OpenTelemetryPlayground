using OpenTelemetry;
using OpenTelemetry.Exporter;
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

            var otel = builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService("WeatherForecast"))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();
                });

            var OtlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
            if (OtlpEndpoint != null)
            {
                otel.UseOtlpExporter();
            }

            return builder;
        }
    }
}
