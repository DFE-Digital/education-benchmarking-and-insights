using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Platform.Functions.Extensions;

public static class HttpRequestExtensions
{
    public static Guid GetCorrelationId(this HttpRequest req)
    {
        if (req.Headers.TryGetValue(Constants.CorrelationIdHeader, out var value))
        {
            return Guid.TryParse(value.ToString(), out var guid)
                ? guid
                : Guid.NewGuid();
        }

        return Guid.NewGuid();
    }

    public static T ReadAsJson<T>(this HttpRequest req)
    {
        using (var bodyReader = new StreamReader(req.BodyReader.AsStream(true)))
        using (var jsonReader = new JsonTextReader(bodyReader))
        {
            return JsonSerializer.CreateDefault(JsonExtensions.Settings).Deserialize<T>(jsonReader) ?? throw new ArgumentNullException();
        }
    }


    public static T GetParameters<T>(this HttpRequest req) where T : QueryParameters, new()
    {
        var parameters = new T();
        parameters.SetValues(req.Query);

        return parameters;
    }
}