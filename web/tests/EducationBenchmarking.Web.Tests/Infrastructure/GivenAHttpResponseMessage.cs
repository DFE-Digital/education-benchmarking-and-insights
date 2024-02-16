using System.Net;
using AutoFixture;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using FluentValidation;
using Newtonsoft.Json.Linq;
using Xunit;

namespace EducationBenchmarking.Web.Tests.Infrastructure;

public class GivenAHttpResponseMessage
{
    private readonly Fixture _fixture = new();

    private static HttpResponseMessage CreateMessage(HttpStatusCode statusCode, HttpContent? content = null)
    {
        return new HttpResponseMessage(statusCode)
        {
            Content = content
        };
    }

    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.Created)]
    [InlineData(HttpStatusCode.Accepted)]
    public async Task ShouldNotThrow(HttpStatusCode code)
    {
        var result = await CreateMessage(code, new JsonContent(new { ID = 1 })).ToApiResult();
        var exception = Record.Exception(() => result.EnsureSuccess());

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(HttpStatusCode.Created)]
    [InlineData(HttpStatusCode.Accepted)]
    public async Task CanReadTextResult(HttpStatusCode code)
    {
        var result = await CreateMessage(code, new StringContent("item/1")).ToApiResult();
        var body = result.GetResultOrThrow<string>();

        Assert.Equal("item/1", body);
    }

    [Theory]
    [InlineData(HttpStatusCode.Created)]
    [InlineData(HttpStatusCode.Accepted)]
    public async Task CanReadJsonResult(HttpStatusCode code)
    {
        var result = await CreateMessage(code, new JsonContent(new { Id = 1 })).ToApiResult();
        var body = result.GetResultOrThrow<JObject>();

        Assert.NotNull(body);
        Assert.Equal(1, body.Value<int>("id"));
    }


    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    public async Task ShouldThrow(HttpStatusCode code)
    {
        var result = await CreateMessage(code).ToApiResult();

        Assert.Throws<StatusCodeException>(() => result.EnsureSuccess());
    }

    [Fact]
    public async Task ShouldThrowConflictException()
    {
        var result = await CreateMessage(HttpStatusCode.Conflict, new JsonContent(_fixture.Create<ConflictData>()))
            .ToApiResult();

        Assert.Throws<DataConflictException>(() => result.EnsureSuccess());
    }

    [Fact]
    public async Task ShouldThrowValidationException()
    {
        var result = await CreateMessage(HttpStatusCode.BadRequest,
            new JsonContent(_fixture.CreateMany<ValidationError>().ToArray())).ToApiResult();

        Assert.Throws<ValidationException>(() => result.EnsureSuccess());
    }
}