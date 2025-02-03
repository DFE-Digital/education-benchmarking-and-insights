using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School;

public partial class SpendingCostsPage
{
    private ILocator SaveImagesModal =>
        page.Locator(Selectors.Modal, new PageLocatorOptions
        {
            HasText = "Save chart images"
        });

    private ILocator SaveImagesModalOkButton => page.Locator($"{Selectors.ModalButton}.govuk-button--ok");
    private ILocator SaveImagesModalCancelButton => page.Locator($"{Selectors.ModalButton}.govuk-button--cancel");
    private ILocator SaveImagesModalCloseButton => page.Locator($"{Selectors.ModalButton}.govuk-button--close");
    private ILocator SaveImagesModalCheckbox(string name) =>
        page.Locator($".govuk-checkboxes__item input:has(+ label:has-text('{name}'))");
    private ILocator SaveImagesModalValidationErrorMessage => page.Locator($"{Selectors.Modal} {Selectors.GovErrorSummary}");

    public async Task IsSaveImagesModalDisplayed(bool visible)
    {
        if (visible)
        {
            await SaveImagesModal.ShouldBeVisible();
        }
        else
        {
            await SaveImagesModal.ShouldNotBeVisible();
        }
    }

    public async Task IsSaveImagesModalStartButtonEnabled(bool enabled)
    {
        await SaveImagesModalOkButton.ShouldHaveText("Start");
        await SaveImagesModalOkButton.ShouldBeVisible();

        if (enabled)
        {
            await SaveImagesModalOkButton.ShouldBeEnabled();
        }
        else
        {
            await SaveImagesModalOkButton.ShouldBeDisabled();
        }
    }

    public async Task IsSaveImagesModalCancelButtonVisible()
    {
        await SaveImagesModalCancelButton.ShouldHaveText("Cancel");
        await SaveImagesModalCancelButton.ShouldBeVisible();
        await SaveImagesModalCancelButton.ShouldBeEnabled();
    }

    public async Task IsSaveImagesModalCloseButtonVisible()
    {
        await SaveImagesModalCloseButton.ShouldHaveText("\u00d7");
        await SaveImagesModalCloseButton.ShouldHaveAttribute("aria-label", "Close modal dialog");
        await SaveImagesModalCloseButton.ShouldBeVisible();
        await SaveImagesModalCloseButton.ShouldBeEnabled();
    }

    public async Task ClickSaveImagesModalOkButton()
    {
        await SaveImagesModalOkButton.ClickAsync();
    }

    public async Task ClickSaveImagesModalCancelButton()
    {
        await SaveImagesModalCancelButton.ClickAsync();
    }

    public async Task ClickSaveImagesModalCloseButton()
    {
        await SaveImagesModalCloseButton.ClickAsync();
    }

    public async Task PressEscapeKey()
    {
        await page.Keyboard.PressAsync("Escape");
    }

    public async Task ToggleSaveImagesModalCheckbox(string item, bool check)
    {
        var checkbox = SaveImagesModalCheckbox(item);
        if (check)
        {
            await checkbox.Check();
        }
        else
        {
            await checkbox.Uncheck();
        }
    }

    public async Task IsSaveImagesModalValidationErrorMessageDisplayed()
    {
        await SaveImagesModalValidationErrorMessage.ShouldBeVisible();
        await SaveImagesModalValidationErrorMessage.ShouldContainText("There is a problem\nSelect one or more items");
    }
}