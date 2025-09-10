using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenATrustPlanningViewModel
{
    private readonly IEnumerable<FinancialPlan> _completePlans;
    private readonly Fixture _fixture = new();
    private readonly IEnumerable<FinancialPlan> _incompletePlans;
    private readonly Trust _trust;

    public GivenATrustPlanningViewModel()
    {
        _trust = _fixture.Create<Trust>();
        _incompletePlans = _fixture.Build<FinancialPlan>().With(p => p.IsComplete, false).CreateMany();
        _completePlans = _fixture.Build<FinancialPlan>().With(p => p.IsComplete, true).CreateMany();
    }

    [Fact]
    public void WhenContainsCompletedPlans()
    {
        var result = new TrustPlanningViewModel(_trust, _completePlans.Concat(_incompletePlans).ToArray());

        Assert.Equal(_trust.CompanyNumber, result.CompanyNumber);
        Assert.Equal(_trust.TrustName, result.Name);
        Assert.Equal(_trust.Schools, result.Schools);
        Assert.Equal(_completePlans, result.Plans);
    }
}