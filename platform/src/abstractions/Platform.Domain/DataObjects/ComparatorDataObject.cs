using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record ComparatorDataObject
{
    public int all_comparators_all_Id { get; set; }
    public int trust_academy_Id { get; set; }
    public int URN { get; set; }
    public string UKPRN_URN1 { get; set; }
    public string UKPRN_URN2 { get; set; }
    public string UKPRN_URN_CG { get; set; }
    public string PeerGroup { get; set; }
    public string CostGroup { get; set; }
    public bool CompareNum { get; set; }
    public bool compare { get; set; }
    public int RANK2 { get; set; }
    public int Comparator_code { get; set; }
    public int RANK3 { get; set; }
    public int ReprocessFlag { get; set; }
    public bool UseAllCompFlag { get; set; }
    public bool Range_flag { get; set; }
    public int DataReleaseId { get; set; }
    public int PartYearDataFlag { get; set; }
}