using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Import.Abstractions;

[ExcludeFromCodeCoverage]
public record ComparatorSetLookup
{
    public string? Urn { get; set; }
    public string PeerGroup { get; set; }
    public string CostGroup { get; set; }

    public List<ComparatorSetLookupEntry> Entries { get; set; }

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

    
    /*public static ComparatorSetLookup Create(ComparatorSetLookupDataObject dataObject)
    {
        return new ComparatorSetLookup
        {
            Urn = dataObject.Urn,
            PeerGroup = dataObject.PeerGroup,
            CostGroup = dataObject.CostGroup,
            Entries = dataObject.Entries.Select(e => new ComparatorSetLookupEntry
            {
                ComparatorCode = e.ComparatorCode,
                Compare = e.Compare,
                CompareNum = e.CompareNum,
                DataReleaseId = e.DataReleaseId,
                PartYearDataFlag = e.PartYearDataFlag,
                RangeFlag = e.RangeFlag,
                Rank2 = e.Rank2,
                Rank3 = e.Rank3,
                ReprocessFlag = e.ReprocessFlag,
                UkPrnUrn1 = e.UkPrnUrn1,
                UkPrnUrn2 = e.UkPrnUrn2,
                UkPrnUrnCg = e.UkPrnUrnCg,
                UseAllCompFlag = e.UseAllCompFlag
            }).ToList()
        };
    }*/
}