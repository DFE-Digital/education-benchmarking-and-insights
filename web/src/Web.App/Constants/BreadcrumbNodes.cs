using SmartBreadcrumbs.Nodes;

namespace Web.App;

public static class BreadcrumbNodes
{
    public static MvcBreadcrumbNode SchoolHome(string urn) => new("Index", "School", PageTitles.SchoolHome) { RouteValues = new { urn } };
    public static MvcBreadcrumbNode SchoolComparison(string urn) => new("Index", "SchoolComparison", PageTitles.Comparison) { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode SchoolSpending(string urn) => new("Index", "SchoolSpending", PageTitles.Spending) { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode SchoolPlanning(string urn) => new("Index", "SchoolPlanning", "Curriculum and financial planning") { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode SchoolCensus(string urn) => new("Index", "SchoolCensus", PageTitles.Census) { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode SchoolCustomData(string urn) => new("Index", "SchoolCustomData", PageTitles.SchoolChangeData) { RouteValues = new { urn }, Parent = SchoolHome(urn) };

    public static MvcBreadcrumbNode TrustHome(string companyNumber) => new("Index", "Trust", PageTitles.TrustHome) { RouteValues = new { companyNumber } };
    public static MvcBreadcrumbNode TrustComparison(string companyNumber) => new("Index", "TrustComparison", PageTitles.Comparison) { RouteValues = new { companyNumber }, Parent = TrustHome(companyNumber) };
    public static MvcBreadcrumbNode TrustCensus(string companyNumber) => new("Index", "TrustCensus", PageTitles.Census) { RouteValues = new { companyNumber }, Parent = TrustHome(companyNumber) };
    public static MvcBreadcrumbNode TrustPlanning(string companyNumber) => new("Index", "TrustPlanning", "Curriculum and financial planning") { RouteValues = new { companyNumber }, Parent = TrustHome(companyNumber) };
}