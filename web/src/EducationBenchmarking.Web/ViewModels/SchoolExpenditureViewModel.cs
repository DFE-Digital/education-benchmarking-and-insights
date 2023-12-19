using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels
{
    public class SchoolExpenditureViewModel
    {
        private readonly School _school;
        private readonly Finances _finances;

        public SchoolExpenditureViewModel(School school, Finances finances)
        {
            _school = school;
            _finances = finances;
        }

        public string Urn => _school.Urn;
        public string Name => _school.Name;
        public string FinancePeriod => $"{_finances.YearEnd - 1}/{_finances.YearEnd}";
    }
}
