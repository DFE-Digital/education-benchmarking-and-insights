using Markdig;
using Microsoft.Extensions.Options;
using Web.App.Infrastructure.WebAssets;
using Web.App.Middleware.Markdown;
using Xunit;

namespace Web.Tests.Middleware;

public class WhenImagePathMarkdownExtensionIsProcessed
{
    private readonly MarkdownPipeline _pipeline;
    private const string ImagesBaseUrl = "/images";

    public WhenImagePathMarkdownExtensionIsProcessed()
    {
        var options = new WebAssetsOptions
        {
            ImagesBaseUrl = ImagesBaseUrl
        };
        var extension = new ImagePathMarkdownExtension(new OptionsWrapper<WebAssetsOptions>(options));
        var pipeline = new MarkdownPipelineBuilder();

        extension.Setup(pipeline);
        _pipeline = pipeline.Build();
    }

    [Theory]
    [InlineData("![Example image](image.png)", "<p><img src=\"/images/image.png\" alt=\"Example image\" /></p>\n")]
    [InlineData("![Example image](/image.png)", "<p><img src=\"/images/image.png\" alt=\"Example image\" /></p>\n")]
    [InlineData("![Example image](https://example.com/image.png)", "<p><img src=\"/images/image.png\" alt=\"Example image\" /></p>\n")]
    public void ShouldRenderImageWithPrefix(string markdown, string expected)
    {
        var actual = Markdown.ToHtml(markdown, _pipeline);
        Assert.Equal(expected, actual);
    }
}