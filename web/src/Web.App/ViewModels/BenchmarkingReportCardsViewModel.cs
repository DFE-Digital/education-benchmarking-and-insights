using Web.App.Domain;
namespace Web.App.ViewModels;

public class BenchmarkingReportCardsViewModel(School school)
{
    public string? Name => school.SchoolName;
}