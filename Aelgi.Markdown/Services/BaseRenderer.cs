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
        protected abstract U RenderPlainText(PlainTextSymbol s);
        protected abstract T CombineNodes();

        protected U ProcessLine(Symbol line)
        {
            if (line is HeadingSymbol) return RenderHeading((HeadingSymbol)line);
            if (line is NewLineSymbol) return RenderNewLine((NewLineSymbol)line);
            if (line is ParagraphSymbol) return RenderParagraph((ParagraphSymbol)line);
            if (line is PlainTextSymbol) return RenderPlainText((PlainTextSymbol)line);

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
