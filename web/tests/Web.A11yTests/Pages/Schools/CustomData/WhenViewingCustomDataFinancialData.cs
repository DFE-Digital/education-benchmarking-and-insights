namespace Web.A11yTests.Pages.Schools.CustomData;

//Requires correct organisation assigned to DSI account

// [Trait("Category", "CustomData")]
// public class WhenViewingCustomDataFinancialData(ITestOutputHelper testOutputHelper, WebDriver webDriver) : AuthPageBase(testOutputHelper, webDriver)
// {
//     protected override string PageUrl => $"/school/{TestConfiguration.School}/custom-data/financial-data";
//
//     [Fact]
//     public async Task ThenThereAreNoAccessibilityIssues()
//     {
//         await GoToPage();
//         await EvaluatePage();
//     }
//
//     [Fact]
//     public async Task ShowAllSectionsThenThereAreNoAccessibilityIssues()
//     {
//         await GoToPage();
//         await Page.Locator(".govuk-accordion__show-all").ClickAsync();
//         await EvaluatePage();
//     }
// }