using Aelgi.Markdown.Services;
using Aelgi.Markdown.Symbols;
using Microsoft.AspNetCore.Components;

namespace Aelgi.Engine.Renderers
{
    public class BlazorRenderer : BaseRenderer<RenderFragment, RenderFragment>
    {
        protected override RenderFragment CombineNodes()
        {
            return (builder) =>
            {
                var i = 0;
                foreach (var node in _nodes)
                {
                    builder.AddContent(i++, node);
                }
            };
        }

        protected override RenderFragment RenderHeading(HeadingSymbol s)
        {
            return (builder) =>
            {
                builder.OpenElement(0, $"h{s.Depth}");
                builder.AddContent(1, s.Title.Content);
                builder.CloseElement();
            };
        }

        protected override RenderFragment RenderNewLine(NewLineSymbol s)
        {
            return (builder) =>
            {
                builder.AddMarkupContent(0, "<br />");
            };
        }

        protected override RenderFragment RenderParagraph(ParagraphSymbol s)
        {
            return (builder) =>
            {
                var i = 0;
                builder.OpenElement(i++, "p");

                foreach (var el in s.Content)
                    builder.AddContent(i++, ProcessLine(el));

                builder.CloseElement();
            };
        }

        protected override RenderFragment RenderPlainText(PlainTextSymbol s)
        {
            return (builder) =>
            {
                builder.AddContent(0, s.Content);
            };
        }
    }
}
