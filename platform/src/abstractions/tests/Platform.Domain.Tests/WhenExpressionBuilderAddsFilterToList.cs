using Xunit;

namespace Platform.Domain.Tests;

public class WhenExpressionBuilderAddsFilterToList
{
    private const string Default = nameof(Default);
    private readonly List<string> _list = [Default];

    public static TheoryData<bool?, Func<List<string>, List<string>>?, Func<List<string>, List<string>>?, string> ConditionallyAddExpressionToListData => new()
    {
        {
            true, x =>
            {
                x.Add("(field eq 'true')");
                return x;
            },
            x =>
            {
                x.Add("(field eq 'false')");
                return x;
            },
            $"{Default} (field eq 'true')"
        },
        { true, null, null, Default },
        {
            false, x =>
            {
                x.Add("(field eq 'true')");
                return x;
            },
            x =>
            {
                x.Add("(field eq 'false')");
                return x;
            },
            $"{Default} (field eq 'false')"
        },
        { false, null, null, Default },
        {
            null, x =>
            {
                x.Add("(field eq 'true')");
                return x;
            },
            x =>
            {
                x.Add("(field eq 'false')");
                return x;
            },
            Default
        }
    };

    [Theory]
    [InlineData("field", "value", $"{Default} (field ne 'value')")]
    [InlineData("field", null, Default)]
    public void ShouldAddNotExpressionToList(string fieldName, string? value, string expected)
    {
        _list.NotValueFilter(fieldName, value);
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", "123.45", "678.9", $"{Default} ((field ge 123.45) and (field le 678.9))")]
    [InlineData("field", null, null, Default)]
    public void ShouldAddDecimalRangeToList(string fieldName, string? from, string? to, string expected)
    {
        _list.RangeFilter(fieldName, from == null || to == null
            ? null
            : new CharacteristicRange
            {
                From = decimal.Parse(from),
                To = decimal.Parse(to)
            });
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", "2024-03-01T00:00:00.00000", "2024-06-30T23:59:59.99999", $"{Default} ((field ge 2024-03-01T00:00:00Z) and (field le 2024-06-30T23:59:59Z))")]
    [InlineData("field", null, null, Default)]
    public void ShouldAddDateRangeToList(string fieldName, string? from, string? to, string expected)
    {
        _list.RangeFilter(fieldName, from == null || to == null
            ? null
            : new CharacteristicDateRange
            {
                From = DateTime.Parse(from),
                To = DateTime.Parse(to)
            });
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", true, $"{Default} (field eq true)")]
    [InlineData("field", false, $"{Default} (field eq false)")]
    [InlineData("field", null, Default)]
    public void ShouldAddBooleanToList(string fieldName, bool? value, string expected)
    {
        _list.RangeFilter(fieldName, value == null
            ? null
            : new CharacteristicValueBool
            {
                Values = value.Value
            });
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", "item1,item2,item3", $"{Default} field:(\"item1\" OR \"item2\" OR \"item3\")")]
    [InlineData("field", null, Default)]
    public void ShouldAddSearchToList(string fieldName, string? value, string expected)
    {
        _list.ListSearch(fieldName, value == null
            ? null
            : new CharacteristicList
            {
                Values = value.Split(",")
            });
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", "item1,item2,item3", $"{Default} ((field eq 'item1') or (field eq 'item2') or (field eq 'item3'))")]
    [InlineData("field", null, Default)]
    public void ShouldAddValuesFilterToList(string fieldName, string? value, string expected)
    {
        _list.ValuesFilter(fieldName, value == null
            ? null
            : new CharacteristicList
            {
                Values = value.Split(",")
            });
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", "item1,item2,item3", $"{Default} (field ne 'item1') and (field ne 'item2') and (field ne 'item3')")]
    [InlineData("field", null, Default)]
    public void ShouldAddNotValuesFilterToList(string fieldName, string? value, string expected)
    {
        _list.NotValuesFilter(fieldName, value == null
            ? null
            : new CharacteristicList
            {
                Values = value.Split(",")
            });
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", $"{Default} (field eq null)")]
    public void ShouldAddNullExpressionToList(string fieldName, string expected)
    {
        _list.NullValueFilter(fieldName);
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", $"{Default} (field ne null)")]
    public void ShouldAddNotNullExpressionToList(string fieldName, string expected)
    {
        _list.NotNullValueFilter(fieldName);
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [InlineData("field", true, "eq 'true'", "eq 'false'", $"{Default} (field eq 'true')")]
    [InlineData("field", true, "", "eq 'false'", Default)]
    [InlineData("field", false, "eq 'true'", "eq 'false'", $"{Default} (field eq 'false')")]
    [InlineData("field", false, "eq 'true'", "", Default)]
    [InlineData("field", null, "eq 'true'", "eq 'false'", Default)]
    public void ShouldConditionallyAddValueToList(string fieldName, bool? condition, string expressionIfTrue, string? expressionIfFalse, string expected)
    {
        _list.ConditionalValueFilter(fieldName, condition, expressionIfTrue, expressionIfFalse);
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Theory]
    [MemberData(nameof(ConditionallyAddExpressionToListData))]
    public void ShouldConditionallyAddExpressionToList(bool? condition, Func<List<string>, List<string>>? expressionIfTrue, Func<List<string>, List<string>>? expressionIfFalse, string expected)
    {
        _list.ConditionalExpressionFilter(condition, expressionIfTrue, expressionIfFalse);
        Assert.Equal(expected, string.Join(" ", _list));
    }

    [Fact]
    public void ShouldBuildFilter()
    {
        _list.AddRange(["filter1", "filter2", "filter3"]);
        var actual = _list.BuildFilter();
        Assert.Equal($"{Default} and filter1 and filter2 and filter3", actual);
    }

    [Fact]
    public void ShouldBuildSearch()
    {
        _list.AddRange(["search1", "search2", "search3"]);
        var actual = _list.BuildSearch();
        Assert.Equal($"{Default} AND search1 AND search2 AND search3", actual);
    }
}