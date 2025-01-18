using System.Net;
using AutoFixture;
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

public class GetCensusCustomFunctionTests : FunctionsTestBase
{
    private const string Urn = "URN";
    private readonly GetCensusCustomFunction _function;
    private readonly Mock<ICensusService> _service;
    private readonly Mock<IValidator<CensusParameters>> _validator;
    private readonly Fixture _fixture;

    public GetCensusCustomFunctionTests()
    {
        _validator = new Mock<IValidator<CensusParameters>>();
        _service = new Mock<ICensusService>();
        _fixture = new Fixture();
        _function = new GetCensusCustomFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<CensusSchoolModel>().Create();

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetCustomAsync(Urn, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData(), Urn, Guid.Empty.ToString());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<CensusSchoolModel>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetCustomAsync(Urn, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((CensusSchoolModel?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), Urn, Guid.Empty.ToString());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(CensusParameters.Dimension), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData(), Urn, Guid.Empty.ToString());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(CensusParameters.Dimension));

        _service
            .Verify(d => d.GetCustomAsync(Urn, It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}