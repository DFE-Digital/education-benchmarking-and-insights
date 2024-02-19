using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace EducationBenchmarking.Platform.Functions.Extensions;

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
    
    public static T FromJson<T>(this byte[] source, Encoding? encoding = null)
    {
        if (source == null || source.Length == 0)
            throw new ArgumentException("The source was empty", nameof(source));

        using (var sr = new StreamReader(new MemoryStream(source), encoding ?? Encoding.UTF8))
        using (var jr = new JsonTextReader(sr))
        {
            var js = JsonSerializer.CreateDefault(Settings);

            return js.Deserialize<T>(jr) ?? throw new ArgumentNullException();
        }
    }
    
    public static T FromJson<T>(this Stream stream)
    {
        using (var sr = new StreamReader(stream))
        using (var jr = new JsonTextReader(sr))
        {
            var js = JsonSerializer.CreateDefault(Settings);

            return js.Deserialize<T>(jr) ?? throw new ArgumentNullException();
        }
    }
}