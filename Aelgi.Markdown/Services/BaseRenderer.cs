using Aelgi.Markdown.IServices;
using Aelgi.Markdown.Symbols;
using System.Collections.Generic;

namespace Aelgi.Markdown.Services
{
    public abstract class BaseRenderer<T, U> : IRenderer<T>
    {
        protected ICollection<U> _nodes;

        protected abstract U RenderHeading(HeadingSymbol s);
        protected abstract U RenderNewLine(NewLineSymbol s);

        protected abstract U RenderParagraph(ParagraphSymbol s);
        protected abstract U RenderBolded(BoldedSymbol s);
        protected abstract U RenderItalics(ItalicsSymbol s);
        protected abstract U RenderPlainText(PlainTextSymbol s);
        protected abstract U RenderUnordered(UnorderedListSymbol s);
        protected abstract U RenderUnorderedItem(UnorderedListItemSymbol s);
        protected abstract U RenderOrdered(OrderedListSymbol s);
        protected abstract U RenderOrderedItem(OrderedListItemSymbol s);
        protected abstract T CombineNodes();

        protected U ProcessLine(Symbol line)
        {
            if (line is HeadingSymbol) return RenderHeading((HeadingSymbol)line);

            if (line is NewLineSymbol) return RenderNewLine((NewLineSymbol)line);

            if (line is ParagraphSymbol) return RenderParagraph((ParagraphSymbol)line);
            if (line is PlainTextSymbol) return RenderPlainText((PlainTextSymbol)line);
            if (line is BoldedSymbol) return RenderBolded((BoldedSymbol)line);
            if (line is ItalicsSymbol) return RenderItalics((ItalicsSymbol)line);

            if (line is UnorderedListSymbol) return RenderUnordered((UnorderedListSymbol)line);
            if (line is UnorderedListItemSymbol) return RenderUnorderedItem((UnorderedListItemSymbol)line);
            if (line is OrderedListSymbol) return RenderOrdered((OrderedListSymbol)line);
            if (line is OrderedListItemSymbol) return RenderOrderedItem((OrderedListItemSymbol)line);

            return default;
        }

        public T Render(DocumentSymbol symbol)
        {
            _nodes = new List<U>();

            var lines = symbol.Symbols;
            foreach (var line in lines)
            {
                var rendered = ProcessLine(line);
                if (rendered != null)
                    _nodes.Add(rendered);
            }

            return CombineNodes();
        }
    }
}
