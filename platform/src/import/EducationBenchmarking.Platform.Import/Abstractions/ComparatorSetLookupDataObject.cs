using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Import.Abstractions;

[ExcludeFromCodeCoverage]
public record ComparatorSetLookupDataObject
{
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("partitionKey")] public string? PartitionKey { get; set; } 
    [JsonProperty("peerGroup")] public string PeerGroup { get; set; }
    [JsonProperty("costGroup")] public string CostGroup { get; set; }
    [JsonProperty("entries")] public ComparatorSetLookupEntryDataObject[] Entries { get; set; }    
}

public record ComparatorSetLookupEntryDataObject
{
    [JsonProperty("comparatorCode")] public int? ComparatorCode { get; set; }
    [JsonProperty("compare")] public bool Compare { get; set; }
    [JsonProperty("compareNum")] public int CompareNum { get; set; }
    [JsonProperty("dataReleaseId")] public int? DataReleaseId { get; set; }
    [JsonProperty("partYearDataFlag")] public int PartYearDataFlag { get; set; }
    [JsonProperty("rangeFlag")] public int? RangeFlag { get; set; }
    [JsonProperty("rank2")] public int? Rank2 { get; set; }
    [JsonProperty("rank3")] public int? Rank3 { get; set; }
    [JsonProperty("reprocessFlag")] public int ReprocessFlag { get; set; }
    [JsonProperty("ukPrnUrn1")] public string UkPrnUrn1 { get; set; }
    [JsonProperty("ukPrnUrn2")] public string UkPrnUrn2 { get; set; }
    [JsonProperty("ukPrnUrnCg")] public string UkPrnUrnCg { get; set; }
    [JsonProperty("useAllCompFlag")] public int? UseAllCompFlag { get; set; }
}