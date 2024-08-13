using Platform.Api.Insight.Income;
using Xunit;
namespace Platform.Tests.Insight.Income.IncomeResponseFactoryCreate;

public class WhenIncomeResponseFactoryCreatesResponseFromTrustIncome
{
    private readonly TrustIncomeModel _model;

    public WhenIncomeResponseFactoryCreatesResponseFromTrustIncome()
    {
        _model = TestDataReader.ReadTestDataFromFile<TrustIncomeModel>("IncomeTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(TotalIncomeTestData<TrustIncomeResponse>))]
    public void ShouldBuildResponseModelWithTotalIncome(string? category, string dimension, bool excludeCentralServices, IncomeBaseResponse expected)
    {
        var response = IncomeResponseFactory.Create(_model, new IncomeParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertTotalIncome(expected, response);
    }

    [Theory]
    [ClassData(typeof(GrantFundingTestData<TrustIncomeResponse>))]
    public void ShouldBuildResponseModelWithGrantFunding(string? category, string dimension, bool excludeCentralServices, IncomeBaseResponse expected)
    {
        var response = IncomeResponseFactory.Create(_model, new IncomeParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertGrantFunding(expected, response);
    }

    [Theory]
    [ClassData(typeof(SelfGeneratedTestData<TrustIncomeResponse>))]
    public void ShouldBuildResponseModelWithSelfGenerated(string? category, string dimension, bool excludeCentralServices, IncomeBaseResponse expected)
    {
        var response = IncomeResponseFactory.Create(_model, new IncomeParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertSelfGenerated(expected, response);
    }

    [Theory]
    [ClassData(typeof(DirectRevenueFinancingTestData<TrustIncomeResponse>))]
    public void ShouldBuildResponseModelWithDirectRevenueFinancing(string? category, string dimension, bool excludeCentralServices, IncomeBaseResponse expected)
    {
        var response = IncomeResponseFactory.Create(_model, new IncomeParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertDirectRevenueFinancing(expected, response);
    }
}