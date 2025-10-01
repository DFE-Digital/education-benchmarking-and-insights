using AutoFixture;
using Microsoft.Azure.Cosmos.Linq;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenATrustComparisonItSpendViewModel
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldReturnValuesAndFilters()
    {
        var trust = _fixture.Create<Trust>();
        const bool comparatorGenerated = true;
        const string redirectUri = nameof(redirectUri);
        var userDefinedSet = _fixture.CreateMany<string>().ToArray();
        var expenditures = _fixture.Build<TrustItSpend>().CreateMany().ToArray();
        var subCategories = new TrustComparisonSubCategoriesViewModel(string.Empty, expenditures, []);
        var currentBfrYear = _fixture.Create<int>();

        var actual = new TrustComparisonItSpendViewModel(trust, comparatorGenerated, redirectUri, userDefinedSet, subCategories, currentBfrYear);

        Assert.Equal(trust.CompanyNumber, actual.CompanyNumber);
        Assert.Equal(trust.TrustName, actual.Name);
        Assert.Equal(comparatorGenerated, actual.ComparatorGenerated);
        Assert.Equal(redirectUri, actual.RedirectUri);
        Assert.Equal(userDefinedSet, actual.UserDefinedSet);
        Assert.Equal(subCategories.Items, actual.SubCategories);
        Assert.Equal(currentBfrYear, actual.CurrentBfrYear);
        Assert.Equal(Views.ViewAsOptions.Chart, actual.ViewAs);
        Assert.Equal(Dimensions.ResultAsOptions.Actuals, actual.ResultAs);
        Assert.Empty(actual.SelectedSubCategories);
    }
}