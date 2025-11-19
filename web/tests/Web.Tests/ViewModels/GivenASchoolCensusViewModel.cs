using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

[SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable")]
public class GivenASchoolCensusViewModel
{
    private readonly Fixture _fixture = new();
    private readonly School _school;

    public GivenASchoolCensusViewModel()
    {
        _school = _fixture.Create<School>();
    }

    public static TheoryData<Census?, decimal?> CensusInput =>
        new()
        {
            { null, null },
            { new Census(), null },
            {
                new Census
                {
                    TotalPupils = 123.45m
                },
                123.45m
            }
        };

    public static TheoryData<SchoolComparatorSet?, bool> ComparatorSetInput =>
        new()
        {
            { null, false },
            { new SchoolComparatorSet(), false },
            {
                new SchoolComparatorSet
                {
                    Building = [string.Empty],
                    Pupil = [string.Empty]
                },
                false
            },
            {
                new SchoolComparatorSet
                {
                    Building = ["building"]
                },
                false
            },
            {
                new SchoolComparatorSet
                {
                    Pupil = ["pupil"]
                },
                true
            }
        };

    public static TheoryData<KS4ProgressBandings?, KS4ProgressBanding[], bool> Ks4ProgressBandingsInput
    {
        get
        {
            var outOfRange = new KeyValuePair<string, string?>("000000", "Out of range");
            var wellBelowAverage = new KeyValuePair<string, string?>("000001", "Well below average");
            var belowAverage = new KeyValuePair<string, string?>("000002", "Below average");
            var average = new KeyValuePair<string, string?>("000003", "Average");
            var aboveAverage = new KeyValuePair<string, string?>("000004", "Above average");
            var wellAboveAverage = new KeyValuePair<string, string?>("000005", "Well above average");

            return new TheoryData<KS4ProgressBandings?, KS4ProgressBanding[], bool>
            {
                { null, [], false },
                { new KS4ProgressBandings([outOfRange]), [], false },
                { new KS4ProgressBandings([wellBelowAverage]), [], false },
                { new KS4ProgressBandings([belowAverage]), [], false },
                { new KS4ProgressBandings([average]), [], false },
                { new KS4ProgressBandings([aboveAverage, average]), [new KS4ProgressBanding(aboveAverage.Key, KS4ProgressBandings.Banding.AboveAverage)], true },
                { new KS4ProgressBandings([wellAboveAverage, average]), [new KS4ProgressBanding(wellAboveAverage.Key, KS4ProgressBandings.Banding.WellAboveAverage)], true },
                {
                    new KS4ProgressBandings([wellAboveAverage, aboveAverage, average]), [new KS4ProgressBanding(wellAboveAverage.Key, KS4ProgressBandings.Banding.WellAboveAverage), new KS4ProgressBanding(aboveAverage.Key, KS4ProgressBandings.Banding.AboveAverage)],
                    true
                }
            };
        }
    }

    [Fact]
    public void WhenContainsSchool()
    {
        var vm = new SchoolCensusViewModel(_school);

        Assert.Equal(_school.URN, vm.Urn);
        Assert.Equal(_school.SchoolName, vm.Name);
        Assert.Equal(_school.IsPartOfTrust, vm.IsPartOfTrust);
    }

    [Fact]
    public void WhenContainsUserDefinedSetId()
    {
        var userDefinedSetId = _fixture.Create<string>();

        var vm = new SchoolCensusViewModel(_school, userDefinedSetId);

        Assert.Equal(userDefinedSetId, vm.UserDefinedSetId);
    }

    [Fact]
    public void WhenContainsCustomDataId()
    {
        var customDataId = _fixture.Create<string>();

        var vm = new SchoolCensusViewModel(_school, null, customDataId);

        Assert.Equal(customDataId, vm.CustomDataId);
    }

    [Theory]
    [MemberData(nameof(CensusInput))]
    public void WhenContainsCensus(Census? census, decimal? expected)
    {
        var vm = new SchoolCensusViewModel(_school, null, null, census);

        Assert.Equal(expected, vm.TotalPupils);
    }

    [Theory]
    [MemberData(nameof(ComparatorSetInput))]
    public void WhenContainsDefaultComparatorSet(SchoolComparatorSet? defaultComparatorSet, bool expected)
    {
        var vm = new SchoolCensusViewModel(_school, null, null, null, defaultComparatorSet);

        Assert.Equal(expected, vm.HasDefaultComparatorSet);
    }

    [Theory]
    [MemberData(nameof(Ks4ProgressBandingsInput))]
    public void WhenContainsKs4ProgressBandings(KS4ProgressBandings? ks4ProgressBandings, KS4ProgressBanding[] expectedBandings, bool expectedProgressIndicators)
    {
        var vm = new SchoolCensusViewModel(_school, ks4ProgressBandings: ks4ProgressBandings);

        Assert.Equal(expectedBandings, vm.WellOrAboveAverageKS4ProgressBandingsInComparatorSet);
        Assert.Equal(expectedProgressIndicators, vm.HasProgressIndicators);
    }
}