using AutoFixture;
using Web.App.Controllers.Api.Mappers;
using Web.App.Domain.NonFinancial;
using Xunit;

namespace Web.Tests.Controllers.Api.Mappers;

public class WhenEducationHealthCarePlansComparisonResponseMapperMaps
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public void ShouldMapToEducationHealthCarePlansHistoryResponse()
    {
        var codes = new[] { "code1", "code2", "code3" };

        var plans = codes.Select(c => Fixture
                .Build<LocalAuthorityNumberOfPlans>()
                .With(p => p.Code, c)
                .Create())
            .ToArray();

        var results = plans.ToArray().MapToApiResponse().ToArray();

        foreach (var actual in results)
        {
            var expected = plans.FirstOrDefault(p => p.Code == actual.Code);
            Assert.NotNull(expected);

            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.Name, actual.Name);
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