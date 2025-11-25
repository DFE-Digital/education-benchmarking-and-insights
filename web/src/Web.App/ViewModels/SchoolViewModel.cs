using Web.App.Domain;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolViewModel(School school) : ISchoolKeyInformationViewModel
{
    public SchoolViewModel(
        School school,
        SchoolBalance? balance,
        IEnumerable<RagRating> ratings,
        SchoolCharacteristic? characteristic,
        bool? comparatorGenerated = false,
        bool? comparatorReverted = false,
        string? userDefinedSetId = null,
        string? customDataId = null,
        string? compareSchoolPerformanceUrl = null)
        : this(school)
    {
        UserDefinedSetId = userDefinedSetId;
        CustomDataId = customDataId;
        InYearBalance = balance?.InYearBalance;
        RevenueReserve = balance?.RevenueReserve;
        PeriodCoveredByReturn = balance?.PeriodCoveredByReturn;
        ComparatorGenerated = comparatorGenerated;
        ComparatorReverted = comparatorReverted;
        KS4Progress = characteristic?.KS4Progress;
        KS4ProgressBanding = characteristic?.KS4ProgressBanding.ToBanding();
        CompareSchoolPerformanceUrl = compareSchoolPerformanceUrl;

        var ratingsArray = ratings.ToArray();

        HasMetricRag = ratingsArray.Length != 0;
        Ratings = ratingsArray
            .Where(x => x.RAG is "red" or "amber" && x.Category is not Category.Other)
            .OrderBy(x => Lookups.StatusOrderMap[x.RAG ?? string.Empty])
            .ThenByDescending(x => x.Decile)
            .ThenByDescending(x => x.Value)
            .Take(3);
    }

    public SchoolViewModel(
        School school,
        bool? customDataGenerated = false)
        : this(school)
    {
        CustomDataGenerated = customDataGenerated;
    }

    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? TrustIdentifier => school.TrustCompanyNumber;
    public string? TrustName => school.TrustName;

    public int? PeriodCoveredByReturn { get; }
    public FederationSchool[] FederationSchools => school.FederationSchools;

    public string? UserDefinedSetId { get; }
    public string? CustomDataId { get; }
    public bool HasMetricRag { get; }
    public IEnumerable<RagRating> Ratings { get; } = [];
    public bool? ComparatorGenerated { get; }
    public bool? ComparatorReverted { get; }
    public bool? CustomDataGenerated { get; }

    public FinanceToolsViewModel Tools => school.IsPartOfTrust
        ? new FinanceToolsViewModel(
            school.URN,
            FinanceTools.CompareYourCosts,
            FinanceTools.FinancialPlanning,
            FinanceTools.BenchmarkCensus)
        : new FinanceToolsViewModel(
            school.URN,
            FinanceTools.CompareYourCosts,
            FinanceTools.SpendingComparisonIt,
            FinanceTools.FinancialPlanning,
            FinanceTools.BenchmarkCensus);

    public FinanceToolsViewModel CustomTools => new(
        school.URN,
        FinanceTools.SpendingComparison,
        FinanceTools.CompareYourCosts,
        FinanceTools.Spending,
        FinanceTools.BenchmarkCensus);

    public decimal? KS4Progress { get; }
    public KS4ProgressBandings.Banding? KS4ProgressBanding { get; }
    public bool HasProgressIndicator => KS4Progress.HasValue && KS4ProgressBanding.HasValue;

    public string? CompareSchoolPerformanceUrl { get; }

    public string? OverallPhase => school.OverallPhase;
    public decimal? InYearBalance { get; }
    public decimal? RevenueReserve { get; }
}