using Platform.Api.School.Features.Risks;
using Platform.Api.School.Features.Risks.Models;
using Xunit;

namespace Platform.School.Tests.Features.Risks.Mappers;

public class WhenMappingSchoolRisksHistory
{
    [Fact]
    public void ShouldMapYearsAndRowsCorrectly()
    {
        var years = new YearsModelDto
        {
            StartYear = 2020,
            EndYear = 2021
        };

        var rows = new[]
        {
            new RisksHistoryModelDto
            {
                RunId = 2020,
                Urn = "123456",
                SchoolName = "Test School",
                OverallGrade = "A",
                Overall = 1.0m,
                Financial = 2.0m,
                SchoolAndPupil = 3.0m,
                EducationalPerformance = 4.0m
            },
            new RisksHistoryModelDto
            {
                RunId = 2021,
                Urn = "123456",
                SchoolName = "Test School",
                OverallGrade = "B",
                Overall = 1.1m,
                Financial = 2.1m,
                SchoolAndPupil = 3.1m,
                EducationalPerformance = 4.1m
            }
        };

        var result = years.MapToApiResponse(rows);

        Assert.Equal(2020, result.StartYear);
        Assert.Equal(2021, result.EndYear);
        Assert.Equal(2, result.Rows.Count());

        var first = result.Rows.First();
        Assert.Equal(2020, first.Year);
        Assert.Equal("123456", first.Urn);
        Assert.Equal("Test School", first.SchoolName);
        Assert.Equal("A", first.OverallGrade);
        Assert.Equal(1.0m, first.Overall);
        Assert.Equal(2.0m, first.Financial);
        Assert.Equal(3.0m, first.SchoolAndPupil);
        Assert.Equal(4.0m, first.EducationalPerformance);

        var second = result.Rows.Skip(1).First();
        Assert.Equal(2021, second.Year);
        Assert.Equal("123456", second.Urn);
        Assert.Equal("Test School", second.SchoolName);
        Assert.Equal("B", second.OverallGrade);
        Assert.Equal(1.1m, second.Overall);
        Assert.Equal(2.1m, second.Financial);
        Assert.Equal(3.1m, second.SchoolAndPupil);
        Assert.Equal(4.1m, second.EducationalPerformance);
    }

    [Fact]
    public void ShouldThrowWhenModelIsNull()
    {
        var years = new YearsModelDto
        {
            StartYear = 2020,
            EndYear = 2021
        };

        var rows = new RisksHistoryModelDto?[]
        {
            null
        };

        var result = years.MapToApiResponse(rows!);
        Assert.Throws<ArgumentNullException>(() => result.Rows.First());
    }

    [Fact]
    public void ShouldMapEmptyRowsCollection()
    {
        var years = new YearsModelDto
        {
            StartYear = 2020,
            EndYear = 2021
        };

        var rows = Enumerable.Empty<RisksHistoryModelDto>();

        var result = years.MapToApiResponse(rows);

        Assert.Equal(2020, result.StartYear);
        Assert.Equal(2021, result.EndYear);
        Assert.Empty(result.Rows);
    }
}
