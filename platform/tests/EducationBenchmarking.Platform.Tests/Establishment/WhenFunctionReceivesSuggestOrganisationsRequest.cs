using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class WhenFunctionReceivesSuggestOrganisationsRequest : OrganisationsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Search
            .Setup(d => d.SuggestAsync(It.IsAny<PostSuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SuggestOutput<Organisation>());

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await Functions.SuggestOrganisationsAsync(CreateRequestWithBody(new PostSuggestRequest())) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {
        
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure(nameof(PostSuggestRequest.SuggesterName), "This error message") }));
        
        var result = await Functions.SuggestOrganisationsAsync(CreateRequestWithBody(new PostSuggestRequest())) as ValidationErrorsResult;

        Assert.NotNull(result);
        Assert.Equal(400, result?.StatusCode);

        var values = result?.Value as IEnumerable<ValidationError>;
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(PostSuggestRequest.SuggesterName));
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequest>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());
        
        var result = await Functions.SuggestOrganisationsAsync(CreateRequestWithBody(new PostSuggestRequest())) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}