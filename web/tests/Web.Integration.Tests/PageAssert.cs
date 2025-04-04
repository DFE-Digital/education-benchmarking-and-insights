using AngleSharp.Html.Dom;

namespace Web.Integration.Tests;

public static class PageAssert
{
    public static void IsNotFoundPage(IHtmlDocument doc)
    {
        DocumentAssert.TitleAndH1(doc, "Page not found - Financial Benchmarking and Insights Tool - GOV.UK", "Page not found");

        var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
        DocumentAssert.TextEqual(paras[0], "If you typed the web address, check it is correct.");
        DocumentAssert.TextEqual(paras[1], "If you pasted the web address, check you copied the entire address.");
    }

    public static void IsAccessDeniedPage(IHtmlDocument doc)
    {
        DocumentAssert.TitleAndH1(doc, "Access denied - Financial Benchmarking and Insights Tool - GOV.UK",
            "Access denied");

        var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
        DocumentAssert.TextEqual(paras[0], "You do not have access to this page.");
    }

    public static void IsProblemPage(IHtmlDocument doc)
    {
        DocumentAssert.TitleAndH1(doc, "Sorry, there is a problem with the service - Financial Benchmarking and Insights Tool - GOV.UK",
            "Sorry, there is a problem with the service");

        var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
        DocumentAssert.TextEqual(paras[0], "Try again later.");
    }

    public static void IsForbiddenPage(IHtmlDocument doc)
    {
        DocumentAssert.TitleAndH1(doc, "Access denied - Financial Benchmarking and Insights Tool - GOV.UK",
            "Access denied");

        var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
        DocumentAssert.TextEqual(paras[0], "You are unable to access this page. This could be because:");
    }

    public static void IsFeatureDisabledPage(IHtmlDocument doc)
    {
        DocumentAssert.TitleAndH1(doc, "Access denied - Financial Benchmarking and Insights Tool - GOV.UK",
            "Access denied");

        var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
        DocumentAssert.TextEqual(paras[0], "This feature is currently disabled.");
    }
}