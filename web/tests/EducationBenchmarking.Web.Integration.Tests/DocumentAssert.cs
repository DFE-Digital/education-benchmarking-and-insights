using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public static class DocumentAssert
{
    public static void Breadcrumbs(IHtmlDocument? doc, params (string, string)[] breadcrumbs)
    {
        Assert.NotNull(doc);
        var bcs = doc.GetBreadcrumbs();
        Assert.Collection(bcs,
            breadcrumbs.Select<(string, string), Action<(string, string)>>(expected =>
            {
                return actual => Assert.Equal(expected, actual);
            }).ToArray()
        );
    }
    
    public static void TitleAndH1(IHtmlDocument? doc, string pageTitle, string header1)
    {
        Assert.NotNull(doc);
        var h1 = doc.QuerySelector("h1") ?? throw new Exception("No <h1> elements found in this page document");
        
        Assert.Equal(pageTitle, doc.Title);
        AssertNodeText(h1, header1);
    }

    public static void Heading2(IHtmlDocument? doc, string header2)
    {
        Assert.NotNull(doc);
        
        var h2 = doc.QuerySelector("h2") ?? throw new Exception("No <h2> elements found in this page document");
        AssertNodeText(h2, header2);
    }
    
    public static void Heading2(INode? node, string header2)
    {
        Assert.NotNull(node);
        
        var h2 = node.ChildNodes.QuerySelector("h2") ?? throw new Exception("No <h2> elements found in this page document");
        AssertNodeText(h2, header2);
    }

    public static void Heading3(INode? node, string header3)
    {
        Assert.NotNull(node);

        var h3 = node.ChildNodes.QuerySelector("h3") ?? throw new Exception("No <h3> elements found in this page document");
        AssertNodeText(h3, header3);
    }

    public static void AssertPageUrl(IHtmlDocument? doc, string expectedUrl)
    {
        Assert.NotNull(doc);
        Assert.Equal(expectedUrl, doc.Url);
    }
    
    public static void PrimaryCta(IElement element, string contents, string url, bool enabled = true)
    {
        AssertStandardCta(element, contents, url, enabled);
    }

    public static void SecondaryCta(IElement element, string contents, string url, bool enabled = true)
    {
        AssertStandardCta(element, contents,url, enabled);
        Assert.True(element.ClassList.Contains("govuk-button--secondary"),"The secondary CTA should have a the class govuk-button--secondary assigned");
    }
    
    public static void Link(IElement element, string contents, string url)
    {
        Assert.Equal(contents, element.TextContent.Trim());
        Assert.True(element.ClassList.Contains("govuk-link"), "A link should have a the class govuk-link");
        
        switch (element)
        {
            case IHtmlAnchorElement a:
                Assert.Equal(url, a.Href);
                break;
            case IHtmlLinkElement l:
                Assert.Equal(url, l.Href);
                break;
        }
    }

    public static void BackLink(IElement element, string contents, string url)
    {
        Assert.Equal(contents, element.TextContent.Trim());
        Assert.True(element.ClassList.Contains("govuk-back-link"), "A back link should have a the class govuk-back-link");

        switch (element)
        {
            case IHtmlAnchorElement a:
                Assert.Equal(url, a.Href);
                break;
            case IHtmlLinkElement l:
                Assert.Equal(url, l.Href);
                break;
        }
    }

    private static void AssertNodeText(INode element, string text)
    {
        var elementText = string.Join(" ", element.ChildNodes.Select(n => n.TextContent.Trim())).Trim();
        Assert.Equal(text, elementText);
    }
    
    private static void AssertStandardCta(IElement element, string contents, string url, bool enabled)
    {
        Assert.Equal(contents, element.TextContent.Trim());
        Assert.True(element.ClassList.Contains("govuk-button"),"The CTA should have a the class govuk-button assigned");
        
        switch (element)
        {
            case IHtmlLinkElement a:
                Assert.Equal(url, a.Href);
                break;
            case IHtmlAnchorElement c:
                Assert.Equal(url, c.PathName + c.Search);
                break;
            case IHtmlButtonElement b:
                Assert.Equal(url, b.Form?.Action);
                break;
        }

        if (!enabled)
        {
            Assert.True(element.ClassList.Contains("govuk-button--disabled"), $"The {element.Id} should have govuk-button--disabled class supplied");
        }
    }
}