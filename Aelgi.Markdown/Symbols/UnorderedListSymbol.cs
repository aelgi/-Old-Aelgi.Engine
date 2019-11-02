using System.Collections.Generic;

namespace Aelgi.Markdown.Symbols
{
    public class UnorderedListItemSymbol : Symbol
    {
        public ICollection<Symbol> Content { get; }

        public UnorderedListItemSymbol(ICollection<Symbol> content)
        {
            Content = content;
        }
    }

    public class UnorderedListSymbol : Symbol
    {
        public ICollection<Symbol> Items { get; }

        public UnorderedListSymbol(ICollection<Symbol> items)
        {
            Items = items;
        }
    }
}
