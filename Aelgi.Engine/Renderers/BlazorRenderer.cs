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

        protected override RenderFragment RenderBolded(BoldedSymbol s)
        {
            return (builder) =>
            {
                builder.OpenElement(0, "b");
                builder.AddContent(1, s.Content);
                builder.CloseElement();
            };
        }

        protected override RenderFragment RenderItalics(ItalicsSymbol s)
        {
            return (builder) =>
            {
                builder.OpenElement(0, "i");
                builder.AddContent(1, s.Content);
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

        protected override RenderFragment RenderUnordered(UnorderedListSymbol s)
        {
            return (builder) =>
            {
                var i = 0;
                builder.OpenElement(i++, "ul");
                foreach (var el in s.Items)
                    builder.AddContent(i++, ProcessLine(el));
                builder.CloseElement();
            };
        }

        protected override RenderFragment RenderUnorderedItem(UnorderedListItemSymbol s)
        {
            return (builder) =>
            {
                var i = 0;
                builder.OpenElement(i++, "li");
                foreach (var el in s.Content)
                    builder.AddContent(i++, ProcessLine(el));
                builder.CloseElement();
            };
        }

        protected override RenderFragment RenderOrdered(OrderedListSymbol s)
        {
            return (builder) =>
            {
                var i = 0;
                builder.OpenElement(i++, "ol");
                foreach (var el in s.Items)
                    builder.AddContent(i++, ProcessLine(el));
                builder.CloseElement();
            };
        }

        protected override RenderFragment RenderOrderedItem(OrderedListItemSymbol s)
        {
            return (builder) =>
            {
                var i = 0;
                builder.OpenElement(i++, "li");
                foreach (var el in s.Content)
                    builder.AddContent(i++, ProcessLine(el));
                builder.CloseElement();
            };
        }

        protected override RenderFragment RenderPageBreak(PageBreakSymbol s)
        {
            return (builder) =>
            {
                builder.AddMarkupContent(0, "<hr />");
            };
        }

        protected override RenderFragment RenderLink(LinkSymbol s)
        {
            return (builder) =>
            {
                builder.OpenElement(0, "i");
                builder.AddAttribute(1, "href", s.Location);
                builder.AddContent(2, s.Title);
                builder.CloseElement();
            };
        }
    }
}
