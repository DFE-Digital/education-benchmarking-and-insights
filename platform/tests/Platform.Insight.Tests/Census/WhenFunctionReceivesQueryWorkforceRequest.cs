using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Census;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Census;

public class WhenFunctionReceivesQueryCensusRequest : CensusFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusQuerySchoolsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<CensusSchoolModel>());

        var result = await Functions.QueryCensusAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<CensusQuerySchoolsParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(CensusQuerySchoolsParameters.Dimension), "error message")
            ]));

        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<CensusSchoolModel>());

        var result = await Functions.QueryCensusAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(CensusQuerySchoolsParameters.Dimension));

        Service
            .Verify(x => x.QueryAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never());
    }
}