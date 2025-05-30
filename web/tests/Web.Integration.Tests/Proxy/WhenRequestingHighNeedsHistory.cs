using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingHighNeedsHistory(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task CanReturnCorrectResponse()
    {
        var history = Fixture
            .Build<HighNeedsHistory<HighNeedsYear>>()
            .With(h => h.StartYear, 2021)
            .With(h => h.EndYear, 2022)
            .Create();
        const string code = "123";
        var response = await client
            .SetupHighNeeds(null, history)
            .Get(Paths.ApiHighNeedsHistory(code));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<LocalAuthorityHighNeedsHistoryResponse[]>(resultContent);

        Assert.NotNull(actual);

        var startYear = actual.FirstOrDefault(y => y.Year == history.StartYear);
        Assert.NotNull(startYear);

        var endYear = actual.FirstOrDefault(y => y.Year == history.EndYear);
        Assert.NotNull(endYear);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = "123";
        var response = await client
            .SetupLocalAuthoritiesWithException()
            .Get(Paths.ApiHighNeedsHistory(code));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}