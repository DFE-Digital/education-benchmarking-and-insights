using Dapper;

namespace Platform.Sql.QueryBuilders;

public abstract class PlatformQuery : SqlBuilder
{
    public Template QueryTemplate { get; }

    protected PlatformQuery(string sql, dynamic? parameters = null)
    {
        QueryTemplate = AddTemplate(sql, parameters);
    }

    public PlatformQuery WhereUrnEqual(string urn)
    {
        const string sql = "URN = @URN";
        var parameters = new { URN = urn };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereUrnIn(string[] urns)
    {
        const string sql = "URN IN @URNS";
        var parameters = new { URNS = urns };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereRunIdBetween(int start, int end)
    {
        const string sql = "RunId BETWEEN @StartYear AND @EndYear";
        var parameters = new { StartYear = start, EndYear = end };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereRunIdEqual(string runId)
    {
        const string sql = "RunId = @RunId";
        var parameters = new { RunId = runId };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereTrustCompanyNumberEqual(string companyNumber)
    {
        const string sql = "TrustCompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereCompanyNumberEqual(string companyNumber)
    {
        const string sql = "CompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereCompanyNumberIn(string[] companyNumbers)
    {
        const string sql = "CompanyNumber IN @CompanyNumbers";
        var parameters = new { CompanyNumbers = companyNumbers };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereLaCodeEqual(string laCode)
    {
        const string sql = "LaCode = @LaCode";
        var parameters = new { LaCode = laCode };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereCodeEqual(string code)
    {
        const string sql = "Code = @Code";
        var parameters = new { Code = code };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereOverallPhaseEqual(string? phase)
    {
        const string sql = "OverallPhase = @Phase";
        var parameters = new { Phase = phase };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereFinanceTypeEqual(string? financeType)
    {
        const string sql = "FinanceType = @FinanceType";
        var parameters = new { FinanceType = financeType };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereUrnInCurrentFinances()
    {
        const string sql = "URN IN (SELECT URN FROM Financial WHERE RunType = 'default' AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear'))";

        Where(sql);
        return this;
    }

    public PlatformQuery WhereFederationLeadUrnEqual(string urn)
    {
        const string sql = "FederationLeadURN = @FederationLeadURN";
        var parameters = new { FederationLeadURN = urn };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereTypeIn(string[] types)
    {
        const string sql = "Type IN @Types";
        var parameters = new { Types = types };

        Where(sql, parameters);
        return this;
    }
}