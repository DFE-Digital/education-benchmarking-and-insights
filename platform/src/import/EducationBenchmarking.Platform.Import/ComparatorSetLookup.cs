namespace EducationBenchmarking.Platform.Import;

public class ComparatorSetLookup
{
    public string Id { get; set; }
    
    public string Urn { get; set; }
    
    public string PeerGroup { get; set; }
    
    public string CostGroup { get; set; }

    public string PartitionKey => PeerGroup + "-" + CostGroup;

    public List<ComparatorSetLookupEntry>? ComparatorEntities { get; set; }
}

public class ComparatorSetLookupEntry
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

public class ComparatorEntryData
{
    public string URN { get; set; }

    public string UKPRN_URN1 { get; set; }

    public string UKPRN_URN2 { get; set; }

    public string UKPRN_URN_CG { get; set; }

    public string PeerGroup { get; set; }

    public string CostGroup { get; set; }

    public int CompareNum { get; set; }

    public string compare { get; set; }

    public int? RANK2 { get; set; }

    public int? Comparator_code { get; set; }

    public int? RANK3 { get; set; }

    public int ReprocessFlag { get; set; }

    public int? UseAllCompFlag { get; set; }

    public int? Range_flag { get; set; }

    public int? DataReleaseId { get; set; }

    public int PartYearDataFlag { get; set; }
}
