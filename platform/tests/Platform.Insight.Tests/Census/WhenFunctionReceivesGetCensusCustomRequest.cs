using System.Net;
using AutoFixture;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Census;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class WhenFunctionReceivesGetCensusCustomRequest : CensusFunctionsTestBase
{
    private const string Urn = "URN";

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = Fixture.Build<CensusSchoolModel>().Create();

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetCustomAsync(Urn, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(model);

        var result = await Functions.CustomCensusAsync(CreateHttpRequestData(), Urn, Guid.Empty.ToString());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<CensusSchoolModel>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.GetCustomAsync(Urn, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((CensusSchoolModel?)null);

        var result = await Functions.CustomCensusAsync(CreateHttpRequestData(), Urn, Guid.Empty.ToString());

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

        var result = await Functions.CustomCensusAsync(CreateHttpRequestData(), Urn, Guid.Empty.ToString());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(CensusParameters.Dimension));

        Service
            .Verify(d => d.GetCustomAsync(Urn, It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}