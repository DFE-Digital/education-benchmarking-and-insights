using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class ProgressBandingTagViewModel(KS4ProgressBandings.Banding banding)
{
    public string Colour => banding.ToGdsColour();
    public string Label => banding.ToStringValue();
}