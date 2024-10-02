using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Platform.Functions.Extensions;

public static class JsonExtensions
{
    public static JsonSerializerSettings Settings => new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        CheckAdditionalContent = false,
        Converters = new List<JsonConverter>
        {
            new IsoDateTimeConverter(),
            new StringEnumConverter()
        }
    };

    [ExcludeFromCodeCoverage]
    public static void Options(JsonSerializerOptions options)
    {
        options.AllowTrailingCommas = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.PropertyNameCaseInsensitive = true;
        options.Converters.Add(new JsonStringEnumConverter());
    }

    public static string ToJson(this object? source, Formatting formatting = Formatting.None) => JsonConvert.SerializeObject(source, formatting, Settings);

    public static T FromJson<T>(this string source) => JsonConvert.DeserializeObject<T>(source, Settings) ?? throw new ArgumentNullException();

    public static T FromJson<T>(this byte[]? source, Encoding? encoding = null)
    {
        if (source == null || source.Length == 0)
        {
            throw new ArgumentException("The source was empty", nameof(source));
        }

        using var sr = new StreamReader(new MemoryStream(source), encoding ?? Encoding.UTF8);
        using var jr = new JsonTextReader(sr);
        var js = JsonSerializer.CreateDefault(Settings);
        return js.Deserialize<T>(jr) ?? throw new ArgumentNullException();
    }
}