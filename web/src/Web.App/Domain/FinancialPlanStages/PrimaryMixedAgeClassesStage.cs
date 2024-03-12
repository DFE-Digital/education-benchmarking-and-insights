namespace Web.App.Domain.FinancialPlanStages
{
    public class PrimaryMixedAgeClassesStage : Stage
    {
        public bool MixedAgeReceptionYear1 { get; set; }
        public bool MixedAgeYear1Year2 { get; set; }
        public bool MixedAgeYear2Year3 { get; set; }
        public bool MixedAgeYear3Year4 { get; set; }
        public bool MixedAgeYear4Year5 { get; set; }
        public bool MixedAgeYear5Year6 { get; set; }

        public override void SetPlanValues(FinancialPlan plan)
        {
            plan.MixedAgeReceptionYear1 = MixedAgeReceptionYear1;
            plan.MixedAgeYear1Year2 = MixedAgeYear1Year2;
            plan.MixedAgeYear2Year3 = MixedAgeYear2Year3;
            plan.MixedAgeYear3Year4 = MixedAgeYear3Year4;
            plan.MixedAgeYear4Year5 = MixedAgeYear4Year5;
            plan.MixedAgeYear5Year6 = MixedAgeYear5Year6;

            SetPupilFigures(plan);
            SetTeacherFigures(plan);
            //TODO : Set teaching assistant figures
        }

        private void SetTeacherFigures(FinancialPlan plan)
        {
            plan.TeachersMixedReceptionYear1 = MixedAgeReceptionYear1 ? plan.TeachersMixedReceptionYear1 : null;
            plan.TeachersMixedYear1Year2 = MixedAgeYear1Year2 ? plan.TeachersMixedYear1Year2 : null;
            plan.TeachersMixedYear2Year3 = MixedAgeYear2Year3 ? plan.TeachersMixedYear2Year3 : null;
            plan.TeachersMixedYear3Year4 = MixedAgeYear3Year4 ? plan.TeachersMixedYear3Year4 : null;
            plan.TeachersMixedYear4Year5 = MixedAgeYear4Year5 ? plan.TeachersMixedYear4Year5 : null;
            plan.TeachersMixedYear5Year6 = MixedAgeYear5Year6 ? plan.TeachersMixedYear5Year6 : null;

            plan.TeachersReception = MixedAgeReceptionYear1 ? null : plan.TeachersReception;
            plan.TeachersYear1 = MixedAgeReceptionYear1 || MixedAgeYear1Year2 ? null : plan.TeachersYear1;
            plan.TeachersYear2 = MixedAgeYear1Year2 || MixedAgeYear2Year3 ? null : plan.TeachersYear2;
            plan.TeachersYear3 = MixedAgeYear2Year3 || MixedAgeYear3Year4 ? null : plan.TeachersYear3;
            plan.TeachersYear4 = MixedAgeYear3Year4 || MixedAgeYear4Year5 ? null : plan.TeachersYear4;
            plan.TeachersYear5 = MixedAgeYear4Year5 || MixedAgeYear5Year6 ? null : plan.TeachersYear5;
            plan.TeachersYear6 = MixedAgeYear5Year6 ? null : plan.TeachersYear6;
        }

        private void SetPupilFigures(FinancialPlan plan)
        {
            plan.PupilsMixedReceptionYear1 = MixedAgeReceptionYear1 ? plan.PupilsMixedReceptionYear1 : null;
            plan.PupilsMixedYear1Year2 = MixedAgeYear1Year2 ? plan.PupilsMixedYear1Year2 : null;
            plan.PupilsMixedYear2Year3 = MixedAgeYear2Year3 ? plan.PupilsMixedYear2Year3 : null;
            plan.PupilsMixedYear3Year4 = MixedAgeYear3Year4 ? plan.PupilsMixedYear3Year4 : null;
            plan.PupilsMixedYear4Year5 = MixedAgeYear4Year5 ? plan.PupilsMixedYear4Year5 : null;
            plan.PupilsMixedYear5Year6 = MixedAgeYear5Year6 ? plan.PupilsMixedYear5Year6 : null;

            plan.PupilsReception = MixedAgeReceptionYear1 ? null : plan.PupilsReception;
            plan.PupilsYear1 = MixedAgeReceptionYear1 || MixedAgeYear1Year2 ? null : plan.PupilsYear1;
            plan.PupilsYear2 = MixedAgeYear1Year2 || MixedAgeYear2Year3 ? null : plan.PupilsYear2;
            plan.PupilsYear3 = MixedAgeYear2Year3 || MixedAgeYear3Year4 ? null : plan.PupilsYear3;
            plan.PupilsYear4 = MixedAgeYear3Year4 || MixedAgeYear4Year5 ? null : plan.PupilsYear4;
            plan.PupilsYear5 = MixedAgeYear4Year5 || MixedAgeYear5Year6 ? null : plan.PupilsYear5;
            plan.PupilsYear6 = MixedAgeYear5Year6 ? null : plan.PupilsYear6;
        }
    }
}