using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Census;
using Platform.Api.Insight.Features.Census.Models;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Api.Insight.Features.Census.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class GetCensusQueryFunctionTests : FunctionsTestBase
{
    private readonly GetCensusQueryFunction _function;
    private readonly Mock<ICensusService> _service;
    private readonly Mock<IValidator<CensusQuerySchoolsParameters>> _validator;

    public GetCensusQueryFunctionTests()
    {
        _validator = new Mock<IValidator<CensusQuerySchoolsParameters>>();
        _service = new Mock<ICensusService>();
        _function = new GetCensusQueryFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusQuerySchoolsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<CensusSchoolModel>());

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusQuerySchoolsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(CensusQuerySchoolsParameters.Dimension), "error message")
            ]));

        _service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<CensusSchoolModel>());

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(CensusQuerySchoolsParameters.Dimension));

        _service
            .Verify(x => x.QueryAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never());
    }
}