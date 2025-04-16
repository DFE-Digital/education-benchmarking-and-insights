using Platform.Api.Establishment.Features.Trusts.Requests;
using Xunit;

namespace Platform.Establishment.Tests.Trusts.Requests;

public class WhenTrustSuggestRequestCreatesFilterExpression
{
    [Theory]
    [InlineData(0, "")]
    [InlineData(1, "(CompanyNumber ne '0')")]
    [InlineData(2, "(CompanyNumber ne '0') and (CompanyNumber ne '1')")]
    public void ShouldReturnExpectedString(int exclusionCount, string? expected)
    {
        var request = new TrustSuggestRequest
        {
            Exclude = Enumerable.Range(0, exclusionCount)
                .Select(i => $"{i}")
                .ToArray(),
        };

        var result = request.FilterExpression();

        Assert.Equal(expected, result);
    }
}