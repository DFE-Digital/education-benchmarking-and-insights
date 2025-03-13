namespace Platform.Sql.QueryBuilders;

public class YearsSchoolQuery : PlatformQuery
{
    private const string Sql = "SELECT * FROM VW_YearsSchool /**where**/";

    public YearsSchoolQuery(string urn) : base(Sql)
    {
        WhereUrnEqual(urn);
    }
}

public class YearsTrustQuery : PlatformQuery
{
    private const string Sql = "SELECT * FROM VW_YearsTrust /**where**/";

    public YearsTrustQuery(string companyNumber) : base(Sql)
    {
        WhereCompanyNumberEqual(companyNumber);
    }
}

public class YearsOverallPhaseQuery : PlatformQuery
{
    private const string Sql = "SELECT * FROM VW_YearsOverallPhase /**where**/";

    public YearsOverallPhaseQuery(string overallPhase, string financeType) : base(Sql)
    {
        WhereOverallPhaseEqual(overallPhase);
        WhereFinanceTypeEqual(financeType);
    }
}

public class YearsLocalAuthorityQuery : PlatformQuery
{
    private const string Sql = "SELECT * FROM VW_YearsLocalAuthority /**where**/";

    public YearsLocalAuthorityQuery(string code) : base(Sql)
    {
        WhereCodeEqual(code);
    }
}