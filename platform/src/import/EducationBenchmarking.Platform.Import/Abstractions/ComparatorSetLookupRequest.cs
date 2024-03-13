using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Import.Abstractions;

[ExcludeFromCodeCoverage]
public record ComparatorSetLookupRequest
{
    public string Urn { get; set; }
    public string PeerGroup { get; set; }
    public string CostGroup { get; set; }
    public ComparatorSetLookupEntry[] Entries { get; set; }
}

public record ComparatorSetLookupEntry
{
    public int? ComparatorCode { get; set; }
    public string Compare { get; set; }
    public int CompareNum { get; set; }
    public int? DataReleaseId { get; set; }
    public int PartYearDataFlag { get; set; }
    public int? RangeFlag { get; set; }
    public int? Rank2 { get; set; }
    public int? Rank3 { get; set; }
    public int ReprocessFlag { get; set; }
    public string UkPrnUrn1 { get; set; }
    public string UkPrnUrn2 { get; set; }
    public string UkPrnUrnCg { get; set; }
    public int? UseAllCompFlag { get; set; }
}