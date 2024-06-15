namespace Web.App.Domain;

public class TotalNumberTeachersStage : Stage
{
    public decimal? TotalNumberOfTeachersFte { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.TotalNumberOfTeachersFte = TotalNumberOfTeachersFte;
    }
}