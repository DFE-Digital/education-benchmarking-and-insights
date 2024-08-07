using Platform.Api.Insight.Balance;
using Xunit;
namespace Platform.Tests.Insight.Balance.BalanceResponseFactoryCreate;

public class WhenBalanceResponseFactoryCreatesResponseFromTrustBalanceHistory
{
    private readonly TrustBalanceHistoryModel _model;

    public WhenBalanceResponseFactoryCreatesResponseFromTrustBalanceHistory()
    {
        _model = TestDataReader.ReadTestDataFromFile<TrustBalanceHistoryModel>("BalanceTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(TotalBalanceTestData<SchoolBalanceResponse>))]
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