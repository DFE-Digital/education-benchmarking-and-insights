using Platform.Api.Insight.Balance;
using Xunit;
namespace Platform.Tests.Insight.Balance.BalanceResponseFactoryCreate;

public static class Assertions
{
    internal static void AssertBalance(BalanceBaseResponse expected, BalanceBaseResponse response)
    {
        AssertEqual(nameof(BalanceBaseResponse.InYearBalance), expected.InYearBalance, response.InYearBalance);
        AssertEqual(nameof(BalanceBaseResponse.SchoolInYearBalance), expected.SchoolInYearBalance, response.SchoolInYearBalance);
        AssertEqual(nameof(BalanceBaseResponse.CentralInYearBalance), expected.CentralInYearBalance, response.CentralInYearBalance);
    }

    internal static void AssertRevenueReserve(BalanceBaseResponse expected, BalanceBaseResponse response)
    {
        AssertEqual(nameof(BalanceBaseResponse.RevenueReserve), expected.RevenueReserve, response.RevenueReserve);
        AssertEqual(nameof(BalanceBaseResponse.SchoolRevenueReserve), expected.SchoolRevenueReserve, response.SchoolRevenueReserve);
        AssertEqual(nameof(BalanceBaseResponse.CentralRevenueReserve), expected.CentralRevenueReserve, response.CentralRevenueReserve);
    }

    private static void AssertEqual(string field, decimal? expected, decimal? actual) =>
        Assert.True(
            Math.Abs(expected.GetValueOrDefault() - actual.GetValueOrDefault()) < 0.02m,
            $"Expected `{expected}` for {field} but got `{actual}`");
}