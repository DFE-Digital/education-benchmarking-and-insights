using Web.App.Extensions;

namespace Web.App.Domain;

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

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.PupilsNursery = PupilsNursery;
        planInput.PupilsMixedReceptionYear1 = PupilsMixedReceptionYear1;
        planInput.PupilsMixedYear1Year2 = PupilsMixedYear1Year2;
        planInput.PupilsMixedYear2Year3 = PupilsMixedYear2Year3;
        planInput.PupilsMixedYear3Year4 = PupilsMixedYear3Year4;
        planInput.PupilsMixedYear4Year5 = PupilsMixedYear4Year5;
        planInput.PupilsMixedYear5Year6 = PupilsMixedYear5Year6;
        planInput.PupilsReception = PupilsReception;
        planInput.PupilsYear1 = PupilsYear1;
        planInput.PupilsYear2 = PupilsYear2;
        planInput.PupilsYear3 = PupilsYear3;
        planInput.PupilsYear4 = PupilsYear4;
        planInput.PupilsYear5 = PupilsYear5;
        planInput.PupilsYear6 = PupilsYear6;

        ResetTeacherFigures(planInput);
        //TODO : Set teaching assistant figures
    }

    private void ResetTeacherFigures(FinancialPlanInput planInput)
    {
        planInput.TeachersNursery = PupilsNursery > 0 ? planInput.TeachersNursery : null;
        planInput.TeachersMixedReceptionYear1 = PupilsMixedReceptionYear1.ToInt() > 0 ? planInput.TeachersMixedReceptionYear1 : null;
        planInput.TeachersMixedYear1Year2 = PupilsMixedYear1Year2.ToInt() > 0 ? planInput.TeachersMixedYear1Year2 : null;
        planInput.TeachersMixedYear2Year3 = PupilsMixedYear2Year3.ToInt() > 0 ? planInput.TeachersMixedYear2Year3 : null;
        planInput.TeachersMixedYear3Year4 = PupilsMixedYear3Year4.ToInt() > 0 ? planInput.TeachersMixedYear3Year4 : null;
        planInput.TeachersMixedYear4Year5 = PupilsMixedYear4Year5.ToInt() > 0 ? planInput.TeachersMixedYear4Year5 : null;
        planInput.TeachersMixedYear5Year6 = PupilsMixedYear5Year6.ToInt() > 0 ? planInput.TeachersMixedYear5Year6 : null;
        planInput.TeachersReception = PupilsReception.ToInt() > 0 ? planInput.TeachersReception : null;
        planInput.TeachersYear1 = PupilsYear1.ToInt() > 0 ? planInput.TeachersYear1 : null;
        planInput.TeachersYear2 = PupilsYear2.ToInt() > 0 ? planInput.TeachersYear2 : null;
        planInput.TeachersYear3 = PupilsYear3.ToInt() > 0 ? planInput.TeachersYear3 : null;
        planInput.TeachersYear4 = PupilsYear4.ToInt() > 0 ? planInput.TeachersYear4 : null;
        planInput.TeachersYear5 = PupilsYear5.ToInt() > 0 ? planInput.TeachersYear5 : null;
        planInput.TeachersYear6 = PupilsYear6.ToInt() > 0 ? planInput.TeachersYear6 : null;
    }
}