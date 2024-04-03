using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record FloorAreaDataObject
{
    [JsonProperty(PropertyName = "URN")] public int Urn { get; set; }
    [JsonProperty(PropertyName = "Sum_Area")] public int FloorArea { get; set; }
}