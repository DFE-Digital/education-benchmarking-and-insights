using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenASchoolComparisonViewModel
{
    private readonly CostCodes _costCodes;
    private readonly Fixture _fixture = new();
    private readonly School _school;

    public GivenASchoolComparisonViewModel()
    {
        _school = _fixture.Create<School>();
        _costCodes = new CostCodes(false, false);
    }

    public static TheoryData<SchoolExpenditure?, int?> ExpenditureInput =>
        new()
        {
            { null, null },
            { new SchoolExpenditure(), null },
            {
                new SchoolExpenditure
                {
                    PeriodCoveredByReturn = 12
                },
                12
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
                true
            },
            {
                new SchoolComparatorSet
                {
                    Pupil = ["pupil"]
                },
                true
            }
        };

    [Fact]
    public void WhenContainsSchool()
    {
        var vm = new SchoolComparisonViewModel(_school, _costCodes);

        Assert.Equal(_school.URN, vm.Urn);
        Assert.Equal(_school.SchoolName, vm.Name);
        Assert.Equal(_school.IsPartOfTrust, vm.IsPartOfTrust);
    }

    [Fact]
    public void WhenContainsUserDefinedSetId()
    {
        var userDefinedSetId = _fixture.Create<string>();

        var vm = new SchoolComparisonViewModel(_school, _costCodes, userDefinedSetId);

        Assert.Equal(userDefinedSetId, vm.UserDefinedSetId);
    }

    [Fact]
    public void WhenContainsCustomDataId()
    {
        var customDataId = _fixture.Create<string>();

        var vm = new SchoolComparisonViewModel(_school, _costCodes, null, customDataId);

        Assert.Equal(customDataId, vm.CustomDataId);
    }

    [Theory]
    [MemberData(nameof(ExpenditureInput))]
    public void WhenContainsExpenditure(SchoolExpenditure? expenditure, int? expected)
    {
        var vm = new SchoolComparisonViewModel(_school, _costCodes, null, null, expenditure);

        Assert.Equal(expected, vm.PeriodCoveredByReturn);
    }

    [Theory]
    [MemberData(nameof(ComparatorSetInput))]
    public void WhenContainsDefaultComparatorSet(SchoolComparatorSet? defaultComparatorSet, bool expected)
    {
        var vm = new SchoolComparisonViewModel(_school, _costCodes, null, null, null, defaultComparatorSet);

        Assert.Equal(expected, vm.HasDefaultComparatorSet);
    }
}