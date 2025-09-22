using OpenTelemetry.Trace;
using Web.App.Extensions;

namespace Web.App.Instrumentation;

public static class Tracing
{
    public static void Configure(TracerProviderBuilder builder, IWebHostEnvironment environment)
    {
        builder
            .AddAspNetCoreInstrumentation(options =>
            {
                options.EnrichWithHttpRequest = (activity, request) =>
                {
                    activity.TrackAuthenticatedUserId(request);
                };
            })
            .AddHttpClientInstrumentation() ;

        if (environment.IsDevelopment())
        {
            builder.AddConsoleExporter();
        }
    }
}