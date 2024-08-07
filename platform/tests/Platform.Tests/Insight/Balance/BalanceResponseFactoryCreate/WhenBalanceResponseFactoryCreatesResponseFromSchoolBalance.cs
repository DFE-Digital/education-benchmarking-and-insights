using Platform.Api.Insight.Balance;
using Xunit;
namespace Platform.Tests.Insight.Balance.BalanceResponseFactoryCreate;

public class WhenBalanceResponseFactoryCreatesResponseFromSchoolBalance
{
    private readonly SchoolBalanceModel _model;

    public WhenBalanceResponseFactoryCreatesResponseFromSchoolBalance()
    {
        _model = TestDataReader.ReadTestDataFromFile<SchoolBalanceModel>("BalanceTestData.json", GetType());
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