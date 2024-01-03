using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels
{
    public class SchoolComparisonViewModel
    {
        private readonly School _school;
        private readonly FinanceYears _years;

        public SchoolComparisonViewModel(School school, FinanceYears years)
        {
            _school = school;
            _years = years;
        }

        public string Urn => _school.Urn;
        public string Name => _school.Name;
        public string AcademiesFinancePeriod => $"{_years.Academies - 1} / {_years.Academies}";
        public string MaintainedSchoolsFinancePeriod => $"{_years.MaintainedSchools - 1} - {_years.MaintainedSchools}";
    }
    
}
