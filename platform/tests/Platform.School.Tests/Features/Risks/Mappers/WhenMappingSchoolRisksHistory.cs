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
                OverallMax = 20.0m,
                Financial = 2.0m,
                FinancialMax = 10.0m,
                SchoolAndPupil = 3.0m,
                SchoolAndPupilMax = 5.0m,
                EducationalPerformance = 4.0m,
                EducationalPerformanceMax = 5.0m,
            },
            new RisksHistoryModelDto
            {
                RunId = 2021,
                Urn = "123456",
                SchoolName = "Test School",
                OverallGrade = "B",
                Overall = 1.1m,
                OverallMax = 20.0m,
                Financial = 2.1m,
                FinancialMax = 10.0m,
                SchoolAndPupil = 3.1m,
                SchoolAndPupilMax = 5.0m,
                EducationalPerformance = 4.1m,
                EducationalPerformanceMax = 5.0m,
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
        Assert.Equal(20.0m, first.OverallMax);
        Assert.Equal(2.0m, first.Financial);
        Assert.Equal(10.0m, first.FinancialMax);
        Assert.Equal(3.0m, first.SchoolAndPupil);
        Assert.Equal(5.0m, first.SchoolAndPupilMax);
        Assert.Equal(4.0m, first.EducationalPerformance);
        Assert.Equal(5.0m, first.EducationalPerformanceMax);

        var second = result.Rows.Skip(1).First();
        Assert.Equal(2021, second.Year);
        Assert.Equal("123456", second.Urn);
        Assert.Equal("Test School", second.SchoolName);
        Assert.Equal("B", second.OverallGrade);
        Assert.Equal(1.1m, second.Overall);
        Assert.Equal(20.0m, second.OverallMax);
        Assert.Equal(2.1m, second.Financial);
        Assert.Equal(10.0m, second.FinancialMax);
        Assert.Equal(3.1m, second.SchoolAndPupil);
        Assert.Equal(5.0m, second.SchoolAndPupilMax);
        Assert.Equal(4.1m, second.EducationalPerformance);
        Assert.Equal(5.0m, second.EducationalPerformanceMax);
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
