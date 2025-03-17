using Xunit;

namespace Platform.Domain.Tests;

public class WhenRankingChecksIsValid
{
    [Theory]
    [InlineData("SpendAsPercentageOfBudget", true)]
    [InlineData("invalid", false)]
    public void ShouldValidateLocalAuthorityNationalRankings(string ranking, bool expected)
    {
        var actual = Ranking.LocalAuthorityNationalRanking.IsValid(ranking);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Asc", true)]
    [InlineData("desc", true)]
    [InlineData("invalid", false)]
    public void ShouldValidateSort(string sort, bool expected)
    {
        var actual = Ranking.Sort.IsValid(sort);
        Assert.Equal(expected, actual);
    }
}