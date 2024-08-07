using Platform.Api.Insight.Income;
using Xunit;
namespace Platform.Tests.Insight.Income.IncomeResponseFactoryCreate;

public class WhenIncomeResponseFactoryCreatesResponseFromSchoolIncome
{
    private readonly SchoolIncomeModel _model;

    public WhenIncomeResponseFactoryCreatesResponseFromSchoolIncome()
    {
        _model = TestDataReader.ReadTestDataFromFile<SchoolIncomeModel>("IncomeTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(TotalIncomeTestData<SchoolIncomeResponse>))]
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
    [ClassData(typeof(GrantFundingTestData<SchoolIncomeResponse>))]
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
    [ClassData(typeof(SelfGeneratedTestData<SchoolIncomeResponse>))]
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
    [ClassData(typeof(DirectRevenueFinancingTestData<SchoolIncomeResponse>))]
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