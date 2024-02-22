using System.Collections;
using EducationBenchmarking.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using Xunit;

namespace EducationBenchmarking.Web.Tests.Extensions;

public class GivenAHtmlHelper
{
    [Theory]
    [ClassData(typeof(TrackedAnchorTestData))]
    public void TrackedAnchorShouldBeValid(TrackedLinks link, string href, string content, string hidden, string target,
        string[] rel, string[] classes, string expected)
    {
        var helper = new Mock<IHtmlHelper>();

        var actual = helper.Object.TrackedAnchor(link, href, content, hidden, target, rel, classes);

        using var writer = new StringWriter();
        actual.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        var output = writer.ToString();

        Assert.Equal(expected, output);
    }
}

public class TrackedAnchorTestData : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
    [
        [
            TrackedLinks.SchoolDetails,
            "http://www.example.com",
            "content",
            "",
            "",
            Array.Empty<string>(),
            Array.Empty<string>(),
            "<a class=\"govuk-link govuk-link--no-visited-state\" data-id=\"gias-school-details\" href=\"http://www.example.com\">content</a>"
        ],
        [
            TrackedLinks.SchoolDetails,
            "http://www.example.com",
            "content",
            "hidden",
            "",
            Array.Empty<string>(),
            Array.Empty<string>(),
            "<a class=\"govuk-link govuk-link--no-visited-state\" data-id=\"gias-school-details\" href=\"http://www.example.com\">content<span class=\"govuk-visually-hidden\"> hidden</span></a>"
        ],
        [
            TrackedLinks.SchoolDetails,
            "http://www.example.com",
            "content",
            "",
            "_blank",
            Array.Empty<string>(),
            Array.Empty<string>(),
            "<a class=\"govuk-link govuk-link--no-visited-state\" data-id=\"gias-school-details\" href=\"http://www.example.com\" target=\"_blank\">content</a>"
        ],
        [
            TrackedLinks.SchoolDetails,
            "http://www.example.com",
            "content",
            "",
            "",
            new[] { "noopener", "noreferrer", "external" },
            Array.Empty<string>(),
            "<a class=\"govuk-link govuk-link--no-visited-state\" data-id=\"gias-school-details\" href=\"http://www.example.com\" rel=\"noopener noreferrer external\">content</a>"
        ],
        [
            TrackedLinks.SchoolDetails,
            "http://www.example.com",
            "content",
            "",
            "",
            Array.Empty<string>(),
            new[] { "govuk-button" },
            "<a class=\"govuk-button\" data-id=\"gias-school-details\" href=\"http://www.example.com\">content</a>"
        ]
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}