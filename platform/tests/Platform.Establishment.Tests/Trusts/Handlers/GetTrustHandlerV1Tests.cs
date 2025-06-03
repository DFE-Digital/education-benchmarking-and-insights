using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.Trusts.Handlers;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Trusts.Handlers;

public class GetTrustV1HandlerTests : HandlerTestBase
{
    private readonly string _companyNumber;
    private readonly GetTrustV1Handler _handler;
    private readonly Mock<ITrustsService> _service;
    private readonly Trust _trust;

    public GetTrustV1HandlerTests()
    {
        var fixture = new Fixture();
        _companyNumber = fixture.Create<string>();
        var schools = fixture
            .Build<TrustSchool>()
            .CreateMany(10)
            .ToArray();
        _trust = fixture
            .Build<Trust>()
            .With(l => l.CompanyNumber, _companyNumber)
            .With(l => l.Schools, schools)
            .Create();

        _service = new Mock<ITrustsService>();
        _handler = new GetTrustV1Handler(_service.Object);
    }

    [Fact]
    public async Task HandleAsync_Returns200AndTrust_WhenTrustFound()
    {
        _service
            .Setup(s => s.GetAsync(_companyNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_trust);

        var request = CreateHttpRequestData();
        var response = await _handler.HandleAsync(request, _companyNumber, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, response.ContentType());

        var actual = await response.ReadAsJsonAsync<Trust>();
        Assert.NotNull(actual);
        Assert.Equal(_companyNumber, actual.CompanyNumber);
    }

    [Fact]
    public async Task HandleAsync_Returns404_WhenTrustNotFound()
    {
        _service
            .Setup(s => s.GetAsync(_companyNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Trust?)null);

        var request = CreateHttpRequestData();

        var response = await _handler.HandleAsync(request, _companyNumber, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public void VersionProperty_ReturnsExpectedVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }
}