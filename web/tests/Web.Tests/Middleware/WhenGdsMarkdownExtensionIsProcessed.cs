using Markdig;
using Web.App.Middleware.Markdown;
using Xunit;

namespace Web.Tests.Middleware;

public class WhenGdsMarkdownExtensionIsProcessed
{
    private readonly MarkdownPipeline _pipeline;

    public WhenGdsMarkdownExtensionIsProcessed()
    {
        var extension = new GdsMarkdownExtension();
        var pipeline = new MarkdownPipelineBuilder()
            .UsePipeTables();

        extension.Setup(pipeline);
        _pipeline = pipeline.Build();
    }

    [Fact]
    public void ShouldRenderGdsLink()
    {
        const string markdown = "[Example link](https://example.com)";
        var actual = Markdown.ToHtml(markdown, _pipeline);

        Assert.Equal("<p class=\"govuk-body\"><a href=\"https://example.com\" class=\"govuk-link\">Example link</a></p>\n", actual);
    }

    [Fact]
    public void ShouldRenderGdsList()
    {
        const string markdown = "* Example item 1\n* Example item 2";
        var actual = Markdown.ToHtml(markdown, _pipeline);

        Assert.Equal("<ul class=\"govuk-list govuk-list--bullet\">\n<li>Example item 1</li>\n<li>Example item 2</li>\n</ul>\n", actual);
    }

    [Theory]
    [InlineData("Single paragraph", "<p class=\"govuk-body\">Single paragraph</p>\n")]
    [InlineData("Split\nsingle paragraph", "<p class=\"govuk-body\">Split\nsingle paragraph</p>\n")]
    [InlineData("Double\n\nparagraph", "<p class=\"govuk-body\">Double</p>\n<p class=\"govuk-body\">paragraph</p>\n")]
    public void ShouldRenderGdsParagraph(string markdown, string expected)
    {
        var actual = Markdown.ToHtml(markdown, _pipeline);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldRenderGdsTable()
    {
        const string markdown = "| Item | Value |\n| --- | --- |\n| 001 | 123 |\n| 002 | 456 |";
        var actual = Markdown.ToHtml(markdown, _pipeline);

        Assert.Equal("<table class=\"govuk-table\">\n<thead>\n<tr class=\"govuk-table__row\">\n<th class=\"govuk-table__cell\">Item</th>\n<th class=\"govuk-table__cell\">Value</th>\n</tr>\n</thead>\n<tbody>\n<tr class=\"govuk-table__row\">\n<td class=\"govuk-table__cell\">001</td>\n<td class=\"govuk-table__cell\">123</td>\n</tr>\n<tr class=\"govuk-table__row\">\n<td class=\"govuk-table__cell\">002</td>\n<td class=\"govuk-table__cell\">456</td>\n</tr>\n</tbody>\n</table>\n", actual);
    }
}