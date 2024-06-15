namespace Web.App.Domain;

public class PrimaryMixedAgeClassesStage : Stage
{
    public bool MixedAgeReceptionYear1 { get; set; }
    public bool MixedAgeYear1Year2 { get; set; }
    public bool MixedAgeYear2Year3 { get; set; }
    public bool MixedAgeYear3Year4 { get; set; }
    public bool MixedAgeYear4Year5 { get; set; }
    public bool MixedAgeYear5Year6 { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.MixedAgeReceptionYear1 = MixedAgeReceptionYear1;
        planInput.MixedAgeYear1Year2 = MixedAgeYear1Year2;
        planInput.MixedAgeYear2Year3 = MixedAgeYear2Year3;
        planInput.MixedAgeYear3Year4 = MixedAgeYear3Year4;
        planInput.MixedAgeYear4Year5 = MixedAgeYear4Year5;
        planInput.MixedAgeYear5Year6 = MixedAgeYear5Year6;

        SetPupilFigures(planInput);
        SetTeacherFigures(planInput);
        //TODO : Set teaching assistant figures
    }

    private void SetTeacherFigures(FinancialPlanInput planInput)
    {
        planInput.TeachersMixedReceptionYear1 = MixedAgeReceptionYear1 ? planInput.TeachersMixedReceptionYear1 : null;
        planInput.TeachersMixedYear1Year2 = MixedAgeYear1Year2 ? planInput.TeachersMixedYear1Year2 : null;
        planInput.TeachersMixedYear2Year3 = MixedAgeYear2Year3 ? planInput.TeachersMixedYear2Year3 : null;
        planInput.TeachersMixedYear3Year4 = MixedAgeYear3Year4 ? planInput.TeachersMixedYear3Year4 : null;
        planInput.TeachersMixedYear4Year5 = MixedAgeYear4Year5 ? planInput.TeachersMixedYear4Year5 : null;
        planInput.TeachersMixedYear5Year6 = MixedAgeYear5Year6 ? planInput.TeachersMixedYear5Year6 : null;

        planInput.TeachersReception = MixedAgeReceptionYear1 ? null : planInput.TeachersReception;
        planInput.TeachersYear1 = MixedAgeReceptionYear1 || MixedAgeYear1Year2 ? null : planInput.TeachersYear1;
        planInput.TeachersYear2 = MixedAgeYear1Year2 || MixedAgeYear2Year3 ? null : planInput.TeachersYear2;
        planInput.TeachersYear3 = MixedAgeYear2Year3 || MixedAgeYear3Year4 ? null : planInput.TeachersYear3;
        planInput.TeachersYear4 = MixedAgeYear3Year4 || MixedAgeYear4Year5 ? null : planInput.TeachersYear4;
        planInput.TeachersYear5 = MixedAgeYear4Year5 || MixedAgeYear5Year6 ? null : planInput.TeachersYear5;
        planInput.TeachersYear6 = MixedAgeYear5Year6 ? null : planInput.TeachersYear6;
    }

    private void SetPupilFigures(FinancialPlanInput planInput)
    {
        planInput.PupilsMixedReceptionYear1 = MixedAgeReceptionYear1 ? planInput.PupilsMixedReceptionYear1 : null;
        planInput.PupilsMixedYear1Year2 = MixedAgeYear1Year2 ? planInput.PupilsMixedYear1Year2 : null;
        planInput.PupilsMixedYear2Year3 = MixedAgeYear2Year3 ? planInput.PupilsMixedYear2Year3 : null;
        planInput.PupilsMixedYear3Year4 = MixedAgeYear3Year4 ? planInput.PupilsMixedYear3Year4 : null;
        planInput.PupilsMixedYear4Year5 = MixedAgeYear4Year5 ? planInput.PupilsMixedYear4Year5 : null;
        planInput.PupilsMixedYear5Year6 = MixedAgeYear5Year6 ? planInput.PupilsMixedYear5Year6 : null;

        planInput.PupilsReception = MixedAgeReceptionYear1 ? null : planInput.PupilsReception;
        planInput.PupilsYear1 = MixedAgeReceptionYear1 || MixedAgeYear1Year2 ? null : planInput.PupilsYear1;
        planInput.PupilsYear2 = MixedAgeYear1Year2 || MixedAgeYear2Year3 ? null : planInput.PupilsYear2;
        planInput.PupilsYear3 = MixedAgeYear2Year3 || MixedAgeYear3Year4 ? null : planInput.PupilsYear3;
        planInput.PupilsYear4 = MixedAgeYear3Year4 || MixedAgeYear4Year5 ? null : planInput.PupilsYear4;
        planInput.PupilsYear5 = MixedAgeYear4Year5 || MixedAgeYear5Year6 ? null : planInput.PupilsYear5;
        planInput.PupilsYear6 = MixedAgeYear5Year6 ? null : planInput.PupilsYear6;
    }
}