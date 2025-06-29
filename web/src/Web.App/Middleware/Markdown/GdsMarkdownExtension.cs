using Markdig;
using Markdig.Extensions.Tables;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Web.App.Middleware.Markdown;

/// <summary>
///     Post-processes markdown document to enhance with GDS class names for the following elements:
///     <ul>
///         <li>
///             <c>&lt;a&gt;</c>
///         </li>
///         <li>
///             <c>&lt;ul&gt;</c>
///         </li>
///         <li>
///             <c>&lt;p&gt;</c>
///         </li>
///         <li>
///             <c>&lt;table&gt;</c>
///         </li>
///         <li>
///             <c>&lt;tr&gt;</c>
///         </li>
///         <li>
///             <c>&lt;td&gt;</c>
///         </li>
///     </ul>
/// </summary>
public class GdsMarkdownExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        // ensure delegate is only registered once
        pipeline.DocumentProcessed -= PipelineOnDocumentProcessed;
        pipeline.DocumentProcessed += PipelineOnDocumentProcessed;
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        // no renderer extensions registered here
    }

    private static void PipelineOnDocumentProcessed(MarkdownDocument document)
    {
        foreach (var node in document.Descendants())
        {
            var attributes = node.GetAttributes();
            switch (node)
            {
                case LinkInline:
                    attributes.AddClass("govuk-link");
                    break;
                case ListBlock:
                    attributes.AddClass("govuk-list");
                    attributes.AddClass("govuk-list--bullet");
                    break;
                case ParagraphBlock:
                    attributes.AddClass("govuk-body");
                    break;
                case Table:
                    attributes.AddClass("govuk-table");
                    break;
                case TableCell:
                    attributes.AddClass("govuk-table__cell");
                    break;
                case TableRow:
                    attributes.AddClass("govuk-table__row");
                    break;
            }
        }
    }
}