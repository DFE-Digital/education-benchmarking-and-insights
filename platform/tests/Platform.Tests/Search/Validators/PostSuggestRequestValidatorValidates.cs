using Platform.Search.Requests;
using Platform.Search.Validators;
using Xunit;
namespace Platform.Tests.Search.Validators;

public class PostSuggestRequestValidatorValidates
{
    private readonly PostSuggestRequestValidator _validator = new();

    [Theory]
    [InlineData("name", "text", 10)]
    public async Task ShouldValidateAndEvaluateGoodRequestAsValid(string? suggesterName, string? searchText, int size)
    {
        var request = new SuggestRequest
        {
            SuggesterName = suggesterName,
            SearchText = searchText,
            Size = size
        };

        var actual = await _validator.ValidateAsync(request);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("name", "text", 1)]
    [InlineData("name", "te", 10)]
    [InlineData("name", "0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789", 10)]
    [InlineData(null, null, 10)]
    public async Task ShouldValidateAndEvaluateBadRequestAsInvalid(string? suggesterName, string? searchText, int size)
    {
        var request = new SuggestRequest
        {
            SuggesterName = suggesterName,
            SearchText = searchText,
            Size = size
        };

        var actual = await _validator.ValidateAsync(request);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}