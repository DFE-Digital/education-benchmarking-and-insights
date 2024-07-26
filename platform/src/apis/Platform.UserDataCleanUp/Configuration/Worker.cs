using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Worker;
namespace Platform.UserDataCleanUp.Configuration;

[ExcludeFromCodeCoverage]
internal static class Worker
{
    internal static void Configure(IFunctionsWorkerApplicationBuilder builder)
    {
    }

    internal static void Options(WorkerOptions options)
    {
        options.EnableUserCodeException = true;
    }
}