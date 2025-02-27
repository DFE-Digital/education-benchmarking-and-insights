using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.NonFinancial;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenEducationHealthCarePlansHistory(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task CanReturnCorrectResponse()
    {
        const string code = nameof(code);
        const int startYear = 2021;
        const int endYear = 2022;

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


        var response = await client
            .SetupEducationHealthCarePlans(history)
            .Get(Paths.ApiEducationHealthCarePlansHistory(code));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<EducationHealthCarePlansHistoryResponse[]>(resultContent);

        Assert.NotNull(result);

        foreach (var actual in result)
        {
            var expected = history.Plans?.Where(p => p.Year == actual.Year).FirstOrDefault();
            Assert.NotNull(expected);

            Assert.Equal(expected.Year, actual.Year);
            Assert.Equal($"{actual.Year - 1} to {actual.Year}", actual.Term);
            Assert.Equal(expected.Total, actual.Total);
            Assert.Equal(expected.Mainstream, actual.Mainstream);
            Assert.Equal(expected.Resourced, actual.Resourced);
            Assert.Equal(expected.Independent, actual.Independent);
            Assert.Equal(expected.Hospital, actual.Hospital);
            Assert.Equal(expected.Post16, actual.Post16);
            Assert.Equal(expected.Other, actual.Other);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = nameof(code);
        var response = await client
            .SetupEducationHealthCarePlansWithException()
            .Get(Paths.ApiEducationHealthCarePlansHistory(code));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}