using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;
using Platform.Domain;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds;

public class GetHighNeedsFunctionTests : FunctionsTestBase
{
    private const string Code = nameof(Code);
    private const string Dimension = Dimensions.HighNeeds.PerHead;
    private readonly Fixture _fixture;
    private readonly GetHighNeedsFunction _function;
    private readonly Mock<IHighNeedsService> _service;
    private readonly Mock<IValidator<HighNeedsDimensionedParameters>> _validator;

    public GetHighNeedsFunctionTests()
    {
        _service = new Mock<IHighNeedsService>();
        _validator = new Mock<IValidator<HighNeedsDimensionedParameters>>();
        _fixture = new Fixture();
        _function = new GetHighNeedsFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var models = _fixture
            .Build<LocalAuthority<Api.LocalAuthorityFinances.Features.HighNeeds.Models.HighNeeds>>()
            .CreateMany()
            .ToArray();
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsDimensionedParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.Get(new[]
            {
                Code
            }, Dimension, It.IsAny<CancellationToken>()))
            .ReturnsAsync(models);

        var query = new Dictionary<string, StringValues>
        {
            { "code", Code },
            { "dimension", Dimension }
        };

        var result = await _function.RunAsync(CreateHttpRequestData(query), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<LocalAuthority<Api.LocalAuthorityFinances.Features.HighNeeds.Models.HighNeeds>[]>();
        Assert.NotNull(body);
        Assert.Equivalent(models, body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsDimensionedParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(HighNeedsParameters.Codes), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(HighNeedsParameters.Codes));

        _service
            .Verify(d => d.Get(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}