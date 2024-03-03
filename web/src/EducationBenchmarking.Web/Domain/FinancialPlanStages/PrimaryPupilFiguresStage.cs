using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.Domain.FinancialPlanStages;

public class PrimaryPupilFiguresStage : Stage
{
    public decimal? PupilsNursery { get; set; }
    public string? PupilsMixedReceptionYear1 { get; set; }
    public string? PupilsMixedYear1Year2 { get; set; }
    public string? PupilsMixedYear2Year3 { get; set; }
    public string? PupilsMixedYear3Year4 { get; set; }
    public string? PupilsMixedYear4Year5 { get; set; }
    public string? PupilsMixedYear5Year6 { get; set; }
    public string? PupilsReception { get; set; }
    public string? PupilsYear1 { get; set; }
    public string? PupilsYear2 { get; set; }
    public string? PupilsYear3 { get; set; }
    public string? PupilsYear4 { get; set; }
    public string? PupilsYear5 { get; set; }
    public string? PupilsYear6 { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.PupilsNursery = PupilsNursery;
        plan.PupilsMixedReceptionYear1 = PupilsMixedReceptionYear1;
        plan.PupilsMixedYear1Year2 = PupilsMixedYear1Year2;
        plan.PupilsMixedYear2Year3 = PupilsMixedYear2Year3;
        plan.PupilsMixedYear3Year4 = PupilsMixedYear3Year4;
        plan.PupilsMixedYear4Year5 = PupilsMixedYear4Year5;
        plan.PupilsMixedYear5Year6 = PupilsMixedYear5Year6;
        plan.PupilsReception = PupilsReception;
        plan.PupilsYear1 = PupilsYear1;
        plan.PupilsYear2 = PupilsYear2;
        plan.PupilsYear3 = PupilsYear3;
        plan.PupilsYear4 = PupilsYear4;
        plan.PupilsYear5 = PupilsYear5;
        plan.PupilsYear6 = PupilsYear6;

        ResetTeacherFigures(plan);
        //TODO : Set teaching assistant figures
    }

    private void ResetTeacherFigures(FinancialPlan plan)
    {
        plan.TeachersNursery = PupilsNursery > 0 ? plan.TeachersNursery : null;
        plan.TeachersMixedReceptionYear1 = PupilsMixedReceptionYear1.ToInt() > 0 ? plan.TeachersMixedReceptionYear1 : null;
        plan.TeachersMixedYear1Year2 = PupilsMixedYear1Year2.ToInt() > 0 ? plan.TeachersMixedYear1Year2 : null;
        plan.TeachersMixedYear2Year3 = PupilsMixedYear2Year3.ToInt() > 0 ? plan.TeachersMixedYear2Year3 : null;
        plan.TeachersMixedYear3Year4 = PupilsMixedYear3Year4.ToInt() > 0 ? plan.TeachersMixedYear3Year4 : null;
        plan.TeachersMixedYear4Year5 = PupilsMixedYear4Year5.ToInt() > 0 ? plan.TeachersMixedYear4Year5 : null;
        plan.TeachersMixedYear5Year6 = PupilsMixedYear5Year6.ToInt() > 0 ? plan.TeachersMixedYear5Year6 : null;
        plan.TeachersReception = PupilsReception.ToInt() > 0 ? plan.TeachersReception : null;
        plan.TeachersYear1 = PupilsYear1.ToInt() > 0 ? plan.TeachersYear1 : null;
        plan.TeachersYear2 = PupilsYear2.ToInt() > 0 ? plan.TeachersYear2 : null;
        plan.TeachersYear3 = PupilsYear3.ToInt() > 0 ? plan.TeachersYear3 : null;
        plan.TeachersYear4 = PupilsYear4.ToInt() > 0 ? plan.TeachersYear4 : null;
        plan.TeachersYear5 = PupilsYear5.ToInt() > 0 ? plan.TeachersYear5 : null;
        plan.TeachersYear6 = PupilsYear6.ToInt() > 0 ? plan.TeachersYear6 : null;
    }
}