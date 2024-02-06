using System.Text;
using EducationBenchmarking.Platform.Domain;
using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace EducationBenchmarking.Platform.Functions.Extensions;

public static class JsonExtensions
{
    private static readonly JsonConverter ProximitySortConverter = JsonSubtypesConverterBuilder
        .Of<ProximitySort>("Kind")
        .RegisterSubtype<SenProximitySort>(ProximitySortKinds.Sen)
        .RegisterSubtype<SimpleProximitySort>(ProximitySortKinds.Simple)
        .RegisterSubtype<BestInClassProximitySort>(ProximitySortKinds.Bic)
        .SetFallbackSubtype<UnknownProximitySort>()
        .SerializeDiscriminatorProperty()
        .Build();
    
    public static JsonSerializerSettings Settings => new()
    {
        NullValueHandling = NullValueHandling.Ignore, 
        ContractResolver = new CamelCasePropertyNamesContractResolver(), 
        CheckAdditionalContent = false,
        Converters = new List<JsonConverter>
        {
            new IsoDateTimeConverter(),
            new StringEnumConverter(),
            ProximitySortConverter
        }
    };

    public static string ToJson(this object? source, Formatting formatting = Formatting.Indented)
    {
        return JsonConvert.SerializeObject(source, formatting, Settings);
    }

    public static Stream ToJsonStream(this object source, Formatting formatting = Formatting.Indented)
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);
        var jw = new JsonTextWriter(streamWriter)
        {
            CloseOutput = false,
            Formatting = formatting
        };

        var serializer = JsonSerializer.CreateDefault(Settings);
        serializer.Serialize(jw, source, source.GetType());
        jw.Flush();
        jw.Close();
            
        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }

    public static byte[] ToJsonByteArray(this object? source)
    {
        return Encoding.UTF8.GetBytes(ToJson(source));
    }

    public static JArray ToJsonArray(this object source)
    {
        return JArray.FromObject(source);
    }

    public static T FromJson<T>(this string source)
    {
        return JsonConvert.DeserializeObject<T>(source, Settings) ?? throw new NullReferenceException();
    }

    public static T FromJson<T>(this byte[] source, Encoding? encoding = null)
    {
        if (source == null || source.Length == 0)
            throw new ArgumentException("The source was empty", nameof(source));

        using (var sr = new StreamReader(new MemoryStream(source), encoding ?? Encoding.UTF8))
        using (var jr = new JsonTextReader(sr))
        {
            var js = JsonSerializer.CreateDefault(Settings);

            return js.Deserialize<T>(jr) ?? throw new NullReferenceException();
        }
    }

    public static async Task<T> FromJson<T>(this Task<Stream> stream)
    {
        using (var sr = new StreamReader(await stream))
        using (var jr = new JsonTextReader(sr))
        {
            var js = JsonSerializer.CreateDefault(Settings);

            return js.Deserialize<T>(jr) ?? throw new NullReferenceException();
        }
    }
    
    public static async Task<T> FromJsonAsync<T>(this Stream stream)
    {
        var streamCopy = new MemoryStream();
        await stream.CopyToAsync(streamCopy);
        streamCopy.Position = 0;
        
        using var sr = new StreamReader(streamCopy);
        using var jr = new JsonTextReader(sr);
        
        var js = JsonSerializer.CreateDefault(Settings);

        return js.Deserialize<T>(jr) ?? throw new NullReferenceException();
    }

    public static T FromJson<T>(this Stream stream)
    {
        using (var sr = new StreamReader(stream))
        using (var jr = new JsonTextReader(sr))
        {
            var js = JsonSerializer.CreateDefault(Settings);

            return js.Deserialize<T>(jr) ?? throw new NullReferenceException();
        }
    }

    public static async IAsyncEnumerable<T> FromJsonArrayAsync<T>(this Stream stream)
    {
        var js = JsonSerializer.CreateDefault(Settings);
        using var sr = new StreamReader(stream);
        using var jr = new JsonTextReader(sr);
        while (await jr.ReadAsync())
        {
            if (jr.TokenType == JsonToken.StartObject)
            {
                yield return js.Deserialize<T>(jr) ?? throw new NullReferenceException();
            }
        }
    }


}