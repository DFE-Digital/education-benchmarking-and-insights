using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Benchmark.Features.UserData;
using Platform.Api.Benchmark.Features.UserData.Parameters;
using Platform.Api.Benchmark.Features.UserData.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Benchmark.Tests.UserData;

public class WhenGetUserDataFunctionRuns : FunctionsTestBase
{
    private readonly Fixture _fixture = new();
    private readonly GetUserDataFunction _function;
    private readonly Mock<IUserDataService> _service;
    private readonly Mock<IValidator<UserDataParameters>> _validator;

    public WhenGetUserDataFunctionRuns()
    {
        _service = new Mock<IUserDataService>();
        _validator = new Mock<IValidator<UserDataParameters>>();
        _function = new GetUserDataFunction(_service.Object, _validator.Object);
    }

    [Theory]
    [InlineData("userId", null, null, null, null, null)]
    public async Task ShouldReturn200OnValidRequest(string userId, string? type, string? status, string? id, string? organisationId, string? organisationType)
    {
        var query = new Dictionary<string, StringValues>
        {
            { "userId", userId },
            { "type", type },
            { "status", status },
            { "id", id },
            { "organisationId", organisationId },
            { "organisationType", organisationType }
        };

        var userData = _fixture.CreateMany<Api.Benchmark.Features.UserData.Models.UserData>();

        _service
            .Setup(d => d.QueryAsync(userId, type, status, id, organisationId, organisationType))
            .ReturnsAsync(userData)
            .Verifiable(Times.Once);

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<UserDataParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result = await _function.RunAsync(CreateHttpRequestData(query));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var actual = await result.ReadAsJsonAsync<Api.Benchmark.Features.UserData.Models.UserData[]>();
        Assert.NotNull(actual);
        Assert.Equivalent(userData, actual);
        _service.Verify();
    }


    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<UserDataParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(UserDataParameters.UserId), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(UserDataParameters.UserId));

        _service
            .Setup(d => d.QueryAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>()))
            .Verifiable(Times.Never);
    }
}