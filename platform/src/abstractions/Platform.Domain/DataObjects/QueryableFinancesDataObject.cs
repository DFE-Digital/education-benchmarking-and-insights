using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record QueryableFinancesDataObject
{
    [JsonProperty(PropertyName = SchoolTrustFinancialDataObjectFieldNames.URN)]
    public long Urn { get; set; }
}