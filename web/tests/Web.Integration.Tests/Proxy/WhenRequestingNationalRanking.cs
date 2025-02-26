using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingNationalRanking(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task CanReturnCorrectResponse()
    {
        var ranking = Fixture.Create<LocalAuthorityRanking>();
        const string sort = nameof(sort);
        var response = await client
            .SetupLocalAuthoritiesNationalRank(ranking)
            .Get(Paths.ApiNationalRank(sort));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<LocalAuthorityRanking>(resultContent);
        Assert.Equivalent(ranking, actual);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string sort = nameof(sort);
        var response = await client
            .SetupEstablishmentWithException()
            .Get(Paths.ApiNationalRank(sort));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}