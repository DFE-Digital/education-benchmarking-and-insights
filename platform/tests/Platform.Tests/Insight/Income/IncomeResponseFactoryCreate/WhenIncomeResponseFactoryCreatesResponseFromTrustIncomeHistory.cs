using Platform.Api.Insight.Income;
using Xunit;
namespace Platform.Tests.Insight.Income.IncomeResponseFactoryCreate;

public class WhenIncomeResponseFactoryCreatesResponseFromTrustIncomeHistory
{
    private readonly TrustIncomeHistoryModel _model;

    public WhenIncomeResponseFactoryCreatesResponseFromTrustIncomeHistory()
    {
        _model = TestDataReader.ReadTestDataFromFile<TrustIncomeHistoryModel>("IncomeTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(TotalIncomeTestData<TrustIncomeHistoryResponse>))]
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
    [ClassData(typeof(GrantFundingTestData<TrustIncomeHistoryResponse>))]
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
    [ClassData(typeof(SelfGeneratedTestData<TrustIncomeHistoryResponse>))]
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
    [ClassData(typeof(DirectRevenueFinancingTestData<TrustIncomeHistoryResponse>))]
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