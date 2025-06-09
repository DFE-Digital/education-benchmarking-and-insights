using AutoFixture;
using Web.App.Domain;
using Web.App.ViewComponents;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewModels.Components;

public class GivenASchoolSpendingCostsViewModelCostCategories
{
    private readonly Fixture _fixture = new();

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShouldAddFilteredCostsToList(bool hasNegativeOrZeroValues)
    {
        const string urn = nameof(urn);
        var rating = _fixture.Create<RagRating>();
        var urns = _fixture.Build<string>().CreateMany().ToList();
        urns.Add(urn);

        var administrativeSupplies = new AdministrativeSupplies(rating);
        var cateringStaffServices = new CateringStaffServices(rating);
        var educationalIct = new EducationalIct(rating);
        foreach (var u in urns)
        {
            var expenditure = _fixture.Create<SchoolExpenditure>();
            if (hasNegativeOrZeroValues || u == urn)
            {
                expenditure.AdministrativeSuppliesNonEducationalCosts = -1;
            }

            administrativeSupplies.Add(u, expenditure);
            cateringStaffServices.Add(u, _fixture.Create<SchoolExpenditure>());
            educationalIct.Add(u, _fixture.Create<SchoolExpenditure>());
        }

        CostCategory[] costs = [administrativeSupplies, cateringStaffServices, educationalIct];

        var categories = new SchoolSpendingCostsViewModelCostCategories(urn, costs);
        Assert.NotEmpty(categories);
        Assert.Equal(costs.Length, categories.Count);

        var actualAdministrativeSupplies = categories.ElementAt(0);
        Assert.NotNull(actualAdministrativeSupplies.Uuid);
        Assert.Equal(administrativeSupplies, actualAdministrativeSupplies.Category);
        Assert.Null(actualAdministrativeSupplies.ChartSvg);
        Assert.Equal(hasNegativeOrZeroValues, actualAdministrativeSupplies.HasNegativeOrZeroValues);
        var expectedData = administrativeSupplies.Values
            .Where(v => !hasNegativeOrZeroValues || v.Key == urn)
            .Select(x => new PriorityCostCategoryDatum { Urn = x.Key, Amount = x.Value.Value });
        Assert.Equivalent(expectedData, actualAdministrativeSupplies.Data);

        var actualCateringStaffServices = categories.ElementAt(1);
        Assert.NotNull(actualCateringStaffServices.Uuid);
        Assert.Equal(cateringStaffServices, actualCateringStaffServices.Category);
        Assert.Null(actualCateringStaffServices.ChartSvg);
        Assert.False(actualCateringStaffServices.HasNegativeOrZeroValues);
        Assert.Equivalent(cateringStaffServices.Values.Select(x => new PriorityCostCategoryDatum { Urn = x.Key, Amount = x.Value.Value }), actualCateringStaffServices.Data);

        var actualEducationalIct = categories.ElementAt(2);
        Assert.NotNull(actualEducationalIct.Uuid);
        Assert.Equal(educationalIct, actualEducationalIct.Category);
        Assert.Null(actualEducationalIct.ChartSvg);
        Assert.False(actualEducationalIct.HasNegativeOrZeroValues);
        Assert.Equivalent(educationalIct.Values.Select(x => new PriorityCostCategoryDatum { Urn = x.Key, Amount = x.Value.Value }), actualEducationalIct.Data);

    }
}