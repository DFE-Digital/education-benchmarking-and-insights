using SmartBreadcrumbs.Nodes;

namespace Web.App;

public static class BreadcrumbNodes
{
    public static MvcBreadcrumbNode SchoolHome(string urn) => new("Index", "School", PageTitles.SchoolHome) { RouteValues = new { urn } };
    public static MvcBreadcrumbNode SchoolComparison(string urn) => new("Index", "SchoolComparison", PageTitles.Comparison) { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode SchoolSpending(string urn) => new("Index", "SchoolSpending", PageTitles.Spending) { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode HistoricData(string urn) => new("Index", "SchoolHistory", PageTitles.HistoricData) { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode DataDashboard(string urn) => new("Index", "SchoolDashboard", "View your data dashboard") { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode SchoolPlanning(string urn) => new("Index", "SchoolPlanning", "Curriculum and financial planning") { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode SchoolWorkforce(string urn) => new("Index", "SchoolWorkforce", PageTitles.Workforce) { RouteValues = new { urn }, Parent = SchoolHome(urn) };

    public static MvcBreadcrumbNode TrustHome(string companyNumber) => new("Index", "Trust", PageTitles.TrustHome) { RouteValues = new { companyNumber } };
    public static MvcBreadcrumbNode TrustComparison(string companyNumber) => new("Index", "TrustComparison", PageTitles.Comparison) { RouteValues = new { companyNumber }, Parent = TrustHome(companyNumber) };
    public static MvcBreadcrumbNode TrustWorkforce(string urn) => new("Index", "TrustWorkforce", PageTitles.Workforce) { RouteValues = new { urn }, Parent = TrustHome(urn) };
}