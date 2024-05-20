using System.Net;
using Microsoft.Playwright;
using Web.A11yTests.Drivers;
using Xunit.Abstractions;
namespace Web.A11yTests;

public abstract class AuthPageBase(ITestOutputHelper testOutputHelper, WebDriver webDriver) : PageBase(testOutputHelper, webDriver)
{
    protected override async Task GoToPage(HttpStatusCode statusCode = HttpStatusCode.OK, IPage? basePage = null)
    {
        // Sign in using credentials set in config
        var page = await webDriver.GetPage($"{ServiceUrl}/account/sign-in", HttpStatusCode.OK);
        await page.Locator("#username").FillAsync(TestConfiguration.Authentication.Username);
        await page.Locator("#password").FillAsync(TestConfiguration.Authentication.Password);
        await page.GetByText("Sign in").ClickAsync();
        await page.WaitForURLAsync($"{ServiceUrl.TrimEnd('/')}/");

        // Save storage state into the file.
        // Tests are executed in <TestProject>\bin\Debug\netX.0\ therefore relative
        // path is used to reference playwright/.auth created in project root.
        await page.Context.StorageStateAsync(new BrowserContextStorageStateOptions
        {
            Path = "../../../playwright/.auth/state.json"
        });

        await base.GoToPage(statusCode, page);
    }
}