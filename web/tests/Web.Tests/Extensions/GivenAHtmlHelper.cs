using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using Web.App;
using Web.App.Extensions;
using Xunit;

namespace Web.Tests.Extensions;

public class GivenAHtmlHelper
{
    public static TheoryData<TrackedLinks, string, string, string, string, string[], string[], Dictionary<string, string>?, string> TrackedAnchorTestData = new()
    {
        { TrackedLinks.SchoolDetails, "http://www.example.com", "content", "", "", [], [], null, "<a class=\"govuk-link govuk-link--no-visited-state\" data-custom-event-id=\"gias-school-details\" href=\"http://www.example.com\">content</a>" },
        {
            TrackedLinks.SchoolDetails, "http://www.example.com", "content", "hidden", "", [], [], null,
            "<a class=\"govuk-link govuk-link--no-visited-state\" data-custom-event-id=\"gias-school-details\" href=\"http://www.example.com\">content<span class=\"govuk-visually-hidden\"> hidden</span></a>"
        },
        { TrackedLinks.SchoolDetails, "http://www.example.com", "content", "", "_blank", [], [], null, "<a class=\"govuk-link govuk-link--no-visited-state\" data-custom-event-id=\"gias-school-details\" href=\"http://www.example.com\" target=\"_blank\">content</a>" },
        {
            TrackedLinks.SchoolDetails, "http://www.example.com", "content", "", "", ["noopener", "noreferrer", "external"], [], null,
            "<a class=\"govuk-link govuk-link--no-visited-state\" data-custom-event-id=\"gias-school-details\" href=\"http://www.example.com\" rel=\"noopener noreferrer external\">content</a>"
        },
        { TrackedLinks.SchoolDetails, "http://www.example.com", "content", "", "", [], ["govuk-button"], null, "<a class=\"govuk-button\" data-custom-event-id=\"gias-school-details\" href=\"http://www.example.com\">content</a>" },
        {
            TrackedLinks.LaSchoolHomepage, "http://www.example.com", "content", "", "", [], [], new Dictionary<string, string>
            {
                { "key", "value" }
            },
            "<a class=\"govuk-link govuk-link--no-visited-state\" data-custom-event-id=\"la-school-homepage\" data-custom-event-key=\"value\" href=\"http://www.example.com\">content</a>"
        }
    };

    [Theory]
    [MemberData(nameof(TrackedAnchorTestData))]
    public void TrackedAnchorShouldBeValid(
        TrackedLinks link,
        string href,
        string content,
        string hidden,
        string target,
        string[] rel,
        string[] classes,
        Dictionary<string, string>? properties,
        string expected)
    {
        var helper = new Mock<IHtmlHelper>();

        var actual = helper.Object.TrackedAnchor(link, href, content, hidden, target, rel, properties, classes);

        using var writer = new StringWriter();
        actual.WriteTo(writer, HtmlEncoder.Default);
        var output = writer.ToString();

        Assert.Equal(expected, output);
    }
}