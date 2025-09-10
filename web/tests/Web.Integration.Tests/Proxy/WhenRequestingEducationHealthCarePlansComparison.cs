using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain.NonFinancial;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenEducationHealthCarePlansComparison(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task CanReturnCorrectResponseWhenComparatorSetExists()
    {
        const string code = "123";
        var set = new[]
        {
            "456",
            "789"
        };

        var plans = new[]
            {
                code
            }
            .Concat(set)
            .Select(c => Fixture
                .Build<LocalAuthorityNumberOfPlans>()
                .With(p => p.Code, c)
                .Create())
            .ToArray();

        var response = await client
            .SetupEducationHealthCarePlans(plans, null)
            .Get(Paths.ApiEducationHealthCarePlansComparison(code, set));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LocalAuthorityNumberOfPlans[]>(resultContent);

        Assert.NotNull(result);

        foreach (var actual in result)
        {
            var expected = plans.FirstOrDefault(p => p.Code == actual.Code);
            Assert.NotNull(expected);

            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Population2To18, actual.Population2To18);
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

    [Fact]
    public async Task CanReturnNotFoundWhenComparatorSetDoesNotExist()
    {
        const string code = "123";
        string[] set = [];

        var response = await client
            .Get(Paths.ApiEducationHealthCarePlansComparison(code, set));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = "123";
        var set = new[]
        {
            "456",
            "789"
        };

        var response = await client
            .SetupEducationHealthCarePlansWithException()
            .Get(Paths.ApiEducationHealthCarePlansComparison(code, set));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}