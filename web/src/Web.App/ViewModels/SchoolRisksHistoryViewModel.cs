using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels;

public class SchoolRisksHistoryViewModel(School school, RiskHistoryTrends risksHistory)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public string? LaCode => school.LACode;
    public RiskHistoryTrends RisksHistory => risksHistory;
    public IReadOnlyList<RiskSectionViewModel> Sections { get; } =
    [
        new("Overall risk score", risksHistory.Overall),
        new("Financial risk score", risksHistory.Financial),
        new("Educational performance risk score*", risksHistory.EducationalPerformance,
            "*From financial year 2024/25, OFSTED risk has been removed from the risk score calculation. Also, KS2 progress score is not available for academic years 2023/24 and 2024/25. KS4 progress score is not available for academic year 2024/25. You may see a decrease in the educational performance risk score as a result."),
        new("School & pupil risk score", risksHistory.SchoolAndPupil)
    ];
    public Views.ViewAsOptions ViewAs { get; init; } = Views.ViewAsOptions.Chart;
}

public record RiskSectionViewModel(
    string Title,
    RiskHistorySeries Series,
    string? Footnote = null);
