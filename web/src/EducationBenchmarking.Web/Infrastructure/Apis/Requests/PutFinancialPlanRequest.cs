using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PutFinancialPlanRequest
{
    public int Year { get; set; }
    public string Urn { get; set; }
    public bool? UseFigures { get; set; } 
    public int? SelectedYear { get; set; }
    public string Name { get; set; }
    public string TotalIncome { get; set; }
    public string TotalExpenditure { get; set; }
    public string TotalTeacherCosts { get; set; }
    public string TotalNumberOfTeachersFte { get; set; }
    public string EducationSupportStaffCosts { get; set; }
}