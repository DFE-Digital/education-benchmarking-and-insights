using Web.App.Extensions;

namespace Web.App.Domain.FinancialPlanStages
{
    public class PupilFiguresStage : Stage
    {
        public string? PupilsYear7 { get; set; }
        public string? PupilsYear8 { get; set; }
        public string? PupilsYear9 { get; set; }
        public string? PupilsYear10 { get; set; }
        public string? PupilsYear11 { get; set; }
        public decimal? PupilsYear12 { get; set; }
        public decimal? PupilsYear13 { get; set; }

        public override void SetPlanValues(FinancialPlan plan)
        {
            plan.PupilsYear7 = PupilsYear7;
            plan.PupilsYear8 = PupilsYear8;
            plan.PupilsYear9 = PupilsYear9;
            plan.PupilsYear10 = PupilsYear10;
            plan.PupilsYear11 = PupilsYear11;
            plan.PupilsYear12 = PupilsYear12;
            plan.PupilsYear13 = PupilsYear13;

            plan.TeachersYear7 = PupilsYear7.ToInt() > 0 ? plan.TeachersYear7 : null;
            plan.TeachersYear8 = PupilsYear8.ToInt() > 0 ? plan.TeachersYear8 : null;
            plan.TeachersYear9 = PupilsYear9.ToInt() > 0 ? plan.TeachersYear9 : null;
            plan.TeachersYear10 = PupilsYear10.ToInt() > 0 ? plan.TeachersYear10 : null;
            plan.TeachersYear11 = PupilsYear11.ToInt() > 0 ? plan.TeachersYear11 : null;
            plan.TeachersYear12 = PupilsYear12 > 0 ? plan.TeachersYear12 : null;
            plan.TeachersYear13 = PupilsYear13 > 0 ? plan.TeachersYear13 : null;
        }
    }
}