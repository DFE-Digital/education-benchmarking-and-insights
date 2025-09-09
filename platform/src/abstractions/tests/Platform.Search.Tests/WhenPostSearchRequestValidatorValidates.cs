using System.Collections;
using Xunit;

namespace Platform.Search.Tests;

public class WhenPostSearchRequestValidatorValidates
{
    private readonly PostSearchRequestValidator _validator = new();

    [Theory]
    [ClassData(typeof(ValidSearchRequestData))]
    public async Task ShouldBeValidWithGoodRequest(SearchRequest request)
    {
        var actual = await _validator.ValidateAsync(request);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [ClassData(typeof(InvalidSearchRequestData))]
    public async Task ShouldBeInvalidWithBadRequest(SearchRequest request, string expectedErrorMessage)
    {
        var actual = await _validator.ValidateAsync(request);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
        Assert.Equal(expectedErrorMessage, actual.Errors[0].ErrorMessage);
    }
}

public class ValidSearchRequestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new SearchRequest
            {
                SearchText = "tes"
            }
        ];
        yield return
        [
            new SearchRequest
            {
                SearchText = new string('a', 100)
            }
        ];
        yield return
        [
            new SearchRequest
            {
                SearchText = "test",
                OrderBy = new OrderByCriteria
                {
                    Field = "SchoolName",
                    Value = "asc"
                }
            }
        ];

        yield return
        [
            new SearchRequest
            {
                SearchText = "test",
                OrderBy = new OrderByCriteria
                {
                    Field = "SchoolName",
                    Value = "desc"
                }
            }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class InvalidSearchRequestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new SearchRequest
            {
                SearchText = null
            },
            "'Search Text' must not be empty."
        ];
        yield return
        [
            new SearchRequest
            {
                SearchText = "te"
            },
            "The length of 'Search Text' must be at least 3 characters. You entered 2 characters."
        ];
        yield return
        [
            new SearchRequest
            {
                SearchText = new string('a', 101)
            },
            "The length of 'Search Text' must be 100 characters or fewer. You entered 101 characters."
        ];
        yield return
        [
            new SearchRequest
            {
                SearchText = "test",
                OrderBy = new OrderByCriteria
                {
                    Field = "SchoolName",
                    Value = "test"
                }
            },
            $"Order By must empty or be one of the supported values: {string.Join(", ", Sort.All)}"
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}