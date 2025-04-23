using AutoFixture;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;
using Xunit;

namespace Platform.NonFinancial.Tests.EducationHealthCarePlans;

public class WhenEducationHealthCarePlansMapperMaps
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldMapWhenMapToLocalAuthorityNumberOfPlansResponse()
    {
        // arrange
        var value = _fixture.Create<LocalAuthorityNumberOfPlans>();

        // act
        var actual = Mapper.MapToLocalAuthorityNumberOfPlansResponse(value);

        // assert
        Assert.Equal(value.LaCode, actual.Code);
        Assert.Equal(value.Name, actual.Name);
        Assert.Equal(value.Population2To18, actual.Population2To18);
        Assert.Equal(value.Total, actual.Total);
        Assert.Equal(value.Mainstream, actual.Mainstream);
        Assert.Equal(value.Resourced, actual.Resourced);
        Assert.Equal(value.Special, actual.Special);
        Assert.Equal(value.Independent, actual.Independent);
        Assert.Equal(value.Hospital, actual.Hospital);
        Assert.Equal(value.Post16, actual.Post16);
        Assert.Equal(value.Other, actual.Other);
    }

    [Fact]
    public void ShouldMapWhenMapToLocalAuthorityNumberOfPlansYearResponse()
    {
        // arrange
        var value = _fixture.Build<LocalAuthorityNumberOfPlansYear>()
            .With(l => l.RunId, _fixture.Create<int>().ToString)
            .Create();

        // act
        var actual = Mapper.MapToLocalAuthorityNumberOfPlansYearResponse(value);

        // assert
        Assert.Equal(value.LaCode, actual.Code);
        Assert.Equal(value.Name, actual.Name);
        Assert.Equal(int.Parse(value.RunId), actual.Year);
        Assert.Equal(value.Total, actual.Total);
        Assert.Equal(value.Mainstream, actual.Mainstream);
        Assert.Equal(value.Resourced, actual.Resourced);
        Assert.Equal(value.Special, actual.Special);
        Assert.Equal(value.Independent, actual.Independent);
        Assert.Equal(value.Hospital, actual.Hospital);
        Assert.Equal(value.Post16, actual.Post16);
        Assert.Equal(value.Other, actual.Other);
    }
}