namespace Web.App.Domain.FinancialPlanStages
{
    public class TeachingAssistantFiguresStage : Stage
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
        public decimal? AssistantsNursery { get; set; }
        public decimal? AssistantsMixedReceptionYear1 { get; set; }
        public decimal? AssistantsMixedYear1Year2 { get; set; }
        public decimal? AssistantsMixedYear2Year3 { get; set; }
        public decimal? AssistantsMixedYear3Year4 { get; set; }
        public decimal? AssistantsMixedYear4Year5 { get; set; }
        public decimal? AssistantsMixedYear5Year6 { get; set; }
        public decimal? AssistantsReception { get; set; }
        public decimal? AssistantsYear1 { get; set; }
        public decimal? AssistantsYear2 { get; set; }
        public decimal? AssistantsYear3 { get; set; }
        public decimal? AssistantsYear4 { get; set; }
        public decimal? AssistantsYear5 { get; set; }
        public decimal? AssistantsYear6 { get; set; }

        public override void SetPlanValues(FinancialPlan plan)
        {
            plan.AssistantsMixedReceptionYear1 = AssistantsMixedReceptionYear1;
            plan.AssistantsMixedYear1Year2 = AssistantsMixedYear1Year2;
            plan.AssistantsMixedYear2Year3 = AssistantsMixedYear2Year3;
            plan.AssistantsMixedYear3Year4 = AssistantsMixedYear3Year4;
            plan.AssistantsMixedYear4Year5 = AssistantsMixedYear4Year5;
            plan.AssistantsMixedYear5Year6 = AssistantsMixedYear5Year6;
            plan.AssistantsNursery = AssistantsNursery;
            plan.AssistantsReception = AssistantsReception;
            plan.AssistantsYear1 = AssistantsYear1;
            plan.AssistantsYear2 = AssistantsYear2;
            plan.AssistantsYear3 = AssistantsYear3;
            plan.AssistantsYear4 = AssistantsYear4;
            plan.AssistantsYear5 = AssistantsYear5;
            plan.AssistantsYear6 = AssistantsYear6;
        }
    }
}