using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record RatingsDataObject
{
    public int RAGRatingId { get; set; }
    public int academies_data_Id { get; set; }
    public string URN { get; set; }
    public string UKPRN { get; set; }
    public string UKPRN_URN { get; set; }
    public string RAGAcademyName { get; set; }
    public string Sector { get; set; }
    public string CostPool { get; set; }
    public int CostPoolID { get; set; }
    public string CostGroup { get; set; }
    public string PeerGroup { get; set; }
    public decimal PerUnit { get; set; }
    public decimal MidDecile { get; set; }
    public int Decile { get; set; }
    public int RAG { get; set; }
    public int Rank { get; set; }
    public string PerUnitText { get; set; }
    public string ReductionText { get; set; }
    public int RankAcademyLevel { get; set; }
    public string RAGletter { get; set; }
    public string RAGWord { get; set; }
    public int RAGOrder { get; set; }
    public int OverallAcademyRank { get; set; }
    public int OverallCostPoolRank { get; set; }
    public int PerUnitRank { get; set; }
    public string? Comparators { get; set; }
    public string PoundDiffText { get; set; }
    public string PercentDiffText { get; set; }
    public string HomepageText { get; set; }
    public string Key { get; set; }
    public decimal PerUnit_PreviousYear { get; set; }
    public decimal YOYchange { get; set; }
    public string YOYchangeText { get; set; }
    public string PerUnit_PreviousYearText { get; set; }
}