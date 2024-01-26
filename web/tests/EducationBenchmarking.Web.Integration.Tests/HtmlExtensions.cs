using System.Diagnostics;
using System.Net.Http.Headers;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using AngleSharp.Io.Dom;

namespace EducationBenchmarking.Web.Integration.Tests;

public static class HtmlExtensions
{
    public static IHtmlFormElement SetFormValues(this IHtmlFormElement form, IDictionary<string,string> formValues)
    {
        foreach (var kvp in formValues)
        {
            if (form[kvp.Key] is IHtmlInputElement element)
            {
                if (element.Type == "radio")
                {
                    element.IsChecked = true;
                }
                    
                if (element.Type == "checkbox")
                {
                    element.IsChecked = true;
                }

                element.Value = kvp.Value;
            }

            if (form[kvp.Key] is IHtmlTextAreaElement textArea)
            {
                textArea.Value = kvp.Value;
            }

            if (form[kvp.Key] is IHtmlSelectElement select)
            {
                select.Value = kvp.Value;
            }
        }

        return form;
    }
        
    public static IHtmlFormElement AddFiles(this IHtmlFormElement form, string inputName, params IFile[] files)
    {
        if (form[inputName] is IHtmlInputElement fileInput)
        {
            foreach (var file in files)
            {
                fileInput.Files?.Add(file);
            }
        }

        return form;
    }
    
    public static async Task<IHtmlDocument> GetDocumentAsync(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var document = await BrowsingContext.New(Configuration.Default.WithCss()).OpenAsync(ResponseFactory, CancellationToken.None);
        
        return (IHtmlDocument) document;

        void ResponseFactory(VirtualResponse htmlResponse)
        {
            Debug.Assert(response.RequestMessage != null, "response.RequestMessage != null");
            
            htmlResponse
                .Address(response.RequestMessage.RequestUri)
                .Status(response.StatusCode);

            MapHeaders(response.Headers);
            MapHeaders(response.Content.Headers);

            htmlResponse.Content(content);

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
    
    public static async Task<IHtmlDocument> GetDocumentAsync(this Task<HttpResponseMessage> responseMessage)
    {
        var response = await responseMessage;
        return await response.GetDocumentAsync();
    }

    public static IEnumerable<(string, string)> GetBreadcrumbs(this IHtmlDocument doc)
    {
        return doc.GetElementsByClassName("govuk-breadcrumbs__list-item").Select(li =>
        {
            var l = li.QuerySelector("a");
            return l is IHtmlAnchorElement c ? (c.TextContent.Trim(), c.Href) : (li.TextContent.Trim(), "");
        });
    }
        
    public static (string Text, string Href) GetBackLink(this IHtmlDocument doc)
    {
        return doc.GetElementsByClassName("govuk-back-link").Select(l =>
        {
            var c = (IHtmlAnchorElement) l;
            return (c.TextContent.Trim(), c.Href);
        }).FirstOrDefault();
    }
}