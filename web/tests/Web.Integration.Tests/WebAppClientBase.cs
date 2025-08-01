using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Xunit.Abstractions;

namespace Web.Integration.Tests;

public abstract class WebAppClientBase<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly IMessageSink _messageSink;
    private HttpClient _http;

    protected WebAppClientBase(IMessageSink messageSink, Action<TestAuthOptions>? authCfg = null)
    {
        _http = BuildHttpClient(authCfg);
        _messageSink = messageSink;
    }

    private HttpClient BuildHttpClient(Action<TestAuthOptions>? authCfg = null)
    {
        return WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication("Test").AddScheme<TestAuthOptions, Auth>("Test", options =>
                {
                    authCfg?.Invoke(options);
                });
                Configure(services);
            });
        }).CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost"),
                HandleCookies = true
            });
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(_ => { });
        builder.UseEnvironment("Integration")
            // prevent `ArgumentNullException` from test run in ADO pipeline in
            // Westwind.AspNetCore.Markdown.MarkdownPageProcessorMiddleware
            .UseWebRoot($"{builder.GetSetting("contentRoot")}/wwwroot");
    }

    protected abstract void Configure(IServiceCollection services);

    public async Task<IHtmlDocument> Follow(IElement? element)
    {
        Assert.NotNull(element);

        var href = element switch
        {
            IHtmlLinkElement link => link.Href,
            IHtmlAnchorElement a => a.Href,
            _ => throw new ArgumentOutOfRangeException(nameof(element), $"Unable to follow a {element.GetType()}")
        };

        _messageSink.OnMessage($"Following link : {href}".ToDiagnosticMessage());
        var response = await _http.GetAsync(href);

        _messageSink.OnMessage(response.ToDiagnosticMessage());
        return await response.GetDocumentAsync();
    }

    public async Task<IHtmlDocument> Navigate(string uri, Action<HttpResponseMessage>? responseValidation = null)
    {
        var response = await _http.GetAsync(uri);
        _messageSink.OnMessage(response.ToDiagnosticMessage());

        responseValidation?.Invoke(response);
        return await response.GetDocumentAsync();
    }

    public async Task<HttpResponseMessage> Get(string uri)
    {
        var response = await _http.GetAsync(uri);
        _messageSink.OnMessage(response.ToDiagnosticMessage());
        return response;
    }

    public async Task<IHtmlDocument> SubmitForm(
        IElement formElement,
        IElement submitButtonElement,
        Action<IHtmlFormElement>? cfg = null
    )
    {
        var response = await SubmitFormRaw(formElement, submitButtonElement, cfg);
        _messageSink.OnMessage(response.ToDiagnosticMessage());
        return await response.GetDocumentAsync();
    }

    private async Task<HttpResponseMessage> SubmitFormRaw(
        IElement formElement,
        IElement submitButtonElement,
        Action<IHtmlFormElement>? cfg = null)
    {
        var form = Assert.IsAssignableFrom<IHtmlFormElement>(formElement);
        var submitButton = Assert.IsAssignableFrom<IHtmlButtonElement>(submitButtonElement);

        cfg?.Invoke(form);

        var submit = form.GetSubmission(submitButton);
        Assert.NotNull(submit);
        var target = new Uri(submit.Target.Href);
        if (!string.IsNullOrWhiteSpace(submitButton.Form?.Action))
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

        _messageSink.OnMessage(submit.ToDiagnosticMessage());
        return await _http.SendAsync(submission);
    }

    public void RebuildHttpClient(Action<TestAuthOptions>? authCfg = null)
    {
        _http = BuildHttpClient(authCfg);
    }
}