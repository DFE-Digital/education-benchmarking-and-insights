namespace EducationBenchmarking.Web.Domain.FinancialPlanStages;

public class TotalNumberTeachersStage : Stage
{
    public decimal? TotalNumberOfTeachersFte { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.TotalNumberOfTeachersFte = TotalNumberOfTeachersFte;
    }
}