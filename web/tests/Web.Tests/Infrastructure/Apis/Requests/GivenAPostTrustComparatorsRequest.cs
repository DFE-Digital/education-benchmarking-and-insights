using AutoFixture;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.Infrastructure.Apis.Requests;

public class GivenAPostTrustComparatorsRequest
{
    private readonly string _companyName;
    private readonly Fixture _fixture;

    public GivenAPostTrustComparatorsRequest()
    {
        _fixture = new Fixture();
        _companyName = _fixture.Create<string>();
    }

    [Fact]
    public void MapsTarget()
    {
        // arrange
        var viewModel = _fixture.Create<UserDefinedTrustCharacteristicViewModel>();

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).Target;

        // assert
        Assert.Equal(_companyName, actual);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsTotalPupils(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedTrustCharacteristicViewModel
        {
            TotalPupils = selected,
            TotalPupilsFrom = from,
            TotalPupilsTo = to
        };

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).TotalPupils;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsSchoolsInTrust(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedTrustCharacteristicViewModel
        {
            SchoolsInTrust = selected,
            SchoolsInTrustFrom = from,
            SchoolsInTrustTo = to
        };

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).SchoolsInTrust;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsTotalIncome(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedTrustCharacteristicViewModel
        {
            TotalIncome = selected,
            TotalIncomeFrom = from,
            TotalIncomeTo = to
        };

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).TotalIncome;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsTotalInternalFloorArea(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedTrustCharacteristicViewModel
        {
            InternalFloorArea = selected,
            InternalFloorAreaFrom = from,
            InternalFloorAreaTo = to
        };

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).TotalInternalFloorArea;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(new[]
    {
        "Nursery"
    }, new[]
    {
        "Nursery"
    })]
    [InlineData(new[]
    {
        "Primary"
    }, new[]
    {
        "Primary"
    })]
    [InlineData(new[]
    {
        "Secondary"
    }, new[]
    {
        "Secondary"
    })]
    [InlineData(new[]
    {
        "Pupil referral unit"
    }, new[]
    {
        "Pupil referral unit"
    })]
    [InlineData(new[]
    {
        "Special"
    }, new[]
    {
        "Special"
    })]
    [InlineData(new[]
    {
        "All-through"
    }, new[]
    {
        "All-through"
    })]
    [InlineData(new[]
    {
        "Nursery",
        "Primary",
        "Secondary"
    }, new[]
    {
        "Nursery",
        "Primary",
        "Secondary"
    })]
    public void MapsOverallPhase(string?[]? overallPhases, string[]? expected)
    {
        // arrange
        var viewModel = new UserDefinedTrustCharacteristicViewModel
        {
            OverallPhases = overallPhases
        };

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).PhasesCovered;

        // assert
        Assert.Equal(expected, actual?.Values);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentFreeSchoolMeals(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedTrustCharacteristicViewModel
        {
            FreeSchoolMeals = selected,
            FreeSchoolMealsFrom = from,
            FreeSchoolMealsTo = to
        };

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).PercentFreeSchoolMeals;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 123, 456, 123, 456)]
    public void MapsPercentSpecialEducationNeeds(string? selected, int? from, int? to, int? expectedFrom, int? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedTrustCharacteristicViewModel
        {
            SpecialEducationalNeeds = selected,
            SpecialEducationalNeedsFrom = from,
            SpecialEducationalNeedsTo = to
        };

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).PercentSpecialEducationNeeds;

        // assert
        Assert.Equal(expectedFrom, actual?.From);
        Assert.Equal(expectedTo, actual?.To);
    }

    [Theory]
    [InlineData("false", null, null, null, null)]
    [InlineData("true", 1900, 2100, "1900-01-01", "2100-12-31")]
    public void MapsOpenDate(string? selected, int? from, int? to, string? expectedFrom, string? expectedTo)
    {
        // arrange
        var viewModel = new UserDefinedTrustCharacteristicViewModel
        {
            FormationYear = selected,
            FormationYearFrom = from,
            FormationYearTo = to
        };

        // act
        var actual = new PostTrustComparatorsRequest(_companyName, viewModel).OpenDate;

        // assert
        Assert.Equal(expectedFrom == null ? null : DateTime.Parse(expectedFrom), actual?.From);
        Assert.Equal(expectedTo == null ? null : DateTime.Parse(expectedTo), actual?.To);
    }
}