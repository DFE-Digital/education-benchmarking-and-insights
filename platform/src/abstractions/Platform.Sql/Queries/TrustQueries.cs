using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetTrust(string? companyNumber)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * FROM Trust WHERE CompanyNumber = @CompanyNumber", 
            new { CompanyNumber = companyNumber });
        
        return template;
    }
    
    public static SqlBuilder.Template GetTrustSchools(string? companyNumber)
    {
        var subQuery = GetCurrentFinancial("URN");
        
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            $"SELECT URN, SchoolName, OverallPhase FROM School WHERE TrustCompanyNumber = @CompanyNumber AND FinanceType = 'Academy' AND URN IN ({subQuery.RawSql})", 
            new { CompanyNumber = companyNumber });
        
        return template;
    }
}