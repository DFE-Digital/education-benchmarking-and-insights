using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenAnOverallPhaseTypes
{
    [Fact]
    public void ShouldReturnExpectedAllPhaseTypes()
    {
        string[] expected =
        [
            OverallPhaseTypes.Primary,
            OverallPhaseTypes.Secondary,
            OverallPhaseTypes.Special,
            OverallPhaseTypes.PupilReferralUnit,
            OverallPhaseTypes.AllThrough,
            OverallPhaseTypes.Nursery,
            OverallPhaseTypes.PostSixteen,
            OverallPhaseTypes.AlternativeProvision,
            OverallPhaseTypes.UniversityTechnicalCollege
        ];

        var actual = OverallPhaseTypes.All;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldReturnExpectedAcademyPhaseTypes()
    {
        string[] expected =
        [
            OverallPhaseTypes.Primary,
            OverallPhaseTypes.Secondary,
            OverallPhaseTypes.Special,
            OverallPhaseTypes.AllThrough,
            OverallPhaseTypes.PostSixteen,
            OverallPhaseTypes.AlternativeProvision,
            OverallPhaseTypes.UniversityTechnicalCollege
        ];

        var actual = OverallPhaseTypes.AcademyPhases;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldReturnExpectedAcademySendCharacteristicsPhaseTypes()
    {
        string[] expected =
        [
            OverallPhaseTypes.Special,
            OverallPhaseTypes.PupilReferralUnit,
            OverallPhaseTypes.AlternativeProvision
        ];

        var actual = OverallPhaseTypes.SendCharacteristicsPhases;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldReturnExpectedAllPhaseTypeFilters()
    {
        OverallPhaseTypes.OverallPhaseTypeFilter[] expected =
        [
            OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
            OverallPhaseTypes.OverallPhaseTypeFilter.Secondary,
            OverallPhaseTypes.OverallPhaseTypeFilter.Special,
            OverallPhaseTypes.OverallPhaseTypeFilter.PupilReferralUnit,
            OverallPhaseTypes.OverallPhaseTypeFilter.AllThrough,
            OverallPhaseTypes.OverallPhaseTypeFilter.Nursery,
            OverallPhaseTypes.OverallPhaseTypeFilter.PostSixteen,
            OverallPhaseTypes.OverallPhaseTypeFilter.AlternativeProvision,
            OverallPhaseTypes.OverallPhaseTypeFilter.UniversityTechnicalCollege
        ];

        var actual = OverallPhaseTypes.AllFilters;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.Primary, OverallPhaseTypes.Primary)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.Secondary, OverallPhaseTypes.Secondary)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.Special, OverallPhaseTypes.Special)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.PupilReferralUnit, OverallPhaseTypes.PupilReferralUnit)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.AllThrough, OverallPhaseTypes.AllThrough)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.Nursery, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.PostSixteen, OverallPhaseTypes.PostSixteen)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.AlternativeProvision, OverallPhaseTypes.AlternativeProvision)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.UniversityTechnicalCollege, OverallPhaseTypes.UniversityTechnicalCollege)]
    public void ShouldReturnExpectedFilterDescriptions(OverallPhaseTypes.OverallPhaseTypeFilter filter, string expected)
    {
        var actual = filter.GetFilterDescription();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldThrowExceptionForOutOfRangeFilterDescription()
    {
        var exception = Assert.Throws<ArgumentException>(() => ((OverallPhaseTypes.OverallPhaseTypeFilter)999).GetFilterDescription());

        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.Primary, OverallPhaseTypes.Primary)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.Secondary, OverallPhaseTypes.Secondary)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.Special, OverallPhaseTypes.Special)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.PupilReferralUnit, OverallPhaseTypes.PupilReferralUnit)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.AllThrough, OverallPhaseTypes.AllThrough)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.Nursery, OverallPhaseTypes.Nursery)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.PostSixteen, OverallPhaseTypes.PostSixteen)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.AlternativeProvision, OverallPhaseTypes.AlternativeProvision)]
    [InlineData(OverallPhaseTypes.OverallPhaseTypeFilter.UniversityTechnicalCollege, OverallPhaseTypes.UniversityTechnicalCollege)]
    public void ShouldReturnExpectedQueryParam(OverallPhaseTypes.OverallPhaseTypeFilter filter, string expected)
    {
        var actual = filter.GetQueryParam();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldThrowExceptionForOutOfRangeQueryParam()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ((OverallPhaseTypes.OverallPhaseTypeFilter)999).GetQueryParam());

        Assert.NotNull(exception);
    }
}