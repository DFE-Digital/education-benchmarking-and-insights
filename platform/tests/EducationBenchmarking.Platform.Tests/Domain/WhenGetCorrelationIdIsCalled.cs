using EducationBenchmarking.Platform.Domain.Extensions;
using FluentAssertions;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Domain;

public class WhenGetGetDecimalValueByNameIsCalled
{
    [Fact]
    public void ShouldReturnValue()
    {
        var data = new TestData { Foo = decimal.MaxValue };

        var x = data.DecimalValueByName<TestData>("Foo");
        x.Should().NotBeNull();
        x.Should().Be(data.Foo);
    }

    [Fact]
    public void ShouldReturnNullWhenNotKnown()
    {
        var data = new TestData { Foo = decimal.MaxValue };

        var x = data.DecimalValueByName<TestData>("Bar");
        x.Should().BeNull();
    }
    
    [Fact]
    public void ShouldReturnNullWhenNotSet()
    {
        var data = new TestData { Foo = decimal.MaxValue };

        var x = data.DecimalValueByName<TestData>(null);
        x.Should().BeNull();
    }
    
    private class TestData
    {
        public decimal Foo { get; init; }
    }
    
}

