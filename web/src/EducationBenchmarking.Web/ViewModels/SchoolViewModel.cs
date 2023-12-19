using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolViewModel
{
    private readonly School _school;
    private readonly Finances _finances;

    public SchoolViewModel(School school, Finances finances)
    {
        _school = school;
        _finances = finances;
    }
    
    public string Name => _school.Name;
    public string Urn => _school.Urn;
    public string LastFinancialYear => $"{_finances.YearEnd}";
}