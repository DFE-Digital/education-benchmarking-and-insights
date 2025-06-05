using System.Diagnostics.CodeAnalysis;
using Web.App.Domain;

namespace Web.App.ViewModels;

[ExcludeFromCodeCoverage]
public class ComparatorCharacteristicsCommentaryViewModel(string? overallPhase, bool? showTitle = false)
{
    public bool HasSendCharacteristics => OverallPhaseTypes.SendCharacteristicsPhases.Contains(overallPhase);
    public bool ShowTitle => showTitle ?? false;
}