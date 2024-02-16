using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PutFinancialPlanRequest
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public bool? UseFigures { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public int? TimetablePeriods { get; set; }

    public static PutFinancialPlanRequest Create(FinancialPlan plan)
    {
        return new PutFinancialPlanRequest
        {
            Year = plan.Year,
            Urn = plan.Urn,
            UseFigures = plan.UseFigures,
            TotalIncome = plan.TotalIncome,
            TotalExpenditure = plan.TotalExpenditure,
            TotalTeacherCosts = plan.TotalTeacherCosts,
            TotalNumberOfTeachersFte = plan.TotalNumberOfTeachersFte,
            EducationSupportStaffCosts = plan.EducationSupportStaffCosts,
            TimetablePeriods = plan.TimetablePeriods,
        };
    }
}