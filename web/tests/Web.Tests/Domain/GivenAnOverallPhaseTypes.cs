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
}