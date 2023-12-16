using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
    
    public static T ReadAsJson<T>(this HttpRequest req)
    {
        using (var bodyReader = new StreamReader(req.BodyReader.AsStream(true)))
        using (var jsonReader = new JsonTextReader(bodyReader))
        {
            return JsonSerializer.CreateDefault(JsonExtensions.Settings).Deserialize<T>(jsonReader) ?? throw new NullReferenceException();
        }
    }
}