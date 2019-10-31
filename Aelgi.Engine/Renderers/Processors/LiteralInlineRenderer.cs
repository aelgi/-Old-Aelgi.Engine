using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace Aelgi.Engine.Renderers.Processors
{
    public class LiteralInlineRenderer : MarkdownObjectRenderer<BlazorRenderer, LiteralInline>
    {
        protected override void Write(BlazorRenderer renderer, LiteralInline obj)
        {
            var content = obj.Content;
        }
    }
}
