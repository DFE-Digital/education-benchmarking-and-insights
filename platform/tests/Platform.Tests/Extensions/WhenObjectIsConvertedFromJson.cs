using System.Text;
using Newtonsoft.Json;
using Platform.Functions.Extensions;
using Xunit;
// ReSharper disable UseCollectionExpression
namespace Platform.Tests.Extensions;

public class WhenObjectIsConvertedFromJson
{
    public static TheoryData<ShouldReturnObjectFromStringTestData> ShouldReturnObjectFromStringTestDataItems =>
        new()
        {
            new ShouldReturnObjectFromStringTestData
            {
                Source = "{\"testProp\":\"testProp\"}",
                Expected = new TestObjectType("testProp")
            },
            new ShouldReturnObjectFromStringTestData
            {
                Source = string.Empty,
                ThrowsExceptionType = typeof(ArgumentNullException)
            },
            new ShouldReturnObjectFromStringTestData
            {
                Source = null,
                ThrowsExceptionType = typeof(ArgumentNullException)
            }
        };

    public static TheoryData<ShouldReturnObjectFromByteArrayTestData> ShouldReturnObjectFromByteArrayTestDataItems =>
        new()
        {
            new ShouldReturnObjectFromByteArrayTestData
            {
                Source = "{\"testProp\":\"testProp\"}"u8.ToArray(),
                Expected = new TestObjectType("testProp")
            },
            new ShouldReturnObjectFromByteArrayTestData
            {
                Source = [],
                ThrowsExceptionType = typeof(ArgumentException)
            },
            new ShouldReturnObjectFromByteArrayTestData
            {
                Source = null,
                ThrowsExceptionType = typeof(ArgumentException)
            },
            new ShouldReturnObjectFromByteArrayTestData
            {
                Source = " "u8.ToArray(),
                ThrowsExceptionType = typeof(ArgumentNullException)
            },
            new ShouldReturnObjectFromByteArrayTestData
            {
                Source = "[]"u8.ToArray(),
                ThrowsExceptionType = typeof(JsonSerializationException)
            },
            new ShouldReturnObjectFromByteArrayTestData
            {
                Source = Encoding.UTF32.GetBytes("{\"testProp\":\"testProp\"}"),
                Expected = new TestObjectType(null!)
            },
            new ShouldReturnObjectFromByteArrayTestData
            {
                Source = Encoding.UTF32.GetBytes("{\"testProp\":\"testProp\"}"),
                Encoding = Encoding.UTF32,
                Expected = new TestObjectType("testProp")
            }
        };

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromStringTestDataItems), MemberType = typeof(WhenObjectIsConvertedFromJson))]
    public void ShouldReturnObjectFromString(ShouldReturnObjectFromStringTestData input)
    {
        if (input.ThrowsExceptionType != null)
        {
            Assert.Throws(input.ThrowsExceptionType, () => input.Source.FromJson<TestObjectType>());
            return;
        }

        var actual = input.Source.FromJson<TestObjectType>();
        Assert.Equivalent(input.Expected, actual);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromByteArrayTestDataItems), MemberType = typeof(WhenObjectIsConvertedFromJson))]
    public void ShouldReturnObjectFromByteArray(ShouldReturnObjectFromByteArrayTestData input)
    {
        if (input.ThrowsExceptionType != null)
        {
            Assert.Throws(input.ThrowsExceptionType, () => input.Source.FromJson<TestObjectType>(input.Encoding));
            return;
        }

        var actual = input.Source.FromJson<TestObjectType>(input.Encoding);
        Assert.Equivalent(input.Expected, actual);
    }

    public record ShouldReturnObjectFromStringTestData
    {
        public string? Source { get; init; }
        public TestObjectType? Expected { get; init; }
        public Type? ThrowsExceptionType { get; init; }
    }

    public record ShouldReturnObjectFromByteArrayTestData
    {
        public byte[]? Source { get; init; }
        public Encoding? Encoding { get; init; }
        public TestObjectType? Expected { get; init; }
        public Type? ThrowsExceptionType { get; init; }
    }
}