using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenAKS4ProgressBandings
{
    public static TheoryData<IEnumerable<KeyValuePair<string, string?>>, KS4ProgressBanding[]> Ks4ProgressBandingsInput
    {
        get
        {
            var outOfRange = new KeyValuePair<string, string?>("000000", "Out of range");
            var wellBelowAverage = new KeyValuePair<string, string?>("000001", "Well below average");
            var belowAverage = new KeyValuePair<string, string?>("000002", "Below average");
            var average = new KeyValuePair<string, string?>("000003", "Average");
            var aboveAverage = new KeyValuePair<string, string?>("000004", "Above average");
            var wellAboveAverage = new KeyValuePair<string, string?>("000005", "Well above average");

            return new TheoryData<IEnumerable<KeyValuePair<string, string?>>, KS4ProgressBanding[]>
            {
                { [], [] },
                { [outOfRange], [] },
                { [wellBelowAverage], [new KS4ProgressBanding(wellBelowAverage.Key, KS4ProgressBandings.Banding.WellBelowAverage)] },
                { [belowAverage], [new KS4ProgressBanding(belowAverage.Key, KS4ProgressBandings.Banding.BelowAverage)] },
                { [average], [new KS4ProgressBanding(average.Key, KS4ProgressBandings.Banding.Average)] },
                { [aboveAverage, average], [new KS4ProgressBanding(aboveAverage.Key, KS4ProgressBandings.Banding.AboveAverage), new KS4ProgressBanding(average.Key, KS4ProgressBandings.Banding.Average)] },
                { [wellAboveAverage, average], [new KS4ProgressBanding(wellAboveAverage.Key, KS4ProgressBandings.Banding.WellAboveAverage), new KS4ProgressBanding(average.Key, KS4ProgressBandings.Banding.Average)] },
                { [wellAboveAverage, aboveAverage, average], [new KS4ProgressBanding(wellAboveAverage.Key, KS4ProgressBandings.Banding.WellAboveAverage), new KS4ProgressBanding(aboveAverage.Key, KS4ProgressBandings.Banding.AboveAverage), new KS4ProgressBanding(average.Key, KS4ProgressBandings.Banding.Average)] },
                { [wellBelowAverage, new KeyValuePair<string, string?>(wellBelowAverage.Key, average.Value)], [new KS4ProgressBanding(wellBelowAverage.Key, KS4ProgressBandings.Banding.Average)] },
            };
        }
    }

    [Theory]
    [MemberData(nameof(Ks4ProgressBandingsInput))]
    public void WhenBandings(IEnumerable<KeyValuePair<string, string?>> bandings, KS4ProgressBanding[] expected)
    {
        var result = new KS4ProgressBandings(bandings);
        Assert.Equal(expected, result.Items);
    }
}

public class GivenAKS4ProgressBandingToStringValue
{
    [Theory]
    [InlineData("Out of range", null)]
    [InlineData("Well below average", KS4ProgressBandings.Banding.WellBelowAverage)]
    [InlineData("Below average", KS4ProgressBandings.Banding.BelowAverage)]
    [InlineData("Average", KS4ProgressBandings.Banding.Average)]
    [InlineData("Above average", KS4ProgressBandings.Banding.AboveAverage)]
    [InlineData("Well above average", KS4ProgressBandings.Banding.WellAboveAverage)]
    public void WhenBandingStringIs(string banding, KS4ProgressBandings.Banding? expected)
    {
        var result = banding.ToBanding();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(KS4ProgressBandings.Banding.WellBelowAverage, "Well below average")]
    [InlineData(KS4ProgressBandings.Banding.BelowAverage, "Below average")]
    [InlineData(KS4ProgressBandings.Banding.Average, "Average")]
    [InlineData(KS4ProgressBandings.Banding.AboveAverage, "Above average")]
    [InlineData(KS4ProgressBandings.Banding.WellAboveAverage, "Well above average")]
    [InlineData((KS4ProgressBandings.Banding)999, null)]
    public void WhenBandingIs(KS4ProgressBandings.Banding banding, string? expected)
    {
        var result = banding.ToStringValue();
        Assert.Equal(expected, result);
    }
}

public class GivenAKS4ProgressBandingToGdsColour
{
    [Theory]
    [InlineData(KS4ProgressBandings.Banding.WellBelowAverage, "red")]
    [InlineData(KS4ProgressBandings.Banding.BelowAverage, "orange")]
    [InlineData(KS4ProgressBandings.Banding.Average, "yellow")]
    [InlineData(KS4ProgressBandings.Banding.AboveAverage, "blue")]
    [InlineData(KS4ProgressBandings.Banding.WellAboveAverage, "turquoise")]
    [InlineData((KS4ProgressBandings.Banding)999, "grey")]
    public void WhenBandingIs(KS4ProgressBandings.Banding banding, string expected)
    {
        var result = banding.ToGdsColour();
        Assert.Equal(expected, result);
    }
}