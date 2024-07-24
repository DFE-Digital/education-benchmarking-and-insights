using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Platform.Functions;
using Platform.Functions.Middleware;
namespace Platform.Api.Insight.Configuration;

[ExcludeFromCodeCoverage]
internal static class Worker
{
    internal static void Configure(IFunctionsWorkerApplicationBuilder builder)
    {
        builder.UseWhen<CorrelationHeaderMiddleware>(context =>
        {
            // We want to use this middleware only for http trigger invocations.
            return context.FunctionDefinition.InputBindings.Values.First(a => a.Type.EndsWith("Trigger")).Type == "httpTrigger";
        });
    }

    internal static void Options(WorkerOptions options)
    {
        options.EnableUserCodeException = true;
    }
}