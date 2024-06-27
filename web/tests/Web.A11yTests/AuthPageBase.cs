using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Web.A11yTests.Drivers;
using Xunit.Abstractions;
namespace Web.A11yTests;

public abstract partial class AuthPageBase(ITestOutputHelper testOutputHelper, WebDriver webDriver) : PageBase(testOutputHelper, webDriver)
{
    protected override async Task GoToPage(HttpStatusCode statusCode = HttpStatusCode.OK, IPage? basePage = null)
    {
        // Sign in using credentials set in config
        var page = await webDriver.GetPage($"{ServiceUrl}/sign-in", HttpStatusCode.OK);
        await page.Locator("#username").FillAsync(TestConfiguration.Authentication.Username);
        await page.Locator("#password").FillAsync(TestConfiguration.Authentication.Password);
        await page.GetByText("Sign in").ClickAsync();
        await page.WaitForURLAsync(SelectOrganisation());
        await page.Locator("#organisation").Locator("input[type='radio']").First.ClickAsync();
        await page.GetByText("Continue").ClickAsync();
        await page.WaitForURLAsync($"{ServiceUrl.TrimEnd('/')}/");
        await base.GoToPage(statusCode, page);
    }

    [GeneratedRegex("\\/select-organisation{1}")]
    private static partial Regex SelectOrganisation();
}