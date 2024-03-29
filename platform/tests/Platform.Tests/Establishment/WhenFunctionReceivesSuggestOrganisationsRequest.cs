using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Domain;
using Platform.Functions;
using Platform.Infrastructure.Search;
using Xunit;

namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesSuggestOrganisationsRequest : OrganisationsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Search
            .Setup(d => d.SuggestAsync(It.IsAny<PostSuggestRequestModel>()))
            .ReturnsAsync(new SuggestResponseModel<OrganisationResponseModel>());

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequestModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await Functions.SuggestOrganisationsAsync(CreateRequestWithBody(new PostSuggestRequestModel())) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequestModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure(nameof(PostSuggestRequestModel.SuggesterName), "This error message") }));

        var result = await Functions.SuggestOrganisationsAsync(CreateRequestWithBody(new PostSuggestRequestModel())) as ValidationErrorsResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);

        var values = result.Value as IEnumerable<ValidationError>;
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(PostSuggestRequestModel.SuggesterName));
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequestModel>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());

        var result = await Functions.SuggestOrganisationsAsync(CreateRequestWithBody(new PostSuggestRequestModel())) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}