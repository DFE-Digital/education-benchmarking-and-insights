using Xunit;
using Platform.Api.Establishment.Features.Schools.Validators;
using Platform.Search;
using System.Collections;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Domain;

namespace Platform.Establishment.Tests.Schools.Validators;

public class WhenSchoolsSearchValidatorValidates
{
    private readonly SchoolsSearchValidator _validator = new();

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
        yield return [new SearchRequest { SearchText = "tes" }];
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
                    Filters =
                    [
                        new FilterCriteria { Field = "OverallPhase", Value = "Primary" }
                    ]
                }
            ];
        yield return
            [
                new SearchRequest
                {
                    SearchText = "test",
                    Filters =
                    [
                        new FilterCriteria { Field = "OverallPhase", Value = "Primary" },
                        new FilterCriteria { Field = "OverallPhase", Value = "Secondary" },
                        new FilterCriteria { Field = "OverallPhase", Value = "All-through" }
                    ]
                }
            ];
        yield return
            [
                new SearchRequest
                { SearchText = "test", OrderBy = new OrderByCriteria { Field = "SchoolNameSortable", Value = "asc" } }
            ];
        yield return
            [
                new SearchRequest
                { SearchText = "test", OrderBy = new OrderByCriteria { Field = "SchoolNameSortable", Value = "desc" } }
            ];
        yield return
            [
                new SearchRequest
                {
                    SearchText = "test",
                    Filters = [new FilterCriteria { Field = "OverallPhase", Value = "Primary" }],
                    OrderBy = new OrderByCriteria { Field = "SchoolNameSortable", Value = "asc" }
                }
            ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class InvalidSearchRequestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [new SearchRequest { SearchText = null }, "'Search Text' must not be empty."];
        yield return
            [
                new SearchRequest { SearchText = "te" },
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
                OrderBy = new OrderByCriteria { Field = "test", Value = "asc" }
            },
            "OrderBy Field must be SchoolNameSortable"
        ];
        yield return
        [
            new SearchRequest
            {
                SearchText = "test",
                OrderBy = new OrderByCriteria { Field = "SchoolNameSortable", Value = "test" }
            },
            $"Order By must empty or be one of the supported values: {string.Join(", ", Sort.All)}"
        ];
        yield return
        [
            new SearchRequest
            {
                SearchText = "test",
                Filters = [new FilterCriteria { Field = "test", Value = "Primary" }]
            },
            $"Each Filter Field must be {nameof(School.OverallPhase)}"
        ];
        yield return
        [
            new SearchRequest
            {
                SearchText = "test",
                Filters = [new FilterCriteria { Field = "OverallPhase", Value = "test" }]
            },
            $"Filters must be one of the supported values: {string.Join(", ", OverallPhase.All)}"
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}