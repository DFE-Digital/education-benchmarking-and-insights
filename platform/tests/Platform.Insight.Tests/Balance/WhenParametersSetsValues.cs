using System.Collections.Specialized;
using Platform.Api.Insight.Features.Balance;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Balance;

public class WhenBalanceParametersSetsValues
{
    [Theory]
    [MemberData(nameof(Data))]
    public void ShouldSetValuesFromQuery(string? dimension)
    {
        var values = new NameValueCollection
        {
            { "dimension", dimension }
        };

        var parameters = new BalanceParameters();
        parameters.SetValues(values);

        Assert.Equal(dimension, parameters.Dimension);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();


        var parameters = new BalanceParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
    }

    public static TheoryData<string> Data =>
    [
        "Actuals",
        "PercentExpenditure",
        "PercentIncome",
        "PerUnit"
    ];
}

public class WhenBalanceQueryTrustParametersSetsValues
{
    [Theory]
    [MemberData(nameof(Data))]
    public void ShouldSetValuesFromQuery(string? dimension, string? companyNumbers, string[] trusts)
    {
        var values = new NameValueCollection
        {
            { "dimension", dimension },
            { "companyNumbers", companyNumbers }
        };

        var parameters = new BalanceQueryTrustsParameters();
        parameters.SetValues(values);

        Assert.Equal(dimension, parameters.Dimension);
        Assert.Equal(trusts, parameters.Trusts);
    }

    [Fact]
    public void ShouldSetValuesDefaultFromQuery()
    {
        var values = new NameValueCollection();

        var parameters = new BalanceQueryTrustsParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Equal([], parameters.Trusts);
    }

    public static TheoryData<string, string?, string[]> Data => new()
    {
        { "Actuals", "1,2,3", ["1","2","3"] },
        { "PercentExpenditure", "1,2,3,4", ["1","2","3","4"] },
        { "PercentIncome", "101,202,303,102,203,304", ["101","202","303","102","203","304"] },
        { "PerUnit", null, [] },
    };
}