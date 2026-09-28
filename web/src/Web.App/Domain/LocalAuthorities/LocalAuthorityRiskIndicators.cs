using System.Diagnostics.CodeAnalysis;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Domain.LocalAuthorities;

[ExcludeFromCodeCoverage]
public record LocalAuthorityRiskIndicators
{
    public string Urn { get; init; } = string.Empty;
    public string SchoolName { get; init; } = string.Empty;
    public string OverallGrade { get; init; } = string.Empty;
    public decimal Overall { get; init; }
    public decimal OverallMax { get; init; }
    public string OverallGradeColour => MapGradeToTagColour(OverallGrade);
    public decimal Financial { get; init; }
    public decimal FinancialMax { get; init; }
    public decimal SchoolAndPupil { get; init; }
    public decimal SchoolAndPupilMax { get; init; }
    public decimal EducationalPerformance { get; init; }
    public decimal EducationalPerformanceMax { get; init; }

    private static string MapGradeToTagColour(string? grade) =>
        grade switch
        {
            "A*" or "A" or "B" or "C" => "green",
            "D" or "E" => "yellow",
            "F" or "G" => "red",
            _ => "grey"
        };
}

public record LocalAuthorityRiskIndicatorsHistory : LocalAuthorityRiskIndicators
{
    public int Year { get; init; }
}

public record LocalAuthorityRiskIndicatorsHistoryRows
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<LocalAuthorityRiskIndicatorsHistory> Rows { get; set; } = [];

    public RiskHistoryTrends ToTrends()
    {
        return new RiskHistoryTrends(
            Overall: MapSeries(r => r.Overall, r => r.OverallMax),
            Financial: MapSeries(r => r.Financial, r => r.FinancialMax),
            SchoolAndPupil: MapSeries(r => r.SchoolAndPupil, r => r.SchoolAndPupilMax),
            EducationalPerformance: MapSeries(r => r.EducationalPerformance, r => r.EducationalPerformanceMax)
        );
    }

    private RiskHistorySeries MapSeries(
        Func<LocalAuthorityRiskIndicatorsHistory, decimal> valueSelector,
        Func<LocalAuthorityRiskIndicatorsHistory, decimal> maxSelector)
    {
        if (!Rows.Any())
        {
            return new RiskHistorySeries();
        }

        var orderedRows = Rows.OrderBy(r => r.Year).ToList();

        return new RiskHistorySeries
        {
            MaxValue = maxSelector(orderedRows.First()),
            Data = orderedRows.Select(r => new RiskHistoryData(
                Year: $"{r.Year - 1} to {r.Year}",
                Value: valueSelector(r)
            ))
        };
    }
}

public record RiskHistoryTrends(
    RiskHistorySeries Overall,
    RiskHistorySeries Financial,
    RiskHistorySeries SchoolAndPupil,
    RiskHistorySeries EducationalPerformance);

public class RiskHistorySeries
{
    public IEnumerable<RiskHistoryData> Data { get; init; } = [];
    public decimal MaxValue { get; init; }
    public string? Uuid { get; set; }
    public string? ChartSvg { get; set; }
}

public record RiskHistoryData(string Year, decimal Value);
