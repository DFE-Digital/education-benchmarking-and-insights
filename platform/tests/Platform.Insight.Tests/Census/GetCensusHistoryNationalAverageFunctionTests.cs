using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Census;
using Platform.Api.Insight.Features.Census.Models;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Api.Insight.Features.Census.Responses;
using Platform.Api.Insight.Features.Census.Services;
using Platform.Api.Insight.Shared;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class GetCensusHistoryNationalAverageFunctionTests : FunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
    private readonly GetCensusHistoryNationalAverageFunction _function;
    private readonly Mock<ICensusService> _service;
    private readonly Mock<IValidator<CensusNationalAvgParameters>> _validator;
    private readonly Fixture _fixture;

    public GetCensusHistoryNationalAverageFunctionTests()
    {
        _validator = new Mock<IValidator<CensusNationalAvgParameters>>();
        _service = new Mock<ICensusService>();
        _fixture = new Fixture();
        _function = new GetCensusHistoryNationalAverageFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var history = _fixture.CreateMany<CensusHistoryModel>(5);
        var years = new YearsModel { StartYear = 2019, EndYear = 2023 };

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((years, history));

        var result = await _function.RunAsync(CreateHttpRequestData(), _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<CensusHistoryResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusNationalAvgParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(CensusNationalAvgParameters.Dimension), "error message")
            ]));

        _service
            .Setup(d => d.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(CensusNationalAvgParameters.Dimension));

        _service.Verify(
            x => x.GetNationalAvgHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
    }
}