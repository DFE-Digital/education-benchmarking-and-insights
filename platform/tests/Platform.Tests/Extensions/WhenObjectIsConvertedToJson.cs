using Newtonsoft.Json;
using Platform.Functions.Extensions;
using Xunit;
// ReSharper disable UseCollectionExpression
namespace Platform.Tests.Extensions;

public class WhenObjectIsConvertedToJson
{
    [Theory]
    [MemberData(nameof(Data), MemberType = typeof(WhenObjectIsConvertedToJson))]
    public void ShouldReturnJsonString(ShouldReturnJsonStringTestData input)
    {
        const string testProp = nameof(testProp);
        var value = new TestObjectType(testProp);

        var actual = input.Formatting == null ? value.ToJson() : value.ToJson(input.Formatting.Value);
        Assert.Equal(input.Expected, actual);
    }

    public static TheoryData<ShouldReturnJsonStringTestData> Data =>
        new()
        {
            new ShouldReturnJsonStringTestData
            {
                Expected = "{\"testProp\":\"testProp\"}"
            },
            new ShouldReturnJsonStringTestData
            {
                Formatting = Formatting.None,
                Expected = "{\"testProp\":\"testProp\"}"
            },
            new ShouldReturnJsonStringTestData
            {
                Formatting = Formatting.Indented,
                Expected = $"{{{Environment.NewLine}  \"testProp\": \"testProp\"{Environment.NewLine}}}"
            }
        };

    public record ShouldReturnJsonStringTestData
    {
        public Formatting? Formatting { get; init; }
        public string? Expected { get; init; }
    }
}