using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Extensions;

namespace Web.App.ViewModels;

public class SchoolRisksViewModel(
    School school,
    LocalAuthorityRiskIndicators risks,
    IEnumerable<RisksMetrics> metrics)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public string? LaCode => school.LACode;
    public LocalAuthorityRiskIndicators Risks => risks;

    public IReadOnlyCollection<RiskGroupViewModel> RiskGroups => metrics
    .GroupBy(m => m.RiskGroup, StringComparer.OrdinalIgnoreCase)
    .Select(group =>
    {
        var (title, value, max) = GetGroupDetails(group.Key, risks);

        var sortedMetrics = group
            .Select(m => new RiskMetricViewModel(m))
            .OrderBy(m => m.SortValue)
            .ThenByDescending(m => m.RiskIndicatorContribution)
            .ThenBy(m => m.RiskIndicator)
            .ToList();

        return new RiskGroupViewModel(
            Title: title,
            Key: group.Key,
            LaCode: LaCode,
            GroupValue: value,
            GroupMax: max,
            Metrics: sortedMetrics
        );
    })
    .OrderBy(g => GetGroupSortOrder(g.Key))
    .ToList();

    private static int GetGroupSortOrder(string key) => key switch
    {
        LocalAuthorityRisks.FinancialRiskGroup => 1,
        LocalAuthorityRisks.EducationalPerformanceRiskGroup => 2,
        LocalAuthorityRisks.SchoolAndPupilRiskGroup => 3,
        _ => 4
    };

    private static (string Title, decimal? Value, decimal? Max) GetGroupDetails(
    string riskGroup,
    LocalAuthorityRiskIndicators risks) =>
        riskGroup switch
        {
            LocalAuthorityRisks.FinancialRiskGroup =>
                (LocalAuthorityRisks.FinancialTitle, risks.Financial, risks.FinancialMax),

            LocalAuthorityRisks.SchoolAndPupilRiskGroup =>
                (LocalAuthorityRisks.SchoolAndPupilTitle, risks.SchoolAndPupil, risks.SchoolAndPupilMax),

            LocalAuthorityRisks.EducationalPerformanceRiskGroup =>
                (LocalAuthorityRisks.EducationalPerformanceTitle, risks.EducationalPerformance, risks.EducationalPerformanceMax),

            _ => (riskGroup, null, null)
        };
}

public record RiskGroupViewModel(
    string Title,
    string Key,
    string? LaCode,
    decimal? GroupValue,
    decimal? GroupMax,
    IReadOnlyCollection<RiskMetricViewModel> Metrics
);

public record RiskMetricViewModel(RisksMetrics Metric)
{
    public string RiskIndicator => Metric.RiskIndicator;
    public string RiskIndicatorFlag => Metric.RiskIndicatorFlag;
    public decimal RiskIndicatorContribution => Metric.RiskIndicatorContribution;
    public decimal RiskIndicatorContributionMax => Metric.RiskIndicatorContributionMax;

    public string ValueDisplay => FormatMetricValue(Metric.RiskIndicatorValueFormatting, Metric.RiskIndicatorValue);

    public string TagColourClass => Metric.RiskIndicatorFlag switch
    {
        LocalAuthorityRiskFlags.Major => "govuk-tag--red",
        LocalAuthorityRiskFlags.Minor => "govuk-tag--yellow",
        _ => "govuk-tag--grey"
    };

    public int SortValue => Metric.RiskIndicatorFlag switch
    {
        LocalAuthorityRiskFlags.Major => 1,
        LocalAuthorityRiskFlags.Minor => 2,
        _ => 3
    };

    /// <summary>
    /// Formats metric values based on the strongly-typed <see cref="RiskIndicatorValueFormatting"/> discriminator from the domain.
    /// Note: Both 'Boolean' and 'String' formats fall back to the default passthrough behavior since boolean indicators
    /// are pre-serialised as display strings (e.g., "Yes"/"No") upstream in the data pipeline.
    /// </summary>
    private static string FormatMetricValue(string formatType, string? value) => formatType switch
    {
        RiskIndicatorValueFormatting.Percentage when decimal.TryParse(value, out var decimalVal) =>
            (decimalVal * 100).ToPercent(),

        RiskIndicatorValueFormatting.Decimal when decimal.TryParse(value, out var decimalVal) =>
            decimalVal.ToSimpleDisplay(),
        _ => value ?? "Missing"
    };
}
