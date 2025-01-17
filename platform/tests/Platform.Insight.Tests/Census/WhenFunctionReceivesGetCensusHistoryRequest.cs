using System.Net;
using AutoFixture;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Census;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class WhenFunctionReceivesGetCensusHistoryRequest : CensusFunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var history = Fixture.CreateMany<CensusHistoryModel>(5);
        var years = new CensusYearsModel { StartYear = 2019, EndYear = 2023 };

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((years, history));

        var result = await Functions.CensusHistoryAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<CensusHistoryResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((null, Array.Empty<CensusHistoryModel>()));

        var result = await Functions.CensusHistoryAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(CensusParameters.Dimension), "error message")
            ]));

        var result = await Functions.CensusHistoryAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(CensusParameters.Dimension));

        Service
            .Verify(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}