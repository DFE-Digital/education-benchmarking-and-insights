using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolSpendingViewModel(
    School school,
    IEnumerable<SchoolExpenditure> pupilExpenditure,
    IEnumerable<SchoolExpenditure> areaExpenditure,
    FinanceYears latestYears)
{
    public string? Name => school.Name;
    public string? Urn => school.Urn;
    public FinanceYears LatestYears => latestYears;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public IEnumerable<CostCategory> Categories => CategoryBuilder.Build(pupilExpenditure, areaExpenditure);
}