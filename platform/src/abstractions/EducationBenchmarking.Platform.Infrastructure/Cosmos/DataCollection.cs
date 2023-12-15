using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Infrastructure.Cosmos;

public class DataCollection
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    
    [JsonProperty(PropertyName = "active")]
    public string Active { get; set; }
    
    [JsonProperty(PropertyName = "data-group")]
    public string DataGroup { get; set; }
}