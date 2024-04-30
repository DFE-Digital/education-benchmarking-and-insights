namespace Web.App.Domain.FinancialPlanStages;

public class PrimaryHasMixedAgeClassesStage : Stage
{
    public bool? HasMixedAgeClasses { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.HasMixedAgeClasses = HasMixedAgeClasses;

        if (HasMixedAgeClasses is false)
        {
            planInput.MixedAgeReceptionYear1 = false;
            planInput.MixedAgeYear1Year2 = false;
            planInput.MixedAgeYear2Year3 = false;
            planInput.MixedAgeYear3Year4 = false;
            planInput.MixedAgeYear4Year5 = false;
            planInput.MixedAgeYear5Year6 = false;

            planInput.PupilsMixedReceptionYear1 = null;
            planInput.PupilsMixedYear1Year2 = null;
            planInput.PupilsMixedYear2Year3 = null;
            planInput.PupilsMixedYear3Year4 = null;
            planInput.PupilsMixedYear4Year5 = null;
            planInput.PupilsMixedYear5Year6 = null;

            planInput.TeachersMixedReceptionYear1 = null;
            planInput.TeachersMixedYear1Year2 = null;
            planInput.TeachersMixedYear2Year3 = null;
            planInput.TeachersMixedYear3Year4 = null;
            planInput.TeachersMixedYear4Year5 = null;
            planInput.TeachersMixedYear5Year6 = null;

            //TODO : Set teaching assistant figures 
        }
    }
}