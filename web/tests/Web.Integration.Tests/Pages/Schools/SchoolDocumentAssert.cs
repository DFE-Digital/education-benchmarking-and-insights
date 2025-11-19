using AngleSharp.Dom;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools;

public static class SchoolDocumentAssert
{
    public static void AssertAcademyWithIndicators(IElement element, string urn, string? additionalText = null)
    {
        if (additionalText != null)
        {
            var plainText = GetPlainText(element);
            Assert.Equal(additionalText == null ? 2 : 4, plainText.Length);
            DocumentAssert.TextEqual(plainText[0], additionalText!);
        }

        var text = GetInsetText(element);
        Assert.Equal(3, text.Length);
        DocumentAssert.TextEqual(text[0], "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
        DocumentAssert.TextEqual(text[1], "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
        DocumentAssert.TextEqual(text[2], "Progress 8 data uses the latest data available from the academic year 2023 to 2024 and is taken from Compare school and college performance in England", true);

        var link = text[2].QuerySelector("a");
        DocumentAssert.Link(link, "Compare school and college performance in England", $"https://www.compare-school-performance.service.gov.uk/school/{urn}");
    }

    public static void AssertMaintainedSchoolWithIndicators(IElement element, string urn, string? additionalText = null)
    {
        if (additionalText != null)
        {
            var plainText = GetPlainText(element);
            Assert.Equal(additionalText == null ? 2 : 3, plainText.Length);
            DocumentAssert.TextEqual(plainText[0], additionalText!);
        }

        var text = GetInsetText(element);
        Assert.Equal(2, text.Length);
        DocumentAssert.TextEqual(text[0], "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
        DocumentAssert.TextEqual(text[1], "Progress 8 data uses the latest data available from the academic year 2023 to 2024 and is taken from Compare school and college performance in England", true);

        var link = text[1].QuerySelector("a");
        DocumentAssert.Link(link, "Compare school and college performance in England", $"https://www.compare-school-performance.service.gov.uk/school/{urn}");
    }

    public static void AssertAcademyNoIndicators(IElement element, string? additionalText = null)
    {
        if (additionalText != null)
        {
            var plainText = GetPlainText(element);
            Assert.Equal(additionalText == null ? 2 : 3, plainText.Length);
            DocumentAssert.TextEqual(plainText[0], additionalText!);
        }

        var text = GetInsetText(element);
        Assert.Equal(2, text.Length);
        DocumentAssert.TextEqual(text[0], "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
        DocumentAssert.TextEqual(text[1], "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
    }

    public static void AssertMaintainedSchoolNoIndicators(IElement element, string? additionalText = null)
    {
        if (additionalText != null)
        {
            var plainText = GetPlainText(element);
            Assert.Equal(additionalText == null ? 2 : 3, plainText.Length);
            DocumentAssert.TextEqual(plainText[0], additionalText!);
        }

        var text = GetInsetText(element);
        Assert.Equal(1, text.Length);
        DocumentAssert.TextEqual(text[0], "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
    }

    public static void AssertAcademyNoBanding(IElement element, string? additionalText = null)
    {
        var text = GetPlainText(element);
        Assert.Equal(additionalText == null ? 2 : 3, text.Length);
        if (additionalText != null)
        {
            DocumentAssert.TextEqual(text[0], additionalText);
        }

        DocumentAssert.TextEqual(text[additionalText == null ? 0 : 1], "This school's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");
        DocumentAssert.TextEqual(text[additionalText == null ? 1 : 2], "Data for academies in a Multi-Academy Trust (MAT) includes a share of MAT central finance.");
    }

    public static void AssertMaintainedSchoolNoBanding(IElement element, string? additionalText = null)
    {
        var text = GetPlainText(element);
        Assert.Equal(additionalText == null ? 1 : 2, text.Length);
        if (additionalText != null)
        {
            DocumentAssert.TextEqual(text[0], additionalText);
        }

        DocumentAssert.TextEqual(text[additionalText == null ? 0 : 1], "This school's data covers the financial year April 2020 to March 2021 consistent financial reporting return (CFR).");
    }

    private static IHtmlCollection<IElement> GetInsetText(IElement element)
    {
        var inset = element.QuerySelector(".govuk-inset-text");
        Assert.NotNull(inset);
        var text = inset.QuerySelectorAll("p");
        Assert.NotNull(text);
        return text;
    }

    private static IHtmlCollection<IElement> GetPlainText(IElement element)
    {
        var text = element.QuerySelectorAll("p");
        Assert.NotNull(text);
        return text;
    }
}