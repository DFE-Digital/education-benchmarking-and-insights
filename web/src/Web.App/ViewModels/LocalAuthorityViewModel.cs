using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Domain.Content;
using Web.App.Extensions;
using Web.App.ViewModels.Shared;

namespace Web.App.ViewModels;

public class LocalAuthorityViewModel(Domain.LocalAuthorities.LocalAuthority localAuthority, RagRatingSummary[] ragRatings, FinanceYears years)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int? NumberOfSchools => localAuthority.Schools.Length;

    public LocalAuthorityHeadlineStatisticsViewModel? HeadlineStatistics => new LocalAuthorityHeadlineStatisticsViewModel()
    {
        DsgHighNeedsAllocation = localAuthority.HeadlineStatistics.DsgHighNeedsAllocation,
        OutturnTotalHighNeeds = localAuthority.HeadlineStatistics.OutturnTotalHighNeeds,
        OutturnDsgCarriedForward = localAuthority.HeadlineStatistics.OutturnDsgCarriedForward,
        OutturnDsgGCarriedForwardPreviousPeriod = localAuthority.HeadlineStatistics.OutturnDsgCarriedForwardPreviousPeriod,
        S251Year = years.S251
    };

    public IEnumerable<IGrouping<string?, Domain.LocalAuthorities.LocalAuthoritySchool>> GroupedSchoolNames { get; } = localAuthority.Schools
        .OrderBy(x => x.SchoolName)
        .GroupBy(x => x.OverallPhase)
        .OrderBy(x => GetLaPhaseOrder(x.Key));

    public IEnumerable<(string? OverallPhase, IEnumerable<RagSchoolViewModel> Schools)> GroupedSchools => ragRatings
        .GroupBy(x => x.OverallPhase)
        .Select(x => (
            OverallPhase: x.Key,
            Schools: x
                .Select(s => new RagSchoolViewModel(
                    s.URN,
                    s.SchoolName,
                    s.RedCount ?? 0,
                    s.AmberCount ?? 0,
                    s.GreenCount ?? 0
                )).OrderByDescending(o => o.RedRatio)
                .ThenByDescending(o => o.AmberRatio)
                .ThenBy(o => o.Name)
                .Take(5)))
        .OrderBy(x => GetLaPhaseOrder(x.OverallPhase));

    public FinanceToolsViewModel Tools => new(
        localAuthority.Code,
        FinanceTools.CompareYourCosts,
        FinanceTools.BenchmarkCensus);

    private static int GetLaPhaseOrder(string? phase)
    {
        return phase switch
        {
            OverallPhaseTypes.Primary => 1,
            OverallPhaseTypes.Secondary => 2,
            OverallPhaseTypes.Special => 3,
            OverallPhaseTypes.PupilReferralUnit => 4,
            OverallPhaseTypes.Nursery => 5,
            OverallPhaseTypes.AllThrough => 6,
            OverallPhaseTypes.PostSixteen => 7,
            _ => 99
        };
    }

    public static class FormFieldNames
    {
        public const string Fragment = "__fragment";
        public const string OtherFormFields = "__otherForm";
        public const string ResetFields = "__resetFields";
    }
}

public class LocalAuthoritySchoolNamesSectionViewModel
{
    public string? Heading { get; init; }
    public int Id { get; init; }
    public IEnumerable<Domain.LocalAuthorities.LocalAuthoritySchool> Schools { get; init; } = [];
}

public class LocalAuthoritySchoolsSectionViewModel
{
    public string Heading { get; init; } = string.Empty;
    public IEnumerable<RagSchoolViewModel> Schools { get; init; } = [];
}

public class LocalAuthorityHeadlineStatisticsViewModel
{
    public decimal? DsgHighNeedsAllocation { get; set; }
    public decimal? OutturnTotalHighNeeds { get; set; }
    public decimal? OutturnDsgCarriedForward { get; set; }
    public decimal? OutturnDsgGCarriedForwardPreviousPeriod { get; set; }
    public int S251Year { get; set; }
    public decimal? OutturnAsPercentageOfAllocation => OutturnTotalHighNeeds.SafePercentageOf(DsgHighNeedsAllocation);
}