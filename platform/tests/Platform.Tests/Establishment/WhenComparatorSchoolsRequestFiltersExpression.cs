using Platform.Api.Establishment.Comparators;
using Xunit;
namespace Platform.Tests.Establishment;

public class WhenComparatorSchoolsRequestFiltersExpression
{

    public static TheoryData<ShouldReturnExpressionTestData> TestData =>
    [
        new()
        {
            ExpectedSearchExpression = "*"
        },
        new()
        {
            Request = new ComparatorSchoolsRequest(),
            Urn = "urn",
            ExpectedFilterExpression = "(URN ne 'urn')",
            ExpectedSearchExpression = "*"
        },
        new()
        {
            Request = new ComparatorSchoolsRequest
            {
                TotalPupils = new CharacteristicRange
                {
                    From = 123,
                    To = 456
                }
            },
            Urn = "urn",
            ExpectedFilterExpression = "(URN ne 'urn') and ((TotalPupils ge 123) and (TotalPupils le 456))",
            ExpectedSearchExpression = "*"
        },
        new()
        {
            Request = new ComparatorSchoolsRequest
            {
                IsPFISchool = new CharacteristicValueBool
                {
                    Values = true
                }
            },
            Urn = "urn",
            ExpectedFilterExpression = "(URN ne 'urn') and (IsPFISchool eq true)",
            ExpectedSearchExpression = "*"
        },
        new()
        {
            Request = new ComparatorSchoolsRequest
            {
                FinanceType = new CharacteristicList
                {
                    Values = ["123", "456", "789"]
                }
            },
            Urn = "urn",
            ExpectedFilterExpression = "(URN ne 'urn') and ((FinanceType eq '123') or (FinanceType eq '456') or (FinanceType eq '789'))",
            ExpectedSearchExpression = "*"
        },
        new()
        {
            Request = new ComparatorSchoolsRequest
            {
                OverallPhase = new CharacteristicList
                {
                    Values = ["123", "456", "789"]
                },
                LAName = new CharacteristicList
                {
                    Values = ["This and That"]
                }
            },
            Urn = "urn",
            ExpectedFilterExpression = "(URN ne 'urn') and ((OverallPhase eq '123') or (OverallPhase eq '456') or (OverallPhase eq '789')) and ((LAName eq 'This and That'))",
            ExpectedSearchExpression = "*"
        }
    ];
    [Theory]
    [MemberData(nameof(TestData), MemberType = typeof(WhenComparatorSchoolsRequestFiltersExpression))]
    public void ShouldReturnFilterExpression(ShouldReturnExpressionTestData input)
    {
        var actual = input.Request.FilterExpression(input.Urn);
        Assert.Equal(input.ExpectedFilterExpression, actual);
    }

    [Theory]
    [MemberData(nameof(TestData), MemberType = typeof(WhenComparatorSchoolsRequestFiltersExpression))]
    public void ShouldReturnSearchExpression(ShouldReturnExpressionTestData input)
    {
        var actual = input.Request.SearchExpression();
        Assert.Equal(input.ExpectedSearchExpression, actual);
    }

    public record ShouldReturnExpressionTestData
    {
        public ComparatorSchoolsRequest Request { get; init; } = new();
        public string Urn { get; init; } = string.Empty;
        public string ExpectedFilterExpression { get; init; } = "(URN ne '')";
        public string ExpectedSearchExpression { get; init; } = string.Empty;
    }
}