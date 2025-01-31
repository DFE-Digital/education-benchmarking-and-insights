using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School;

public partial class SpendingCostsPage
{
    private ILocator SaveAllImagesModal =>
        page.Locator(Selectors.Modal, new PageLocatorOptions
        {
            HasText = "Save all chart images"
        });

    private ILocator SaveAllImagesModalOkButton => page.Locator($"{Selectors.ModalButton}.govuk-button--ok");
    private ILocator SaveAllImagesModalCancelButton => page.Locator($"{Selectors.ModalButton}.govuk-button--cancel");
    private ILocator SaveAllImagesModalCloseButton => page.Locator($"{Selectors.ModalButton}.govuk-button--close");

    public async Task IsSaveAllImagesModalDisplayed(bool visible)
    {
        if (visible)
        {
            await SaveAllImagesModal.ShouldBeVisible();
        }
        else
        {
            await SaveAllImagesModal.ShouldNotBeVisible();
        }
    }

    public async Task IsSaveAllImagesModalStartButtonEnabled(bool enabled)
    {
        await SaveAllImagesModalOkButton.ShouldHaveText("Start");
        await SaveAllImagesModalOkButton.ShouldBeVisible();

        if (enabled)
        {
            await SaveAllImagesModalOkButton.ShouldBeEnabled();
        }
        else
        {
            await SaveAllImagesModalOkButton.ShouldBeDisabled();
        }
    }

    public async Task IsSaveAllImagesModalCancelButtonVisible()
    {
        await SaveAllImagesModalCancelButton.ShouldHaveText("Cancel");
        await SaveAllImagesModalCancelButton.ShouldBeVisible();
        await SaveAllImagesModalCancelButton.ShouldBeEnabled();
    }

    public async Task IsSaveAllImagesModalCloseButtonVisible()
    {
        await SaveAllImagesModalCloseButton.ShouldHaveText("\u00d7");
        await SaveAllImagesModalCloseButton.ShouldHaveAttribute("aria-label", "Close modal dialog");
        await SaveAllImagesModalCloseButton.ShouldBeVisible();
        await SaveAllImagesModalCloseButton.ShouldBeEnabled();
    }

    public async Task ClickSaveAllImagesModalOkButton()
    {
        await SaveAllImagesModalOkButton.ClickAsync();
    }

    public async Task ClickSaveAllImagesModalCancelButton()
    {
        await SaveAllImagesModalCancelButton.ClickAsync();
    }

    public async Task ClickSaveAllImagesModalCloseButton()
    {
        await SaveAllImagesModalCloseButton.ClickAsync();
    }

    public async Task PressEscapeKey()
    {
        await page.Keyboard.PressAsync("Escape");
    }
}