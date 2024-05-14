using SmartBreadcrumbs.Nodes;

namespace Web.App;

public static class BreadcrumbNodes
{
    public static MvcBreadcrumbNode SchoolHome(string urn)
    {
        return new MvcBreadcrumbNode("Index", "School", PageTitles.SchoolHome) { RouteValues = new { urn } };
    }

    public static MvcBreadcrumbNode SchoolComparison(string urn)
    {
        return new MvcBreadcrumbNode("Index", "SchoolComparison", PageTitles.Comparison)
        { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    }

    public static MvcBreadcrumbNode SchoolSpending(string urn)
    {
        return new MvcBreadcrumbNode("Index", "SchoolSpending", PageTitles.Spending)
        { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    }

    public static MvcBreadcrumbNode SchoolPlanning(string urn)
    {
        return new MvcBreadcrumbNode("Index", "SchoolPlanning", "Curriculum and financial planning")
        { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    }

    public static MvcBreadcrumbNode SchoolCensus(string urn)
    {
        return new MvcBreadcrumbNode("Index", "SchoolCensus", PageTitles.Census)
        { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    }

    public static MvcBreadcrumbNode SchoolCustomData(string urn)
    {
        return new MvcBreadcrumbNode("Index", "SchoolCustomData", PageTitles.SchoolChangeData)
        { RouteValues = new { urn }, Parent = SchoolSpending(urn) };
    }

    public static MvcBreadcrumbNode TrustHome(string companyNumber)
    {
        return new MvcBreadcrumbNode("Index", "Trust", PageTitles.TrustHome) { RouteValues = new { companyNumber } };
    }

    public static MvcBreadcrumbNode TrustComparison(string companyNumber)
    {
        return new MvcBreadcrumbNode("Index", "TrustComparison", PageTitles.Comparison)
        { RouteValues = new { companyNumber }, Parent = TrustHome(companyNumber) };
    }

    public static MvcBreadcrumbNode TrustCensus(string companyNumber)
    {
        return new MvcBreadcrumbNode("Index", "TrustCensus", PageTitles.Census)
        { RouteValues = new { companyNumber }, Parent = TrustHome(companyNumber) };
    }

    public static MvcBreadcrumbNode TrustPlanning(string companyNumber)
    {
        return new MvcBreadcrumbNode("Index", "TrustPlanning", "Curriculum and financial planning")
        { RouteValues = new { companyNumber }, Parent = TrustHome(companyNumber) };
    }

    public static MvcBreadcrumbNode LocalAuthorityHome(string code)
    {
        return new MvcBreadcrumbNode("Index", "LocalAuthority", PageTitles.LocalAuthorityHome)
        { RouteValues = new { code } };
    }

    public static MvcBreadcrumbNode LocalAuthorityComparison(string code)
    {
        return new MvcBreadcrumbNode("Index", "LocalAuthorityComparison", PageTitles.Comparison)
        { RouteValues = new { code }, Parent = LocalAuthorityHome(code) };
    }

    public static MvcBreadcrumbNode LocalAuthorityCensus(string code)
    {
        return new MvcBreadcrumbNode("Index", "LocalAuthorityCensus", PageTitles.Census)
        { RouteValues = new { code }, Parent = LocalAuthorityHome(code) };
    }
}