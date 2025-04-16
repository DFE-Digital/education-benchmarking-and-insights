using Platform.Api.Establishment.Features.Schools.Requests;
using Xunit;

namespace Platform.Establishment.Tests.Schools.Requests;

public class WhenSchoolSuggestRequestCreatesFilterExpression
{
    [Theory]
    [InlineData(0, false, "")]
    [InlineData(0, true, "(PeriodCoveredByReturn ne null)")]
    [InlineData(1, false, "(URN ne '0')")]
    [InlineData(2, false, "(URN ne '0') and (URN ne '1')")]
    [InlineData(2, true, "(URN ne '0') and (URN ne '1') and (PeriodCoveredByReturn ne null)")]
    public void ShouldReturnExpectedString(int exclusionCount, bool excludeMissingFinancialData, string? expected)
    {
        var request = new SchoolSuggestRequest
        {
            Exclude = Enumerable.Range(0, exclusionCount)
                .Select(i => $"{i}")
                .ToArray(),
            ExcludeMissingFinancialData = excludeMissingFinancialData
        };

        var result = request.FilterExpression();

        Assert.Equal(expected, result);
    }
}