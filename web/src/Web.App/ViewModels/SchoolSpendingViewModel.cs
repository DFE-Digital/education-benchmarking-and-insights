using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolSpendingViewModel(
    School school,
    IEnumerable<SchoolExpenditure> pupilExpenditure,
    IEnumerable<SchoolExpenditure> areaExpenditure)
{
    public string? Name => school.Name;
    public string? Urn => school.Urn;

    public Dictionary<string, Dictionary<string, Category>> Categories => CategoryBuilder.Build(pupilExpenditure, areaExpenditure);
}