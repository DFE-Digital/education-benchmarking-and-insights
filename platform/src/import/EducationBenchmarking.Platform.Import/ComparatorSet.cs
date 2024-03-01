namespace EducationBenchmarking.Platform.Import;

public class ComparatorSet
{
    public int Urn { get; set; }

    public List<ComparatorEntry>? DefaultPupil { get; set; }

    public List<ComparatorEntry>? DefaultArea { get; set; }

    public List<ComparatorEntry>? MixedPupil { get; set; }
    
    public List<ComparatorEntry>? MixedArea { get; set; }

}

public class ComparatorEntry
{
    public int ComparatorCode { get; set; }
    public bool Compare { get; set; }
    public int CompareNum { get; set; }
    public string? CostGroup { get; set; }
    public int DataReleaseId { get; set; }
    public int PartYearDataFlag { get; set; }
    public string? PeerGroup { get; set; }
    public int RangeFlag { get; set; }
    public int Rank2 { get; set; }
    public int Rank3 { get; set; }
    public int ReprocessFlag { get; set; }
    public string? UkPrnUrn1 { get; set; }
    public string? UkPrnUrn2 { get; set; }
    public string? UkPrnUrnCg { get; set; }
    public int UseAllCompFlag { get; set; }
}