using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class ParametersQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        const string name = nameof(name);
        var builder = new ParametersQuery(name);

        const string expectedSql = "SELECT Value FROM Parameters WHERE Name = @Name\n";
        var expectedParams = new Dictionary<string, object>
        {
            { "Name", name }
        };

        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
        Assert.Equal(expectedParams, builder.QueryTemplate.Parameters?.GetTemplateParameters("Name"));
    }
}