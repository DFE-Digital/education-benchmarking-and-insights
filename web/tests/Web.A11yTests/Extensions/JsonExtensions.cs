using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
namespace Web.A11yTests.Extensions;

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
    public static string ToJson(this object? source, Formatting formatting = Formatting.Indented)
    {
        return JsonConvert.SerializeObject(source, formatting, Settings);
    }
}