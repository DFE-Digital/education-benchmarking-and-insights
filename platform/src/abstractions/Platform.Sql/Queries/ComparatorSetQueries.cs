using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetDefaultComparatorSet(string? urn, string? runId)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * FROM ComparatorSet WHERE RunType = 'default' AND RunId = @RunId AND URN = @URN",
            new { URN = urn, RunId = runId });

        return template;
    }
    
    public static SqlBuilder.Template GetCustomComparatorSet(string? urn, string? runId)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * FROM ComparatorSet WHERE RunType = 'custom' AND RunId = @RunId AND URN = @URN",
            new { URN = urn, RunId = runId });

        return template;
    }
    
    public static SqlBuilder.Template GetUserDefinedSchoolComparatorSet(string? urn, string? runId, string? runType = "default")
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * from UserDefinedSchoolComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType",
            new { URN = urn, RunId = runId, RunType = runType });

        return template;
    }
    
    public static SqlBuilder.Template GetUserDefinedTrustComparatorSet(string? companyNumber, string? runId, string? runType = "default")
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * from UserDefinedTrustComparatorSet where CompanyNumber = @CompanyNumber AND RunId = @RunId AND RunType = @RunType",
            new { CompanyNumber = companyNumber, RunId = runId, RunType = runType });

        return template;
    }
}