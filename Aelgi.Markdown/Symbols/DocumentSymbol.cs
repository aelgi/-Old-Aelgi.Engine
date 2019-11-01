using System.Collections.Generic;

namespace Aelgi.Markdown.Symbols
{
    public class DocumentSymbol : Symbol
    {
        public ICollection<Symbol> Symbols { get; }

        public DocumentSymbol(ICollection<Symbol> symbols)
        {
            Symbols = symbols;
        }
    }
}
