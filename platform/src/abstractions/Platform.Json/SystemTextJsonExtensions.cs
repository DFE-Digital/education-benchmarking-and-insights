using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Platform.Json;

[ExcludeFromCodeCoverage]
public static class SystemTextJsonExtensions
{
    public static void Options(JsonSerializerOptions options)
    {
        options.AllowTrailingCommas = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.PropertyNameCaseInsensitive = true;
        options.Converters.Add(new JsonStringEnumConverter());
    }
}