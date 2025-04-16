using Xunit;

namespace Platform.Search.Tests.Requests;

public class WhenSearchRequestCreatesFilterExpression
{
    [Theory]
    [InlineData(0, null)]
    [InlineData(1, $"({nameof(FilterCriteria.Field)} eq '{nameof(FilterCriteria.Value)}')")]
    [InlineData(2, $"({nameof(FilterCriteria.Field)} eq '{nameof(FilterCriteria.Value)}' or {nameof(FilterCriteria.Field)} eq '{nameof(FilterCriteria.Value)}')")]
    public void ShouldReturnExpectedString(int filterCount, string? expected)
    {
        var request = new SearchRequest
        {
            Filters = Enumerable.Range(0, filterCount)
                .Select(_ => new FilterCriteria
                {
                    Field = nameof(FilterCriteria.Field),
                    Value = nameof(FilterCriteria.Value)
                })
                .ToArray()
        };

        var result = request.FilterExpression();

        Assert.Equal(expected, result);
    }
}