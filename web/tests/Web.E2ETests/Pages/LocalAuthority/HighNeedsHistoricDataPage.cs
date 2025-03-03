using System.Dynamic;
using Microsoft.Playwright;
using Web.E2ETests.Assist;
using Xunit;

namespace Web.E2ETests.Pages.LocalAuthority;

public enum HighNeedsHistoryTabs
{
    Section251,
    Send2
}

public enum HistoricDataCategoryNames
{
    PlaceFunding,
    PlacementOfPupils
}

public class HighNeedsHistoricDataPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator Section251TabContent => page.Locator(Selectors.Section251Panel);
    private ILocator Section251CategoryHeadings => Section251TabContent.Locator($"{Selectors.H2} {Selectors.AccordionHeadingText}");
    private ILocator Tables => page.Locator(Selectors.GovTable);
    private ILocator Section251Sections => page.Locator($"{Selectors.Section251Tab} {Selectors.GovAccordionSection}");
    private ILocator Section251Accordion => page.Locator(Selectors.Section251Accordions);
    private ILocator Section251SubCategories => Section251Accordion.Locator($"{Selectors.H3}");
    private ILocator Section251ShowHideAllSectionsLink => Section251TabContent.Locator(Selectors.GovShowAllLinkText);
    private ILocator Send2ShowHideAllSectionsLink => Send2TabContent.Locator(Selectors.GovShowAllLinkText);
    private ILocator Section251ModeTable => page.Locator(Selectors.Section251ModeTable);
    private ILocator Section251ModeChart => page.Locator(Selectors.Section251ModeChart);
    private ILocator AllSection251Charts => Section251TabContent.Locator(Selectors.Charts);
    private ILocator Section251ChartsStats => Section251TabContent.Locator(Selectors.LineChartStats);
    private ILocator Section251TableView => page.Locator(Selectors.Section251TableMode);
    private ILocator Section251PlaceFundingAccordionContent => page.Locator(Selectors.Section251AccordionContent1);
    private ILocator Send2TabContent => page.Locator(Selectors.Send2Panel);
    private ILocator Send2CategoryHeadings => Send2TabContent.Locator($"{Selectors.H2} {Selectors.AccordionHeadingText}");
    private ILocator Send2Sections => page.Locator($"{Selectors.Send2Tab} {Selectors.GovAccordionSection}");
    private ILocator Send2Accordion => page.Locator(Selectors.Send2Accordions);
    private ILocator Send2SubCategories => Send2Accordion.Locator($"{Selectors.H3}");
    private ILocator Send2ModeTable => page.Locator(Selectors.Send2ModeTable);
    private ILocator Send2ModeChart => page.Locator(Selectors.Send2ModeChart);
    private ILocator AllSend2Charts => Send2TabContent.Locator(Selectors.Charts);
    private ILocator Send2ChartsStats => Send2TabContent.Locator(Selectors.LineChartStats);
    private ILocator Send2TableView => page.Locator(Selectors.Send2ModeTable);
    private ILocator Send2PlaceFundingAccordionContent => page.Locator(Selectors.Section251AccordionContent1);
    private ILocator Send2AccordionContent => page.Locator(Selectors.Send2AccordionContent1);
    private ILocator SaveAsImageButtons(string elementId)
    {
        return page.Locator($"#{elementId}").Locator(".share-button--save");
    }

    public async Task IsDisplayed(HighNeedsHistoryTabs? tab = null)
    {
        await PageH1Heading.ShouldBeVisible();

        var selectedTab = tab ?? HighNeedsHistoryTabs.Section251;
        switch (selectedTab)
        {
            case HighNeedsHistoryTabs.Section251:
                await Section251ShowHideAllSectionsLink.First.ShouldBeVisible();
                await Section251ModeTable.ShouldBeVisible().ShouldBeChecked(false);
                await Section251ModeChart.ShouldBeVisible().ShouldBeChecked();
                break;
            case HighNeedsHistoryTabs.Send2:
                await Send2ShowHideAllSectionsLink.First.ShouldBeVisible();
                await Send2ModeTable.ShouldBeVisible().ShouldBeChecked(false);
                await Send2ModeChart.ShouldBeVisible().ShouldBeChecked();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tab));
        }
    }

    public async Task ClickShowAllSections(HighNeedsHistoryTabs tab)
    {
        var showAllSectionsLink = tab switch
        {
            HighNeedsHistoryTabs.Section251 => Section251ShowHideAllSectionsLink.First,
            HighNeedsHistoryTabs.Send2 => Send2ShowHideAllSectionsLink.First,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var textContent = await showAllSectionsLink.TextContentAsync();

        if (textContent == "Show all sections")
        {
            await showAllSectionsLink.Click();
        }
    }

    public async Task AreSectionsExpanded(HighNeedsHistoryTabs tab)
    {
        var tabSections = tab switch
        {
            HighNeedsHistoryTabs.Section251 => Section251Sections,
            HighNeedsHistoryTabs.Send2 => Send2Sections,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var sections = await tabSections.AllAsync();
        foreach (var section in sections)
        {
            await section.AssertLocatorClass("govuk-accordion__section govuk-accordion__section--expanded");
        }
    }

    public async Task IsShowHideAllSectionsText(HighNeedsHistoryTabs tab, string expectedText)
    {
        var showAllSectionsLink = tab switch
        {
            HighNeedsHistoryTabs.Section251 => Section251ShowHideAllSectionsLink.First,
            HighNeedsHistoryTabs.Send2 => Send2ShowHideAllSectionsLink.First,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        await showAllSectionsLink.TextEqual(expectedText);
    }

    public async Task AreSubCategoriesVisible(HighNeedsHistoryTabs tab, string[] subCategories)
    {
        var expectedSubCategories = tab switch
        {
            HighNeedsHistoryTabs.Section251 => subCategories,
            HighNeedsHistoryTabs.Send2 => subCategories,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        await AreChartStatsVisible(tab);
        await AreChartsVisible(tab);
        Assert.Equal(expectedSubCategories, await GetSubCategoriesOfTab(tab));
    }

    public async Task HasChartCount(HighNeedsHistoryTabs tab, int count)
    {
        var charts = tab switch
        {
            HighNeedsHistoryTabs.Section251 => AllSection251Charts,
            HighNeedsHistoryTabs.Send2 => AllSend2Charts,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        Assert.Equal(count, await charts.Count());
    }

    public async Task AreTablesShown(HighNeedsHistoryTabs tab)
    {
        var sections = tab switch
        {
            HighNeedsHistoryTabs.Section251 => Section251TabContent.Locator(Tables),
            HighNeedsHistoryTabs.Send2 => Send2TabContent.Locator(Tables),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var tables = await sections.AllAsync();
        foreach (var table in tables)
        {
            await table.ShouldBeVisible();
        }
    }

    public async Task ClickViewAsTable(HighNeedsHistoryTabs tab)
    {
        var viewAsTableRadio = tab switch
        {
            HighNeedsHistoryTabs.Section251 => Section251TableView,
            HighNeedsHistoryTabs.Send2 => Send2TableView,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        await viewAsTableRadio.Click();
    }

    public async Task ClickSectionLink(HistoricDataCategoryNames categoryName)
    {
        var link = SectionLink(categoryName);

        await link.Locator(Selectors.ToggleSectionText).First.ClickAsync();
    }

    public async Task IsSectionVisible(HistoricDataCategoryNames categoryName, bool visibility, string text,
        string chartMode)
    {
        var link = SectionLink(categoryName);
        await link.ShouldHaveAttribute("aria-expanded", visibility.ToString().ToLower());
        await link.Locator(Selectors.ToggleSectionText).ShouldHaveText(text);
        await IsSectionContentVisible(categoryName, visibility, chartMode);
    }

    public async Task ChartLegendContains(string chartName, string text, string separator)
    {
        await page.WaitForSelectorAsync(Selectors.Charts);
        var parent = page.Locator($"div[aria-label='{chartName}']");
        var chart = parent.Locator(Selectors.Charts);
        await chart.ShouldBeVisible();

        var legend = chart.Locator("//following-sibling::div[1]");
        if (string.IsNullOrWhiteSpace(text))
        {
            await legend.ShouldNotBeVisible();
        }
        else
        {
            var parts = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                await legend.ShouldContainText(part.Trim());
            }
        }
    }

    public async Task ChartTableContains(string chartName, DataTable expected)
    {
        var table = page.GetByTestId($"{chartName}-table");
        await table.ShouldBeVisible();

        var set = new List<dynamic>();
        var rows = await table.Locator("tr").AllAsync();
        var headerCells = await rows[0].Locator("th").AllAsync();
        var headers = new List<string>();
        foreach (var headerCell in headerCells)
        {
            headers.Add(DynamicTableHelpers.CreatePropertyName(await headerCell.InnerTextAsync()));
        }

        foreach (var row in rows.Skip(1))
        {
            var expando = new ExpandoObject();
            var cells = await row.Locator("td").AllAsync();
            for (var i = 0; i < cells.Count; i++)
            {
                var header = headers.ElementAtOrDefault(i);
                if (!string.IsNullOrWhiteSpace(header))
                {
                    (expando as ICollection<KeyValuePair<string, object>>).Add(new KeyValuePair<string, object>(header, await cells[i].InnerTextAsync()));
                }
            }

            set.Add(expando);
        }

        expected.CompareToDynamicSet(set, false);
    }

    public async Task HasCategoryNames(HighNeedsHistoryTabs tab, string[] categories)
    {
        switch (tab)
        {
            case HighNeedsHistoryTabs.Section251:
                await AssertCategoryNames(categories, tab);
                break;
            case HighNeedsHistoryTabs.Send2:
                await AssertCategoryNames(categories, tab);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tab));
        }
    }

    private async Task IsSectionContentVisible(HistoricDataCategoryNames categoryName, bool visibility, string chartMode)
    {
        var contentLocator = categoryName switch
        {
            HistoricDataCategoryNames.PlaceFunding => Section251PlaceFundingAccordionContent,
            HistoricDataCategoryNames.PlacementOfPupils => Send2AccordionContent,
            _ => throw new ArgumentOutOfRangeException(nameof(categoryName))
        };
        foreach (var locator in await contentLocator.Locator(chartMode).AllAsync())
        {
            if (visibility)
            {
                await locator.ShouldBeVisible();
            }
            else
            {
                await locator.ShouldNotBeVisible();
            }
        }
    }

    private ILocator SectionLink(HistoricDataCategoryNames categoryName)
    {
        var link = categoryName switch
        {
            HistoricDataCategoryNames.PlaceFunding => SectionLink(Selectors.Section251AccordionHeading1),
            HistoricDataCategoryNames.PlacementOfPupils => SectionLink(Selectors.Send2AccordionHeading1),
            _ => throw new ArgumentOutOfRangeException(nameof(categoryName))
        };
        return link;
    }

    private ILocator SectionLink(string sectionId)
    {
        return page.Locator("button",
            new PageLocatorOptions
            {
                Has = page.Locator($"span{sectionId}")
            });
    }

    private async Task<string[]> GetSubCategoriesOfTab(HighNeedsHistoryTabs tab)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var subCategories = tab switch
        {
            HighNeedsHistoryTabs.Section251 => await Section251SubCategories.AllAsync(),
            HighNeedsHistoryTabs.Send2 => await Send2SubCategories.AllAsync(),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var subCategoriesHeadings = new List<string>();
        foreach (var category in subCategories)
        {
            var headingName = await category.TextContentAsync() ?? string.Empty;
            subCategoriesHeadings.Add(headingName.Trim());
        }

        return subCategoriesHeadings.ToArray();
    }

    private async Task AssertCategoryNames(string[] expected, HighNeedsHistoryTabs? tab)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var categories = tab switch
        {
            HighNeedsHistoryTabs.Section251 => await Section251CategoryHeadings.AllTextContentsAsync(),
            HighNeedsHistoryTabs.Send2 => await Send2CategoryHeadings.AllTextContentsAsync(),
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        foreach (var expectedHeading in expected)
        {
            Assert.Contains(expectedHeading, categories);
        }
    }

    private async Task AreChartStatsVisible(HighNeedsHistoryTabs? tab)
    {
        var sections = tab switch
        {
            HighNeedsHistoryTabs.Section251 => Section251ChartsStats,
            HighNeedsHistoryTabs.Send2 => Send2ChartsStats,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        await CheckVisibility(sections);
    }

    private async Task AreChartsVisible(HighNeedsHistoryTabs? tab)
    {
        var sections = tab switch
        {
            HighNeedsHistoryTabs.Section251 => AllSection251Charts,
            HighNeedsHistoryTabs.Send2 => AllSend2Charts,
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };
        await CheckVisibility(sections);
    }

    private static async Task CheckVisibility(ILocator locator)
    {
        var elements = await locator.AllAsync();
        foreach (var element in elements)
        {
            await element.ShouldBeVisible();
        }
    }

    public async Task AreSaveAsImageButtonsPresent(HighNeedsHistoryTabs tab, int expected)
    {
        var elementId = tab switch
        {
            HighNeedsHistoryTabs.Section251 => "section-251",
            HighNeedsHistoryTabs.Send2 => "send-2",
            _ => throw new ArgumentOutOfRangeException(nameof(tab))
        };

        var buttons = await SaveAsImageButtons(elementId).AllAsync();

        var firstButton = buttons[0];
        Assert.NotNull(firstButton);
        await firstButton.ShouldBeVisible();
        Assert.Equal(expected, buttons.Count);
    }
}