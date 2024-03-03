namespace EducationBenchmarking.Web.Domain.FinancialPlanStages;

public class TeacherPeriodAllocationStage : Stage
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
    public string? PupilsYear7 { get; set; }
    public string? PupilsYear8 { get; set; }
    public string? PupilsYear9 { get; set; }
    public string? PupilsYear10 { get; set; }
    public string? PupilsYear11 { get; set; }
    public decimal? PupilsYear12 { get; set; }
    public decimal? PupilsYear13 { get; set; }
    public string? TeachersNursery { get; set; }
    public string? TeachersMixedReceptionYear1 { get; set; }
    public string? TeachersMixedYear1Year2 { get; set; }
    public string? TeachersMixedYear2Year3 { get; set; }
    public string? TeachersMixedYear3Year4 { get; set; }
    public string? TeachersMixedYear4Year5 { get; set; }
    public string? TeachersMixedYear5Year6 { get; set; }
    public string? TeachersReception { get; set; }
    public string? TeachersYear1 { get; set; }
    public string? TeachersYear2 { get; set; }
    public string? TeachersYear3 { get; set; }
    public string? TeachersYear4 { get; set; }
    public string? TeachersYear5 { get; set; }
    public string? TeachersYear6 { get; set; }
    public string? TeachersYear7 { get; set; }
    public string? TeachersYear8 { get; set; }
    public string? TeachersYear9 { get; set; }
    public string? TeachersYear10 { get; set; }
    public string? TeachersYear11 { get; set; }
    public string? TeachersYear12 { get; set; }
    public string? TeachersYear13 { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.TeachersNursery = TeachersNursery;
        plan.TeachersMixedReceptionYear1 = TeachersMixedReceptionYear1;
        plan.TeachersMixedYear1Year2 = TeachersMixedYear1Year2;
        plan.TeachersMixedYear2Year3 = TeachersMixedYear2Year3;
        plan.TeachersMixedYear3Year4 = TeachersMixedYear3Year4;
        plan.TeachersMixedYear4Year5 = TeachersMixedYear4Year5;
        plan.TeachersMixedYear5Year6 = TeachersMixedYear5Year6;
        plan.TeachersReception = TeachersReception;
        plan.TeachersYear1 = TeachersYear1;
        plan.TeachersYear2 = TeachersYear2;
        plan.TeachersYear3 = TeachersYear3;
        plan.TeachersYear4 = TeachersYear4;
        plan.TeachersYear5 = TeachersYear5;
        plan.TeachersYear6 = TeachersYear6;
        plan.TeachersYear7 = TeachersYear7;
        plan.TeachersYear8 = TeachersYear8;
        plan.TeachersYear9 = TeachersYear9;
        plan.TeachersYear10 = TeachersYear10;
        plan.TeachersYear11 = TeachersYear11;
        plan.TeachersYear12 = TeachersYear12;
        plan.TeachersYear13 = TeachersYear13;
    }
}