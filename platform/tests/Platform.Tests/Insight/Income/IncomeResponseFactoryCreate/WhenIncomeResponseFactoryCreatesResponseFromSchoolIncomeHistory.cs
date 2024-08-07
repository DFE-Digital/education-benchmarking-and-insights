using Platform.Api.Insight.Income;
using Xunit;
namespace Platform.Tests.Insight.Income.IncomeResponseFactoryCreate;

public class WhenIncomeResponseFactoryCreatesResponseFromSchoolIncomeHistory
{
    private readonly SchoolIncomeHistoryModel _model;

    public WhenIncomeResponseFactoryCreatesResponseFromSchoolIncomeHistory()
    {
        _model = TestDataReader.ReadTestDataFromFile<SchoolIncomeHistoryModel>("IncomeTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(TotalIncomeTestData<SchoolIncomeHistoryResponse>))]
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
    [ClassData(typeof(GrantFundingTestData<SchoolIncomeHistoryResponse>))]
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
    [ClassData(typeof(SelfGeneratedTestData<SchoolIncomeHistoryResponse>))]
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
    [ClassData(typeof(DirectRevenueFinancingTestData<SchoolIncomeHistoryResponse>))]
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