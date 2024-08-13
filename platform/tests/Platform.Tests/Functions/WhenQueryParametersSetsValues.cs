using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Platform.Functions;
using Xunit;
namespace Platform.Tests.Functions;

public class WhenQueryParametersSetsValues
{
    [Fact]
    public void ShouldAssignValuesFromNameValueCollectionIntoIQueryCollection()
    {
        // arrange
        var parameters = new TestQueryParameters();
        var query = new NameValueCollection
        {
            {
                "name1", "value1"
            },
            {
                "name2", "value2"
            },
            {
                "name2", "value3"
            },
            {
                "name3", null
            },
            {
                null, "value4"
            }
        };

        // act
        parameters.SetValues(query);

        // assert
        var expected = new QueryCollection(new Dictionary<string, StringValues>
        {
            {
                "name1", "value1"
            },
            {
                "name2", new StringValues(["value2", "value3"])
            }
        });
        Assert.Equal(expected, parameters.Actual);
    }
}

public record TestQueryParameters : QueryParameters
{
    public IQueryCollection? Actual { get; private set; }

    public override void SetValues(IQueryCollection query)
    {
        Actual = query;
    }
}