using Microsoft.AspNetCore.Http;

namespace EducationBenchmarking.Platform.Shared;

public static class HttpRequestExtensions
{
    public static Guid GetCorrelationId(this HttpRequest req)
    {
        if (req.Headers.TryGetValue(Constants.CorrelationIdHeader, out var value))
        {
            return Guid.Parse(value.ToString());
        }
            
        return Guid.NewGuid();
    }
}