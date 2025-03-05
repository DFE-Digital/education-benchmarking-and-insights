using System.Net;
using AutoFixture;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Benchmark.Features.UserData.Parameters;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Benchmark.Tests.UserData;

public class WhenFunctionReceivesQueryUserDataRequest : UserDataFunctionsTestBase
{
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

        var userData = Fixture.CreateMany<Api.Benchmark.Features.UserData.Models.UserData>();

        Service
            .Setup(d => d.QueryAsync(userId, type, status, id, organisationId, organisationType))
            .ReturnsAsync(userData)
            .Verifiable(Times.Once);

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<UserDataParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result = await Functions.QueryAsync(CreateHttpRequestData(query));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var actual = await result.ReadAsJsonAsync<Api.Benchmark.Features.UserData.Models.UserData[]>();
        Assert.NotNull(actual);
        Assert.Equivalent(userData, actual);
        Service.Verify();
    }


    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<UserDataParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(UserDataParameters.UserId), "error message")
            ]));

        var result = await Functions.QueryAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(UserDataParameters.UserId));

        Service
            .Setup(d => d.QueryAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>()))
            .Verifiable(Times.Never);
    }
}