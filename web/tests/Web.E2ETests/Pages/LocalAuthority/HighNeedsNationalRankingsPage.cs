﻿using Microsoft.Playwright;
using Web.E2ETests.Assist;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HighNeedsNationalRankingsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ModeTable);
    private ILocator Table => page.Locator($"#la-national-rank {Selectors.GovTable}");
    private ILocator SaveImageButton => page.Locator("xpath=//*[contains(@data-custom-event-chart-name, 'National view')][@data-custom-event-id='save-chart-as-image']");
    private ILocator CopyImageButton => page.Locator("xpath=//*[contains(@data-custom-event-chart-name, 'National view')][@data-custom-event-id='copy-chart-as-image']");
    private ILocator WarningMessage => page.Locator(Selectors.GovWarning);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task ClickViewAsTable()
    {
        await ViewAsTableRadio.ShouldBeVisible();
        await ViewAsTableRadio.ClickAsync();
    }

    public async Task TableContainsValues(DataTable table)
    {
        await Table.ShouldBeVisible();
        var rows = await Table.Locator("> tbody > tr").AllAsync();
        var set = new List<dynamic>();

        foreach (var row in rows)
        {
            set.Add(new
            {
                Name = await row.Locator("> td").Nth(0).InnerTextAsync(),
                Value = await row.Locator("> td").Nth(1).InnerTextAsync()
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    public async Task ClickSaveAsImage()
    {
        await SaveImageButton.Click();
    }

    public async Task ClickCopyImage()
    {
        await CopyImageButton.Click();
    }

    public async Task DoesNotContainWarningMessage()
    {
        await WarningMessage.ShouldNotBeVisible();
    }

    public async Task ContainsWarningMessage(string message)
    {
        await WarningMessage.ShouldBeVisible();
        await WarningMessage.ShouldContainText(message);
    }
}