namespace Web.App.ViewModels.Enhancements;

public record SuggesterViewModel
{
    public string? InputElementId { get; init; }
    public string? SelectedEstablishmentField { get; init; }
    public string? Type { get; init; }
    public string? IdField { get; init; }
}