using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingHighNeedsComparison(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();

    [Fact]
    public async Task CanReturnCorrectResponse()
    {
        const string code = nameof(code);
        var set = new[] { "code2", "code3" };

        var localAuthorities = new[] { code }
            .Concat(set)
            .Select(c => Fixture
                .Build<LocalAuthority<HighNeeds>>()
                .With(p => p.Code, c)
                .Create())
            .ToArray();

        var response = await client
            .SetupLocalAuthoritiesComparators(code, set)
            .SetupHighNeeds(localAuthorities, null)
            .Get(Paths.ApiHighNeedsComparison(code));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LocalAuthorityHighNeedsComparisonResponse[]>(resultContent);

        Assert.NotNull(result);

        foreach (var actual in result)
        {
            var expected = localAuthorities.FirstOrDefault(p => p.Code == actual.Code);
            Assert.NotNull(expected);

            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.Name, actual.Name);
        }
    }

    [Fact]
    public async Task CanReturnNotFoundWhenComparatorSetDoesNotExist()
    {
        const string code = nameof(code);
        string[] set = [];

        var response = await client
            .SetupLocalAuthoritiesComparators(code, set)
            .Get(Paths.ApiHighNeedsComparison(code));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = nameof(code);
        var set = new[] { "code2", "code3" };

        var response = await client
            .SetupLocalAuthoritiesComparators(code, set)
            .SetupLocalAuthoritiesWithException()
            .Get(Paths.ApiHighNeedsComparison(code));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}