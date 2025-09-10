using Microsoft.Playwright;

namespace Web.E2ETests.Pages.Trust.Comparators;

public enum ComparatorsByTypes
{
    Name,
    Characteristic
}

public class CreateComparatorsByPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "How do you want to choose your own set of trusts?"
        });

    private ILocator ComparatorsByRadios => page.Locator(Selectors.GovRadios);

    private ILocator ContinueButton => page.Locator(Selectors.GovButton,
        new PageLocatorOptions
        {
            HasText = "Continue"
        });

    private ILocator NameRadioButton => page.Locator(".govuk-radios__input#by-name");
    private ILocator CharacteristicRadioButton => page.Locator(".govuk-radios__input#by-characteristic");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await ComparatorsByRadios.ShouldBeVisible();
    }

    public async Task SelectComparatorsBy(ComparatorsByTypes type)
    {
        var radioButton = type switch
        {
            ComparatorsByTypes.Name => NameRadioButton,
            ComparatorsByTypes.Characteristic => CharacteristicRadioButton,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        await radioButton.Check();
    }

    public async Task<ICreateComparatorsByPage> ClickContinue(ComparatorsByTypes type)
    {
        await ContinueButton.ClickAsync();
        if (type == ComparatorsByTypes.Characteristic)
        {
            return new CreateComparatorsByCharacteristicPage(page);
        }

        return new CreateComparatorsByNamePage(page);
    }
}