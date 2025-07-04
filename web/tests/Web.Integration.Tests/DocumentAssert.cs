using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Web.App.Domain.Content;
using Xunit;
namespace Web.Integration.Tests;

public static class DocumentAssert
{
    public static void Breadcrumbs(IHtmlDocument? doc, params (string, string)[] breadcrumbs)
    {
        /*
        See decision log: temp remove navigation to be review post private beta
        Assert.NotNull(doc);
        var bcs = doc.GetBreadcrumbs();
        Assert.Collection(bcs,
            breadcrumbs.Select<(string, string), Action<(string, string)>>(expected =>
            {
                return actual => Assert.Equal(expected, actual);
            }).ToArray()
        );*/
    }

    public static void TitleAndH1(IHtmlDocument? doc, string pageTitle, string header1)
    {
        Assert.NotNull(doc);
        Assert.Equal(pageTitle, doc.Title);

        var h1 = doc.QuerySelector("h1");
        AssertNodeText(h1, header1);
    }

    public static void Heading2(IHtmlDocument? doc, string header2)
    {
        Assert.NotNull(doc);

        var h2 = doc.QuerySelector("main h2:not(.govuk-notification-banner__title)");
        AssertNodeText(h2, header2);
    }

    public static void Heading2(INode? node, string header2)
    {
        Assert.NotNull(node);

        var h2 = node.ChildNodes.QuerySelector("h2");
        AssertNodeText(h2, header2);
    }

    public static void AssertPageUrl(IHtmlDocument? doc, string expectedUrl,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Assert.NotNull(doc);
        Assert.Equal(expectedUrl, doc.Url);
        Assert.Equal(statusCode, doc.StatusCode);
    }

    public static void PrimaryCta(IElement? element, string contents, string url, bool enabled = true)
    {
        Assert.NotNull(element);
        AssertStandardCta(element, contents, url, enabled);
    }

    public static void SecondaryCta(IElement? element, string contents, string url, bool enabled = true)
    {
        Assert.NotNull(element);
        AssertStandardCta(element, contents, url, enabled);
        Assert.True(element.ClassList.Contains("govuk-button--secondary"),
            "The secondary CTA should have a the class govuk-button--secondary assigned");
    }

