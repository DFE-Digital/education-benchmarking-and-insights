using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Insight.Features.CommercialResources;
using Platform.Api.Insight.Features.CommercialResources.Parameters;
using Platform.Api.Insight.Features.CommercialResources.Responses;
using Platform.Api.Insight.Features.CommercialResources.Services;
using Platform.Domain;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.CommercialResources;

public class GetCommercialResourcesFunctionTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetCommercialResourcesFunction _function;
    private readonly Mock<ICommercialResourcesService> _service;
    private readonly Mock<IValidator<CommercialResourcesParameters>> _validator;

    public GetCommercialResourcesFunctionTests()
    {
        _service = new Mock<ICommercialResourcesService>();
        _validator = new Mock<IValidator<CommercialResourcesParameters>>();
        _function = new GetCommercialResourcesFunction(_service.Object, _validator.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var models = _fixture
            .Build<CommercialResourcesResponse>()
            .CreateMany()
            .ToArray();

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CommercialResourcesParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(x => x.GetCommercialResourcesByCategory(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(models);

        var query = new Dictionary<string, StringValues>
        {
            { "categories", CostCategories.All },
        };

        var result = await _function.RunAsync(CreateHttpRequestData(query), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<CommercialResourcesResponse[]>();
        Assert.NotNull(body);
        Assert.Equivalent(models, body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CommercialResourcesParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(CommercialResourcesParameters.Categories), "error")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(CommercialResourcesParameters.Categories));

        _service
            .Verify(d => d.GetCommercialResourcesByCategory(It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}