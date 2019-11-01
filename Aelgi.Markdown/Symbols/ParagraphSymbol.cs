using System.Collections.Generic;

namespace Aelgi.Markdown.Symbols
{
    public class ParagraphSymbol : Symbol
    {
        public ICollection<Symbol> Content { get; }

        public ParagraphSymbol(ICollection<Symbol> symbols)
        {
            Content = symbols;
        }
    }
}
