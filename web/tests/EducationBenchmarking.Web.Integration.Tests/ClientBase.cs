using System.Diagnostics;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public abstract class ClientBase<TStartup> : IDisposable
    where TStartup : class
{
    private HttpClient _http;

    public ClientBase(WebApplicationFactory<TStartup> factory)
    {
        _http = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(Configure);
        }).CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
            HandleCookies = true
        });
    }

    protected abstract void Configure(IServiceCollection services);

    public Task<IHtmlDocument> Follow(IElement element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        switch (element)
        {
            case IHtmlLinkElement link:
                return _http.GetAsync(link.Href).GetDocumentAsync();
            case IHtmlAnchorElement a:
                return _http.GetAsync(a.Href).GetDocumentAsync();
            default:
                throw new Exception($"Unable to follow a {element.GetType()}");
        }
    }
        
    public async Task<IHtmlDocument> Navigate(string uri, Action<HttpResponseMessage>? responseValidation = null)
    {
        var response = await _http.GetAsync(uri);
        responseValidation?.Invoke(response);
        return await response.GetDocumentAsync();
    }

    public async Task<HttpResponseMessage> Get(string uri)
    {
        return await _http.GetAsync(uri);
    }
        
    public async Task<string> GetString(string uri)
    {
        return await _http.GetStringAsync(uri);
    }

    public async Task<HttpResponseMessage> Send(string uri, Action<HttpRequestMessage> cfg = null)
    {
        Debug.Assert(_http.BaseAddress != null, "Http.BaseAddress != null");
        
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(_http.BaseAddress.AbsoluteUri.TrimEnd('/') + "/" + uri.TrimStart('/')),
            Method = HttpMethod.Get
        };
            
        cfg?.Invoke(request);
            
        return await _http.SendAsync(request);
    }

    public async Task<IHtmlDocument> SubmitForm(
        IElement formElement,
        IElement submitButtonElement,
        Action<IHtmlFormElement> cfg = null
    ) => await SubmitFormRaw(formElement, submitButtonElement, cfg).GetDocumentAsync();

    public async Task<HttpResponseMessage> SubmitFormRaw(
        IElement formElement,
        IElement submitButtonElement,
        Action<IHtmlFormElement> cfg = null)
    {
        var form = Assert.IsAssignableFrom<IHtmlFormElement>(formElement);
        var submitButton = Assert.IsAssignableFrom<IHtmlButtonElement>(submitButtonElement);
            
        cfg?.Invoke(form);
           
        var submit = form.GetSubmission(submitButton);
            
        var target = (Uri)submit.Target;
        if (!String.IsNullOrWhiteSpace(submitButton.Form.Action))
        {
            var formAction = submitButton.Form.Action;
            target = new Uri(formAction, UriKind.RelativeOrAbsolute);
        }
            
        var submission = new HttpRequestMessage(new HttpMethod(submit.Method.ToString()), target)
        {
            Content = new StreamContent(submit.Body)
        };
            
        foreach (var header in submit.Headers)
        {
            submission.Headers.TryAddWithoutValidation(header.Key, header.Value);
            submission.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
            
        return await _http.SendAsync(submission);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_http != null)
            {
                _http.Dispose();
                _http = null;
            }
        }
    }
}