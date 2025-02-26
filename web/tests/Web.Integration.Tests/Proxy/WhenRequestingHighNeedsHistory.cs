using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingHighNeedsHistory(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task CanReturnCorrectResponse()
    {
        var history = Fixture.Create<History<LocalAuthorityHighNeedsYear>>();
        const string sort = nameof(sort);
        var response = await client
            .SetupHighNeedsHistory(history)
            .Get(Paths.ApiHighNeedsHistory(sort));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<History<LocalAuthorityHighNeedsYear>>(resultContent);
        Assert.Equivalent(history, actual);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string sort = nameof(sort);
        var response = await client
            .SetupLocalAuthoritiesWithException()
            .Get(Paths.ApiHighNeedsHistory(sort));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}