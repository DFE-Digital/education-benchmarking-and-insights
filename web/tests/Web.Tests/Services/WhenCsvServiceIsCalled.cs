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
    public void ShouldReturnCsvStringForValidCollectionWithExcludedColumns()
    {
        var items = new List<AnotherTestObject>
        {
            new()
            {
                Id = 1,
                Value = "Value",
                Ignore = "Ignore 1",
                AnotherIgnore = "AnotherIgnore 1"
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

        var result = _service.SaveToCsv(items, nameof(AnotherTestObject.Ignore), nameof(AnotherTestObject.AnotherIgnore));

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
            new()
            {
                Id = 3
            },
            null
        };

        var result = _service.SaveToCsv(items);

        var expected = $"Id,Value{Environment.NewLine}1,Value{Environment.NewLine}2,Another value{Environment.NewLine}2,\"Value, with a comma\"{Environment.NewLine}3,";
        Assert.Equal(expected, result);
    }

    private class TestObject
    {
        public int Id { get; set; }
        public string? Value { get; set; }
    }

    private class AnotherTestObject : TestObject
    {
        public string? Ignore { get; set; }
        public string? AnotherIgnore { get; set; }
    }
}