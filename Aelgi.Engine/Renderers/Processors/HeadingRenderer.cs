using Markdig.Renderers;
using Markdig.Syntax;
using System;

namespace Aelgi.Engine.Renderers.Processors
{
    public class HeadingRenderer : MarkdownObjectRenderer<BlazorRenderer, HeadingBlock>
    {
        protected override void Write(BlazorRenderer renderer, HeadingBlock obj)
        {
            if (obj.Level <= 0 || obj.Level > 6) throw new NotImplementedException();

            obj.Inline.FirstChild

            renderer.Write(obj.Inline);

            renderer.AddFragment(builder =>
            {
                builder.OpenElement(0, $"h{obj.Level}");
                builder.AddContent(1, "Hello");
                builder.CloseElement();
            });
        }
    }
}
