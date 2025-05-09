﻿using Microsoft.Playwright;
using Reqnroll;
using Web.E2ETests.Drivers;
namespace Web.E2ETests.Hooks;

[Binding]
public class StepFailureHook(IReqnrollOutputHelper outputHelper, ScenarioContext scenarioContext, FeatureContext featureContext)
{
    [AfterStep]
    public async Task AfterStep()
    {
        if (scenarioContext.TestError != null)
        {
            await ScreenShot(scenarioContext.ScenarioInfo.Title);
        }
    }

    private async Task ScreenShot(string title)
    {
        var file = $"{Guid.NewGuid()}.png";
        var path = Path.Combine("screenshots", file);
        var page = await featureContext.FeatureContainer.Resolve<PageDriver>().Current;

        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = path
        });

        outputHelper.WriteLine($"Scenario '{title}' failed. Page screenshot: {file}");
    }
}