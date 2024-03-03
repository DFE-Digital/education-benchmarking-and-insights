namespace EducationBenchmarking.Web.Domain.FinancialPlanStages;

public class PrimaryHasMixedAgeClassesStage : Stage
{
    public bool? HasMixedAgeClasses { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.HasMixedAgeClasses = HasMixedAgeClasses;

        if (HasMixedAgeClasses is false)
        {
            plan.MixedAgeReceptionYear1 = false;
            plan.MixedAgeYear1Year2 = false;
            plan.MixedAgeYear2Year3 = false;
            plan.MixedAgeYear3Year4 = false;
            plan.MixedAgeYear4Year5 = false;
            plan.MixedAgeYear5Year6 = false;

            plan.PupilsMixedReceptionYear1 = null;
            plan.PupilsMixedYear1Year2 = null;
            plan.PupilsMixedYear2Year3 = null;
            plan.PupilsMixedYear3Year4 = null;
            plan.PupilsMixedYear4Year5 = null;
            plan.PupilsMixedYear5Year6 = null;

            plan.TeachersMixedReceptionYear1 = null;
            plan.TeachersMixedYear1Year2 = null;
            plan.TeachersMixedYear2Year3 = null;
            plan.TeachersMixedYear3Year4 = null;
            plan.TeachersMixedYear4Year5 = null;
            plan.TeachersMixedYear5Year6 = null;

            //TODO : Set teaching assistant figures 
        }
    }
}