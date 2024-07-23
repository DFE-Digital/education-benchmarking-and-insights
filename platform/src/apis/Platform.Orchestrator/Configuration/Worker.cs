using System.Diagnostics.CodeAnalysis;
using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker;
using Platform.Functions.Extensions;
namespace Platform.Orchestrator.Configuration;

[ExcludeFromCodeCoverage]
internal static class Worker
{
    internal static void Configure(IFunctionsWorkerApplicationBuilder builder)
    {
    }

    internal static void Options(WorkerOptions options)
    {
        options.EnableUserCodeException = true;
        options.Serializer = new NewtonsoftJsonObjectSerializer(JsonExtensions.Settings);
    }
}