using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public class SchoolRisksHistoryViewModel(School school)
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public string? LaCode => school.LACode;
    public Views.ViewAsOptions ViewAs { get; init; } = Views.ViewAsOptions.Chart;
}
