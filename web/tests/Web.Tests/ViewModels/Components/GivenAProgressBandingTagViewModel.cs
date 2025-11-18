using System.Text;
using Web.App.Domain;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewModels.Components;

public class GivenAProgressBandingTagViewModel
{
    [Theory]
    [InlineData(KS4ProgressBandings.Banding.WellBelowAverage)]
    [InlineData(KS4ProgressBandings.Banding.BelowAverage)]
    [InlineData(KS4ProgressBandings.Banding.Average)]
    [InlineData(KS4ProgressBandings.Banding.AboveAverage)]
    [InlineData(KS4ProgressBandings.Banding.WellAboveAverage)]
    public void ShouldReturnExpectedForBanding(KS4ProgressBandings.Banding banding)
    {
        var model = new ProgressBandingTagViewModel(banding);

        Assert.Equal(banding.ToGdsColour(), model.Colour);
        Assert.Equal(banding.ToStringValue(), model.Label);
    }
}