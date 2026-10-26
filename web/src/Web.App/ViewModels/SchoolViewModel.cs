using Web.App.Domain;
using Web.App.Domain.Content;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class SchoolViewModel : ISchoolKeyInformationViewModel
{
    private readonly School _school;

    public SchoolViewModel(
        School school,
        SchoolBalance? balance,
        IEnumerable<RagRating> ratings,
        SchoolCharacteristic? characteristic,
        FinanceYears years,
        bool? comparatorGenerated = false,
        bool? comparatorReverted = false,
        string? userDefinedSetId = null,
        string? customDataId = null,
        string? compareSchoolPerformanceUrl = null)
    {
        _school = school;
        UserDefinedSetId = userDefinedSetId;
        CustomDataId = customDataId;
        InYearBalance = balance?.InYearBalance;
        RevenueReserve = balance?.RevenueReserve;
        PeriodCoveredByReturn = balance?.PeriodCoveredByReturn;
        ComparatorGenerated = comparatorGenerated;
        ComparatorReverted = comparatorReverted;
        KS4Progress = characteristic?.KS4Progress;
        KS4ProgressBanding = characteristic?.KS4ProgressBanding.ToBanding();
        Ks4ProgressYear = years.Ks4Progress;
        Attainment8Year = years.Attainment8;
        Attainment8 = characteristic?.Attainment8;
        LocalAuthorityAvgAttainment8 = characteristic?.LocalAuthorityAvgAttainment8;
        NationalAvgAttainment8 = characteristic?.NationalAvgAttainment8;
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

    public string? Name => _school.SchoolName;
    public string? Urn => _school.URN;
    public bool IsPartOfTrust => _school.IsPartOfTrust;
    public string? TrustIdentifier => _school.TrustCompanyNumber;
    public string? TrustName => _school.TrustName;
    public string? LaName => _school.LAName;

    public int? PeriodCoveredByReturn { get; }
    public FederationSchool[] FederationSchools => _school.FederationSchools;

    public string? UserDefinedSetId { get; }
    public string? CustomDataId { get; }
    public bool HasMetricRag { get; }
    public IEnumerable<RagRating> Ratings { get; }
    public bool? ComparatorGenerated { get; }
    public bool? ComparatorReverted { get; }

    public FinanceToolsViewModel Tools => _school.IsPartOfTrust
        ? new FinanceToolsViewModel(
            _school.URN,
            FinanceTools.CompareYourCosts,
            FinanceTools.FinancialPlanning,
            FinanceTools.BenchmarkCensus)
        : new FinanceToolsViewModel(
            _school.URN,
            FinanceTools.CompareYourCosts,
            FinanceTools.SpendingComparisonIt,
            FinanceTools.FinancialPlanning,
            FinanceTools.BenchmarkCensus);

    public decimal? KS4Progress { get; }
    public KS4ProgressBandings.Banding? KS4ProgressBanding { get; }
    public bool HasProgressIndicator => KS4Progress.HasValue && KS4ProgressBanding.HasValue;
    public int? Ks4ProgressYear { get; }

    public decimal? Attainment8 { get; }
    public decimal? LocalAuthorityAvgAttainment8 { get; }
    public decimal? NationalAvgAttainment8 { get; }
    public bool HasAttainment8 => Attainment8.HasValue;
    public int Attainment8Year { get; }

    public string? CompareSchoolPerformanceUrl { get; }

    public string? OverallPhase => _school.OverallPhase;
    public decimal? InYearBalance { get; }
    public decimal? RevenueReserve { get; }
}
