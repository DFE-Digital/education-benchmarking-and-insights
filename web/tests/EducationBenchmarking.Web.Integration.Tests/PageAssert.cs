using AngleSharp.Html.Dom;

namespace EducationBenchmarking.Web.Integration.Tests;

public static class PageAssert
{
    public static void IsNotFoundPage(IHtmlDocument doc)
    {
        DocumentAssert.TitleAndH1(doc, "Page not found - Financial Benchmarking and Insights Tool - GOV.UK", "Page not found");

        var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
        DocumentAssert.TextEqual(paras[0], "If you typed the web address, check it is correct.");
        DocumentAssert.TextEqual(paras[1], "If you pasted the web address, check you copied the entire address.");

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(doc, expectedBreadcrumbs);
    }

    public static void IsAccessDeniedPage(IHtmlDocument doc)
    {
        DocumentAssert.TitleAndH1(doc, "Access denied - Financial Benchmarking and Insights Tool - GOV.UK",
            "Access denied");

        var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
        DocumentAssert.TextEqual(paras[0], "You do not have access to this page.");

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(doc, expectedBreadcrumbs);
    }

    public static void IsProblemPage(IHtmlDocument doc)
    {
        DocumentAssert.TitleAndH1(doc, "Sorry, there is a problem with the service - Financial Benchmarking and Insights Tool - GOV.UK",
            "Sorry, there is a problem with the service");

        var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
        DocumentAssert.TextEqual(paras[0], "Try again later.");

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(doc, expectedBreadcrumbs);
    }
}