    public static void Link(IElement? element, string contents, string url)
    {
        Assert.NotNull(element);
        TextEqual(element, contents);
        Assert.True(element.ClassList.Contains("govuk-link"), "A link should have the class govuk-link");

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

    public static void BackLink(IHtmlDocument? doc, string contents, string url)
    {
        /*
        See decision log: temp remove navigation to be review post private beta
        Assert.NotNull(doc);

        var backLink = doc.QuerySelector(".govuk-back-link");
        Assert.NotNull(backLink);
        TextEqual(backLink, contents);
        Assert.True(backLink.ClassList.Contains("govuk-back-link"),
            "A back link should have the class govuk-back-link");

        if (backLink is IHtmlAnchorElement a)
        {
            Assert.Equal(url, a.Href);
        }
        else
        {
            Assert.Fail("A back link should be an anchor element");
        }*/
    }

    public static void FormErrors(IHtmlDocument doc, params (string field, string message)[] errors)
    {
        foreach (var (field, message) in errors)
        {
            var element = doc.GetElementById($"{field}-error");
            Assert.NotNull(element);
            TextMatches(element, new Regex($@"Error:\s+{message}"));
        }
    }

    public static void SummaryErrors(IHtmlDocument doc, params (string field, string message)[] errors)
    {
        foreach (var (field, message) in errors)
        {
            var id = $"{field}-error-summary";
            var element = doc.GetElementById(id);
            Assert.True(element != null, $"Unable to find element {id} by Id");
            TextEqual(element, message);
        }
    }

    public static void Input(IHtmlDocument doc, string id, string? value)
    {
        var input = doc.GetElementById(id);
        Assert.NotNull(input);
        Assert.Equal(value, input.GetAttribute("value"));
    }

    public static void Checkboxes(IElement parent, params (string, string, bool)[] options)
    {
        var index = 0;
        foreach (var checkItem in parent.Descendants<IElement>()
                     .Where(c => c.ClassList.Contains("govuk-checkboxes__item")))
        {
            var (name, label, isChecked) = options[index];
            var inputElement = Assert.IsAssignableFrom<IHtmlInputElement>(checkItem.Children[0]);
            var labelElement = Assert.IsAssignableFrom<IHtmlLabelElement>(checkItem.Children[1]);

            ValidateInputElement(name, isChecked, inputElement);
            ValidateLabelElement(label, labelElement);

            Assert.Equal(inputElement.Id, labelElement.HtmlFor);
            index++;
        }

        return;

        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
        void ValidateInputElement(string name, bool isChecked, IHtmlInputElement e)
        {
            Assert.Equal("checkbox", e.Type);
            Assert.Equal(name, e.Name);
            Assert.Equal(isChecked, e.IsChecked);
            Assert.True(e.ClassList.Contains("govuk-checkboxes__input"),
                "A checkbox input should have the 'govuk-checkboxes__input' class applied");
        }

        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
        void ValidateLabelElement(string label, IElement e)
        {
            TextEqual(e, label);
            Assert.True(e.ClassList.Contains("govuk-label") && e.ClassList.Contains("govuk-checkboxes__label"),
                "A checkbox input should have the 'govuk-label' and 'govuk-checkboxes__label' classes applied");
        }
    }


    public static void Radios(IElement parent, params (string, string, string, bool)[] options)
    {
        var index = 0;
        foreach (var radioItem in parent.Descendants<IElement>()
                     .Where(c => c.ClassList.Contains("govuk-radios__item")))
        {
            var (name, value, label, isChecked) = options[index];
            var inputElement = Assert.IsAssignableFrom<IHtmlInputElement>(radioItem.Children[0]);
            var labelElement = Assert.IsAssignableFrom<IHtmlLabelElement>(radioItem.Children[1]);

            ValidateInputElement(name, value, isChecked, inputElement);
            ValidateLabelElement(label, labelElement);

            Assert.Equal(inputElement.Id, labelElement.HtmlFor);
            index++;
        }

        return;

        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
        void ValidateInputElement(string name, string value, bool isChecked, IHtmlInputElement e)
        {
            Assert.Equal("radio", e.Type);
            Assert.Equal(name, e.Name);
            Assert.Equal(value, e.Value);
            Assert.Equal(isChecked, e.IsChecked);
            Assert.True(e.ClassList.Contains("govuk-radios__input"),
                "A radio input should have the 'govuk-radios__item' class applied");
        }

        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
        void ValidateLabelElement(string label, IElement e)
        {
            TextEqual(e, label);
            Assert.True(e.ClassList.Contains("govuk-label") && e.ClassList.Contains("govuk-radios__label"),
                "A radio input should have the 'govuk-label' and 'govuk-radios__label' classes applied");
        }
    }

    public static void TextEqual(IElement element, string expected)
    {
        Assert.Equal(expected, element.TextContent.Trim());
    }

    public static void TextMatches(IElement element, Regex pattern)
    {
        Assert.Matches(pattern, element.TextContent.Trim());
    }

    public static void AssertNodeText(INode? node, string text)
    {
        Assert.NotNull(node);

        var elementText = string.Join(" ", node.ChildNodes.Select(n => n.TextContent.Trim())).Trim();
        Assert.Equal(text, elementText);
    }

    public static void Banner(IHtmlDocument? doc, Banner? banner)
    {
        Assert.NotNull(doc);

        var notification = doc.QuerySelector(".govuk-notification-banner");
        if (banner == null)
        {
            Assert.Null(notification);
            return;
        }

        Assert.NotNull(notification);
        AssertNodeText(notification.QuerySelector(".govuk-notification-banner__title"), banner.Title ?? "");
        AssertNodeText(notification.QuerySelector(".govuk-notification-banner__heading"), banner.Heading ?? "");
        AssertNodeText(notification.QuerySelector(".govuk-notification-banner__content > p"), banner.Body ?? "");
    }

    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    private static void AssertStandardCta(IElement element, string contents, string url, bool enabled)
    {
        TextEqual(element, contents);
        Assert.True(element.ClassList.Contains("govuk-button"),
            "The CTA should have a the class govuk-button assigned");

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
            Assert.True(element.ClassList.Contains("govuk-button--disabled"),
                $"The {element.Id} should have govuk-button--disabled class supplied");
        }
    }
}