using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Platform.Functions.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Functions.Tests.Extensions;

public class WhenHttpRequestDataCreateUnsupportedVersion
{
    [Fact]
    public async Task ReturnsExpectedProblemDetailsResponse()
    {
        var request = MockHttpRequestData.Create();
        var result = await request.CreateUnsupportedVersionResponseAsync();

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.True(result.Headers.TryGetValues("Content-Type", out var contentType));
        Assert.Contains("application/problem+json", contentType);

        result.Body.Position = 0;

        var problem = await JsonSerializer.DeserializeAsync<ProblemDetails>(result.Body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(problem);
        Assert.Equal("Unsupported API version", problem.Title);
        Assert.Equal(400, problem.Status);
        Assert.Contains("x-api-version", problem.Detail);
        Assert.Equal("/", problem.Instance);
    }
}