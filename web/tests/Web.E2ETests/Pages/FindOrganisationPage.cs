﻿using Microsoft.Playwright;
using Web.E2ETests.Pages.School;

namespace Web.E2ETests.Pages;

public enum OrganisationTypes
{
    School,
    Trust
}

public class FindOrganisationPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator OrganisationsTypeRadios => page.Locator(Selectors.GovRadios);
    private ILocator SchoolSearchInputField => page.Locator(Selectors.SchoolSearchInput);

    private ILocator ContinueButton =>
        page.Locator(Selectors.GovButton, new PageLocatorOptions
        {
            HasText = "Continue"
        });

    private ILocator SchoolRadioButton => page.Locator(Selectors.SchoolRadio);
    private ILocator SchoolSuggestionsDropdown => page.Locator(Selectors.SchoolSuggestDropdown);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await OrganisationsTypeRadios.ShouldBeVisible();
        await ContinueButton.ShouldBeVisible().ShouldBeEnabled();
    }

    public async Task TypeIntoSchoolSearchBox(string text)
    {
        await SchoolSearchInputField.PressSequentially(text);
        await SchoolSuggestionsDropdown.ShouldBeVisible();
    }

    public async Task SelectItemFromSuggester()
    {
        await SchoolSearchInputField.PressAsync(Keyboard.ArrowDownKey);
        await page.Keyboard.PressAsync(Keyboard.EnterKey);
    }

    public async Task<School.HomePage> ClickContinueToSchool()
    {
        await ContinueButton.ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new School.HomePage(page);
    }

    public async Task<SearchPage> ClickContinueToSchoolSearch()
    {
        await ContinueButton.ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new SearchPage(page);
    }

    public async Task SelectOrganisationType(OrganisationTypes type)
    {
        var radioButton = type switch
        {
            OrganisationTypes.School => SchoolRadioButton,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
        await radioButton.Check();
    }

    public async Task AssertSearchResults(string keyword)
    {
        var listItems = await SchoolSuggestionsDropdown.Locator("li").AllAsync();
        foreach (var item in listItems)
        {
            await item.ShouldContainText(keyword);
        }
    }
}