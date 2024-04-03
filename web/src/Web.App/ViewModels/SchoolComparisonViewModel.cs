using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparisonViewModel(School school, FinanceYears years)
{
    public string? Urn => school.Urn;
    public string? Name => school.Name;
    public string AcademiesFinancePeriod => $"{years.Aar - 1} / {years.Aar}";
    public string MaintainedSchoolsFinancePeriod => $"{years.Cfr - 1} - {years.Cfr}";
}