using AngleSharp.Dom;
using Xunit;
namespace Web.Integration.Tests.Pages;

public class WhenViewingCookies(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.Cookies);

        DocumentAssert.AssertPageUrl(page, Paths.Cookies.ToAbsolute());
        DocumentAssert.BackLink(page, "Back", $"{Paths.Cookies.ToAbsolute()}#");
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Cookies");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanToggleAnalyticsCookiesOn(bool accept)
    {
        var page = await Client.Navigate(Paths.Cookies);
        var action = page.QuerySelector("#cookie-settings-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "AnalyticsCookiesEnabled", accept.ToString().ToLower()
                }
            });
        });

        // `checked` attribute will be `null` if not present, or `string.Empty` if present 
        Assert.Equal(accept ? string.Empty : null, page.QuerySelector("#AnalyticsCookiesEnabled-accept")?.Attributes.GetNamedItem("checked")?.Value);
        Assert.Equal(accept ? null : string.Empty, page.QuerySelector("#AnalyticsCookiesEnabled-reject")?.Attributes.GetNamedItem("checked")?.Value);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanToggleAnalyticsCookiesOff(bool accept)
    {
        // ensure previously submitted cookie toggle has not prepopulated form
        Client.RebuildHttpClient();

        var page = await Client.Navigate(Paths.Cookies);
        var action = page.QuerySelector("#cookie-settings-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "AnalyticsCookiesEnabled", accept.ToString().ToLower()
                }
            });
        });

        // `checked` attribute will be `null` if not present, or `string.Empty` if present 
        Assert.Equal(accept ? string.Empty : null, page.QuerySelector("#AnalyticsCookiesEnabled-accept")?.Attributes.GetNamedItem("checked")?.Value);
        Assert.Equal(accept ? null : string.Empty, page.QuerySelector("#AnalyticsCookiesEnabled-reject")?.Attributes.GetNamedItem("checked")?.Value);
    }

    [Fact]
    public async Task CanDisplayErrorIfAnalyticsCookieChoiceNotMade()
    {
        // ensure previously submitted cookie toggle has not prepopulated form
        Client.RebuildHttpClient();

        var page = await Client.Navigate(Paths.Cookies);
        var action = page.QuerySelector("#cookie-settings-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        Assert.Equal("Error: Select if you want to accept analytics cookies", page.QuerySelector("#AnalyticsCookiesEnabled-error")?.GetInnerText());
    }
}