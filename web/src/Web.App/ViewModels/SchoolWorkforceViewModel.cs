using Web.App.Domain;

namespace Web.App.ViewModels
{
    public class SchoolWorkforceViewModel(School school, FinanceYears years)
    {
        public string? Urn => school.Urn;
        public string? Name => school.Name;
        public string AcademiesFinancePeriod => $"{years.Academies - 1} / {years.Academies}";
        public string MaintainedSchoolsFinancePeriod => $"{years.MaintainedSchools - 1} - {years.MaintainedSchools}";
    }
}