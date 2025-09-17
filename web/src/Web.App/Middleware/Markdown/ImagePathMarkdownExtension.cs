using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.Extensions.Options;
using Web.App.Infrastructure.WebAssets;

namespace Web.App.Middleware.Markdown;

/// <summary>
///     Post-processes markdown document to update image URLs with the prefix defined in config
/// </summary>
public class ImagePathMarkdownExtension(IOptions<WebAssetsOptions> options) : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        if (string.IsNullOrWhiteSpace(options.Value.ImagesBaseUrl))
        {
            return;
        }

        // ensure delegate is only registered once
        pipeline.DocumentProcessed -= PipelineOnDocumentProcessed;
        pipeline.DocumentProcessed += PipelineOnDocumentProcessed;
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        // no renderer extensions registered here
    }

    private void PipelineOnDocumentProcessed(MarkdownDocument document)
    {
        foreach (var node in document.Descendants<LinkInline>())
        {
            if (node.IsImage && !string.IsNullOrWhiteSpace(node.Url))
            {
                var url = new Uri(node.Url, UriKind.RelativeOrAbsolute);
                node.Url = $"{options.Value.ImagesBaseUrl?.TrimEnd('/')}/{(url.IsAbsoluteUri ? url.AbsolutePath : url.ToString()).TrimStart('/')}";
            }
        }
    }
}