using AutoFixture;
using Web.App.Controllers.Api.Mappers;
using Web.App.Domain.NonFinancial;
using Xunit;

namespace Web.Tests.Controllers.Api.Mappers;

public class WhenEducationHealthCarePlansHistoryResponseMapperMapsWithCode
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public void ShouldMapToEducationHealthCarePlansHistoryResponse()
    {
        const string code = nameof(code);
        const int startYear = 2021;
        const int endYear = 2024;

        List<LocalAuthorityNumberOfPlansYear> plans = [];

        for (var year = startYear; year <= endYear; year++)
        {
            var plan = Fixture
                .Build<LocalAuthorityNumberOfPlansYear>()
                .With(p => p.Year, year)
                .With(p => p.Code, code)
                .Create();

            plans.Add(plan);
        }

        var history = Fixture
            .Build<EducationHealthCarePlansHistory<LocalAuthorityNumberOfPlansYear>>()
            .With(h => h.StartYear, startYear)
            .With(h => h.EndYear, endYear)
            .With(h => h.Plans, plans.ToArray())
            .Create();

        var result = history.MapToApiResponse(code).ToArray();

        foreach (var actual in result)
        {
            var expected = history.Plans?.Where(p => p.Year == actual.Year).FirstOrDefault();
            Assert.NotNull(expected);

            Assert.Equal(expected.Year, actual.Year);
            Assert.Equal($"{actual.Year - 1} to {actual.Year}", actual.Term);
            Assert.Equal(expected.Total, actual.Total);
            Assert.Equal(expected.Mainstream, actual.Mainstream);
            Assert.Equal(expected.Resourced, actual.Resourced);
            Assert.Equal(expected.Special, actual.Special);
            Assert.Equal(expected.Independent, actual.Independent);
            Assert.Equal(expected.Hospital, actual.Hospital);
            Assert.Equal(expected.Post16, actual.Post16);
            Assert.Equal(expected.Other, actual.Other);
        }
    }
}