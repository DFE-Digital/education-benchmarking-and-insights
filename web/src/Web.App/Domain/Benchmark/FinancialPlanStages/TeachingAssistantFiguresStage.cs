namespace Web.App.Domain;

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

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.AssistantsMixedReceptionYear1 = AssistantsMixedReceptionYear1;
        planInput.AssistantsMixedYear1Year2 = AssistantsMixedYear1Year2;
        planInput.AssistantsMixedYear2Year3 = AssistantsMixedYear2Year3;
        planInput.AssistantsMixedYear3Year4 = AssistantsMixedYear3Year4;
        planInput.AssistantsMixedYear4Year5 = AssistantsMixedYear4Year5;
        planInput.AssistantsMixedYear5Year6 = AssistantsMixedYear5Year6;
        planInput.AssistantsNursery = AssistantsNursery;
        planInput.AssistantsReception = AssistantsReception;
        planInput.AssistantsYear1 = AssistantsYear1;
        planInput.AssistantsYear2 = AssistantsYear2;
        planInput.AssistantsYear3 = AssistantsYear3;
        planInput.AssistantsYear4 = AssistantsYear4;
        planInput.AssistantsYear5 = AssistantsYear5;
        planInput.AssistantsYear6 = AssistantsYear6;
    }
}