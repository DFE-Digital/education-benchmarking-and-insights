using Dapper;

namespace Platform.Sql.QueryBuilders;

public abstract class PlatformQuery : SqlBuilder
{

    protected PlatformQuery(string sql, dynamic? parameters = null)
    {
        QueryTemplate = AddTemplate(sql, parameters);
    }
    public Template QueryTemplate { get; }

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

    public PlatformQuery WhereLaCodesIn(string[] laCodes)
    {
        const string sql = "LaCode IN @LaCodes";
        var parameters = new { LaCodes = laCodes };

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

    public PlatformQuery OrderBy(params string[] columns)
    {
        foreach (var column in columns)
        {
            base.OrderBy(column);
        }

        return this;
    }

    public PlatformQuery WhereUserIdEqual(string userId)
    {
        const string sql = "UserId = @UserId";
        var parameters = new
        {
            UserId = userId
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereStatusIn(params string[] statuses)
    {
        const string sql = "Status IN @Statuses";
        var parameters = new
        {
            Statuses = statuses
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereActive(bool active = true)
    {
        const string sql = "Active = @Active";
        var parameters = new
        {
            Active = active ? 1 : 0
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereOrganisationIdEqual(string organisationId)
    {
        const string sql = "OrganisationId = @OrganisationId";
        var parameters = new
        {
            OrganisationId = organisationId
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereOrganisationTypeEqual(string organisationType)
    {
        const string sql = "OrganisationType = @OrganisationType";
        var parameters = new
        {
            OrganisationType = organisationType
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereTypeEqual(string type)
    {
        const string sql = "Type = @Type";
        var parameters = new
        {
            Type = type
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereStatusEqual(string status)
    {
        const string sql = "Status = @Status";
        var parameters = new
        {
            Status = status
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereIdEqual(string id)
    {
        const string sql = "Id = @Id";
        var parameters = new
        {
            Id = id
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereValueIsNotNull()
    {
        const string sql = "Value IS NOT NULL";
        Where(sql);
        return this;
    }

    public PlatformQuery WhereNameEqual(string name)
    {
        const string sql = "Name = @Name";
        var parameters = new
        {
            Name = name
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereValueEqual(string value)
    {
        const string sql = "Value = @Value";
        var parameters = new
        {
            Value = value
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereTargetEqual(string target)
    {
        const string sql = "Target = @Target";
        var parameters = new
        {
            Target = target
        };

        Where(sql, parameters);
        return this;
    }

    public PlatformQuery WhereSlugEqual(string slug)
    {
        const string sql = "Slug = @Slug";
        var parameters = new
        {
            Slug = slug
        };

        Where(sql, parameters);
        return this;
    }
}