using Web.App.Services;
using Xunit;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Web.Tests.Services;

public class WhenCsvServiceIsCalled
{
    private readonly CsvService _service = new();

    [Fact]
    public void ShouldReturnEmptyStringForEmptyCollection()
    {
        var items = Array.Empty<TestObject>();

        var result = _service.SaveToCsv(items);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ShouldReturnCsvStringForValidCollection()
    {
        var items = new List<TestObject>
        {
            new()
            {
                Id = 1,
                Value = "Value"
            },
            new()
            {
                Id = 2,
                Value = "Another value"
            },
            new()
            {
                Id = 2,
                Value = "Value, with a comma"
            }
        };

        var result = _service.SaveToCsv(items);

        var expected = $"Id,Value{Environment.NewLine}1,Value{Environment.NewLine}2,Another value{Environment.NewLine}2,\"Value, with a comma\"";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldReturnCsvStringForValidCollectionContainingNulls()
    {
        var items = new List<TestObject?>
        {
            null,
            new()
            {
                Id = 1,
                Value = "Value"
            },
            new()
            {
                Id = 2,
                Value = "Another value"
            },
            new()
            {
                Id = 2,
                Value = "Value, with a comma"
            },
            null
        };

        var result = _service.SaveToCsv(items);

        var expected = $"Id,Value{Environment.NewLine}1,Value{Environment.NewLine}2,Another value{Environment.NewLine}2,\"Value, with a comma\"";
        Assert.Equal(expected, result);
    }

    private class TestObject
    {
        public int Id { get; set; }
        public string? Value { get; set; }
    }
}