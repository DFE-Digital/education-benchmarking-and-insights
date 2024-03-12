using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain.Responses;
using Platform.Functions;
using Platform.Infrastructure.Search;
using Xunit;

namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesSearchSchoolsRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Search
            .Setup(d => d.SearchAsync(It.IsAny<PostSearchRequest>()))
            .ReturnsAsync(new SearchOutput<School>());

        var result = await Functions.SearchSchoolsAsync(CreateRequestWithBody(new PostSearchRequest())) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Search
            .Setup(d => d.SearchAsync(It.IsAny<PostSearchRequest>()))
            .Throws(new Exception());

        var result = await Functions.SearchSchoolsAsync(CreateRequestWithBody(new PostSearchRequest())) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}