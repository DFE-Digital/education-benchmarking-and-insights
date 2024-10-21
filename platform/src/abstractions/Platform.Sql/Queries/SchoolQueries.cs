using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetSchool(string? urn)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * FROM School WHERE URN = @URN", 
            new { URN = urn });
        
        return template;
    }
    
    public static SqlBuilder.Template GetFederationSchools(string? urn)
    {
    
        
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * FROM School WHERE FederationLeadURN = @URN", 
            new { URN = urn });
        
        return template;
    }
    
    public static SqlBuilder.Template GetSchools(string? companyNumber, string? laCode, string? phase)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * FROM School /**where**/");

        if (!string.IsNullOrEmpty(companyNumber))
        {
            builder.Where(
                "TrustCompanyNumber = @CompanyNumber AND FinanceType = 'Academy'", 
                new { companyNumber });
        }

        if (!string.IsNullOrEmpty(laCode))
        {
            builder.Where(
                "LaCode = @LaCode AND FinanceType = 'Maintained'", 
                new { laCode });
        }

        if (!string.IsNullOrEmpty(phase))
        {
            builder.Where(
                "OverallPhase = @phase",
                new { phase });
        }
        
        return template;
    }
}