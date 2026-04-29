using AutoFixture;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.EducationHealthCarePlans;

public class MapperTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldMapEducationHealthCarePlansDtoToApiResponse()
    {
        var dtos = _fixture.CreateMany<EducationHealthCarePlansDto>(3).ToList();
        var result = dtos.MapToApiResponse().ToList();

        Assert.Equal(dtos.Count, result.Count);
        for (var i = 0; i < dtos.Count; i++)
        {
            Assert.Equal(dtos[i].LaCode, result[i].Code);
            Assert.Equal(dtos[i].Name, result[i].Name);
            Assert.Equal(dtos[i].Population2To18, result[i].Population2To18);
            Assert.Equal(dtos[i].TotalPupils, result[i].TotalPupils);
            Assert.Equal(dtos[i].Total, result[i].Total);
            Assert.Equal(dtos[i].Mainstream, result[i].Mainstream);
            Assert.Equal(dtos[i].Resourced, result[i].Resourced);
            Assert.Equal(dtos[i].Special, result[i].Special);
            Assert.Equal(dtos[i].Independent, result[i].Independent);
            Assert.Equal(dtos[i].Hospital, result[i].Hospital);
            Assert.Equal(dtos[i].Post16, result[i].Post16);
            Assert.Equal(dtos[i].Other, result[i].Other);
        }
    }

    [Fact]
    public void ShouldMapEducationHealthCarePlansYearDtoToApiResponseWithValidYear()
    {
        var years = _fixture.Create<YearsModelDto>();
        var dtos = _fixture.Build<EducationHealthCarePlansYearDto>()
            .With(x => x.RunId, "2023")
            .CreateMany(3)
            .ToList();

        var result = years.MapToApiResponse(dtos);

        Assert.Equal(years.StartYear, result.StartYear);
        Assert.Equal(years.EndYear, result.EndYear);
        Assert.Equal(dtos.Count, result.Plans!.Length);

        var firstPlan = result.Plans.First();
        var firstDto = dtos.First();

        Assert.Equal(2023, firstPlan.Year);
        Assert.Equal(firstDto.LaCode, firstPlan.Code);
        Assert.Equal(firstDto.Name, firstPlan.Name);
        Assert.Equal(firstDto.Total, firstPlan.Total);
        Assert.Equal(firstDto.Mainstream, firstPlan.Mainstream);
        Assert.Equal(firstDto.Resourced, firstPlan.Resourced);
        Assert.Equal(firstDto.Special, firstPlan.Special);
        Assert.Equal(firstDto.Independent, firstPlan.Independent);
        Assert.Equal(firstDto.Hospital, firstPlan.Hospital);
        Assert.Equal(firstDto.Post16, firstPlan.Post16);
        Assert.Equal(firstDto.Other, firstPlan.Other);
    }

    [Fact]
    public void ShouldMapEducationHealthCarePlansYearDtoToApiResponseWithInvalidYear()
    {
        var years = _fixture.Create<YearsModelDto>();
        var dtos = _fixture.Build<EducationHealthCarePlansYearDto>()
            .With(x => x.RunId, "not_an_int")
            .CreateMany(1)
            .ToList();

        var result = years.MapToApiResponse(dtos);

        Assert.Null(result.Plans!.First().Year);
    }
}