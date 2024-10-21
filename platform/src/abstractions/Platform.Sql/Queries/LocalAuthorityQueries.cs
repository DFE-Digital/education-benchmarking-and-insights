using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetLocalAuthority(string? code)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * FROM LocalAuthority WHERE Code = @Code", 
            new { Code = code });
        
        return template;
    }
    
    public static SqlBuilder.Template GetLocalAuthoritySchools(string? code)
    {
        var subQuery = GetCurrentFinancial("URN");
        
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            $"SELECT URN, SchoolName, OverallPhase FROM School WHERE LaCode = @Code AND FinanceType = 'Maintained' AND URN IN ({subQuery.RawSql})", 
            new { Code = code });
        
        return template;
    }
}