using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Platform.Infrastructure.Search;
using Xunit;

namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesSearchSchoolsRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        SchoolSearch
            .Setup(d => d.SearchAsync(It.IsAny<PostSearchRequestModel>()))
            .ReturnsAsync(new SearchResponseModel<SchoolResponseModel>());

        var result = await Functions.SearchSchoolsAsync(CreateRequestWithBody(new PostSearchRequestModel())) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        SchoolSearch
            .Setup(d => d.SearchAsync(It.IsAny<PostSearchRequestModel>()))
            .Throws(new Exception());

        var result = await Functions.SearchSchoolsAsync(CreateRequestWithBody(new PostSearchRequestModel())) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}