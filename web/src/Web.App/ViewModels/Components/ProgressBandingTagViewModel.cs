using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class ProgressBandingTagViewModel(KS4ProgressBandings.Banding banding)
{
    public string? Colour =>
        banding switch
        {
            KS4ProgressBandings.Banding.WellBelowAverage => "red",
            KS4ProgressBandings.Banding.BelowAverage => "orange",
            KS4ProgressBandings.Banding.Average => "yellow",
            KS4ProgressBandings.Banding.AboveAverage => "blue",
            KS4ProgressBandings.Banding.WellAboveAverage => "turquoise",
            _ => null
        };

    public string Label => banding.ToStringValue();
}