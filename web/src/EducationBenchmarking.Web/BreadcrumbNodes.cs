using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web;

public static class BreadcrumbNodes
{
    public static MvcBreadcrumbNode SchoolHome(string urn) => new("Index", "School",PageTitleConstants.SchoolHome) { RouteValues = new { urn } };
    public static MvcBreadcrumbNode SchoolComparison(string urn) => new("Index", "SchoolComparison", PageTitleConstants.SchoolComparison) { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode HistoricData(string urn) => new("Index", "SchoolHistory", PageTitleConstants.HistoricData) { RouteValues = new { urn }, Parent = SchoolHome(urn) };
    public static MvcBreadcrumbNode DataDashboard(string urn) => new ("Index", "SchoolDashboard", "View your data dashboard") { RouteValues = new { urn }, Parent = SchoolHome(urn)};
    public static MvcBreadcrumbNode SchoolPlanning(string urn) => new("Index", "SchoolPlanning", "Curriculum and financial planning") { RouteValues = new { urn }, Parent = SchoolHome(urn)};
    public static MvcBreadcrumbNode SchoolWorkforce(string urn) => new("Index", "SchoolWorkforce", "Benchmark workforce data"){ RouteValues = new { urn }, Parent = SchoolHome(urn)};
    
    public static MvcBreadcrumbNode TrustHome(string companyNumber) => new("Index", "Trust", "Your trust") { RouteValues = new { companyNumber } };
}