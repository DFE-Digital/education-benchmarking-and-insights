using Newtonsoft.Json;

namespace Platform.Domain.DataObjects;

public class FloorAreaDataObject
{
    [JsonProperty(PropertyName = "URN")] public int Urn { get; set; }
    [JsonProperty(PropertyName = "Sum_Area")] public int FloorArea { get; set; }
}