using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels
{
    public class SchoolComparisonViewModel(School school, FinanceYears years)
    {
        public string Urn => school.Urn;
        public string Name => school.Name;
        public string AcademiesFinancePeriod => $"{years.Academies - 1} / {years.Academies}";
        public string MaintainedSchoolsFinancePeriod => $"{years.MaintainedSchools - 1} - {years.MaintainedSchools}";
    }
    
}
