﻿using System.Net;
using AutoFixture;
using FluentValidation;
using Newtonsoft.Json.Linq;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Xunit;
namespace Web.Tests.Infrastructure;

public class GivenAHttpResponseMessage
{
    private readonly Fixture _fixture = new();

    private static Task<HttpResponseMessage> CreateMessage(HttpStatusCode statusCode, HttpContent? content = null)
        => Task.FromResult(new HttpResponseMessage(statusCode)
        {
            Content = content
        });

    private static Task<HttpResponseMessage> CreateMessage(CancellationToken cancellationToken)
        => Task.FromCanceled<HttpResponseMessage>(cancellationToken);

    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.Created)]
    [InlineData(HttpStatusCode.Accepted)]
    public async Task ShouldNotThrow(HttpStatusCode code)
    {
        var result = await CreateMessage(code, new JsonContent(new
        {
            ID = 1
        })).ToApiResult();
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
        var result = await CreateMessage(code, new JsonContent(new
        {
            Id = 1
        })).ToApiResult();
        var body = result.GetResultOrThrow<JObject>();

        Assert.NotNull(body);
        Assert.Equal(1, body.Value<int>("id"));
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError, "The API returned `Internal Server Error` (underlying status code 500)")]
    [InlineData(HttpStatusCode.ServiceUnavailable, "The API returned `Service unavailable` (underlying status code 503)")]
    public async Task ShouldThrow(HttpStatusCode code, string expectedErrorMessage)
    {
        var result = await CreateMessage(code).ToApiResult();

        var exception = Assert.Throws<StatusCodeException>(() => result.EnsureSuccess());
        Assert.Equal(code, exception.Status);
        Assert.Equal(expectedErrorMessage, exception.Message);
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

    [Fact]
    public async Task ShouldReturn499WhenRequestCancelled()
    {
        var cancellationToken = new CancellationTokenSource(TimeSpan.Zero).Token;

        var result = await CreateMessage(cancellationToken).ToApiResult(cancellationToken);

        var exception = Assert.Throws<StatusCodeException>(() => result.EnsureSuccess());
        Assert.Equal(499, (int)exception.Status);
        Assert.Equal("The API returned `Client Closed Request` (underlying status code 499)", exception.Message);
    }
}