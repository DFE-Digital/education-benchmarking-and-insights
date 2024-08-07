using Platform.Api.Insight.Balance;
using Xunit;
namespace Platform.Tests.Insight.Balance.BalanceResponseFactoryCreate;

public class WhenBalanceResponseFactoryCreatesResponseFromSchoolBalanceHistory
{
    private readonly SchoolBalanceHistoryModel _model;

    public WhenBalanceResponseFactoryCreatesResponseFromSchoolBalanceHistory()
    {
        _model = TestDataReader.ReadTestDataFromFile<SchoolBalanceHistoryModel>("BalanceTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(TotalBalanceTestData<SchoolBalanceHistoryResponse>))]
    public void ShouldBuildResponseModel(string dimension, bool excludeCentralServices, BalanceBaseResponse expected)
    {
        var response = BalanceResponseFactory.Create(_model, new BalanceParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Dimension = dimension
        });

        Assertions.AssertBalance(expected, response);
        Assertions.AssertRevenueReserve(expected, response);
    }
}