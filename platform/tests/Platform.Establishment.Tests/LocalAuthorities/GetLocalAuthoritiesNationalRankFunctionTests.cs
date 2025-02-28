using System.Net;
using AutoFixture;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

public class GetLocalAuthoritiesNationalRankFunctionTests : FunctionsTestBase
{
    private readonly LocalAuthorityRanking _localAuthorityRanking;
    private readonly GetLocalAuthoritiesNationalRankFunction _function;
    private readonly Mock<ILocalAuthorityRankingService> _service;

    public GetLocalAuthoritiesNationalRankFunctionTests()
    {
        _service = new Mock<ILocalAuthorityRankingService>();
        _function = new GetLocalAuthoritiesNationalRankFunction(_service.Object);

        var fixture = new Fixture();
        _localAuthorityRanking = fixture
            .Create<LocalAuthorityRanking>();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string sort = nameof(sort);
        _service
            .Setup(d => d.GetRanking(sort))
            .ReturnsAsync(_localAuthorityRanking);

        var result = await _function.RunAsync(CreateHttpRequestData(), sort);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<LocalAuthorityRanking>();
        Assert.NotNull(body);
    }
}