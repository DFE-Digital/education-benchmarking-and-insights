using System.Net.Http.Headers;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Xunit;

namespace Web.Integration.Tests;

public static class HtmlExtensions
{
    public static IHtmlFormElement SetFormValues(this IHtmlFormElement form, IDictionary<string, string> formValues)
    {
        foreach (var kvp in formValues)
        {
            switch (form[kvp.Key])
            {
                case IHtmlInputElement element:
                    element.IsChecked = element.Type is "radio" or "checkbox";
                    element.Value = kvp.Value;
                    break;
                case IHtmlTextAreaElement textArea:
                    textArea.Value = kvp.Value;
                    break;
                case IHtmlSelectElement select:
                    select.Value = kvp.Value;
                    break;
            }
        }

        return form;
    }

    public static async Task<IHtmlDocument> GetDocumentAsync(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var document = await BrowsingContext.New(Configuration.Default.WithCss())
            .OpenAsync(ResponseFactory, CancellationToken.None);

        return (IHtmlDocument)document;

        void ResponseFactory(VirtualResponse htmlResponse)
        {
            Assert.NotNull(response.RequestMessage);

            htmlResponse
                .Address(response.RequestMessage.RequestUri)
                .Status(response.StatusCode);

            MapHeaders(response.Headers);
            MapHeaders(response.Content.Headers);

            htmlResponse.Content(content);
            return;

            void MapHeaders(HttpHeaders headers)
            {
                foreach (var header in headers)
                {
                    foreach (var value in header.Value)
                    {
                        htmlResponse.Header(header.Key, value);
                    }
                }
            }
        }
    }

    public static IEnumerable<(string, string)> GetBreadcrumbs(this IHtmlDocument doc)
    {
        return doc.GetElementsByClassName("govuk-breadcrumbs__list-item").Select(li =>
        {
            var l = li.QuerySelector("a");
            return l is IHtmlAnchorElement c ? (c.TextContent.Trim(), c.Href) : (li.TextContent.Trim(), "");
        });
    }

    public static IEnumerable<T> FindMainContentElements<T>(this IHtmlDocument doc)
    {
        var main = doc.GetElementById("main-content");
        Assert.NotNull(main);
        return main.Descendants<T>();
    }
}