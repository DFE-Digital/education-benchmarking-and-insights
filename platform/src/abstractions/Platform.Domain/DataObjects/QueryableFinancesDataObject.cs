using Newtonsoft.Json;

namespace Platform.Domain;

public record QueryableFinancesDataObject
{
    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.URN)]
    public long Urn { get; set; }
}