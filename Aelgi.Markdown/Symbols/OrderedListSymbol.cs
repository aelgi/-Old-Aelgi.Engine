using System.Collections.Generic;

namespace Aelgi.Markdown.Symbols
{
    public class OrderedListItemSymbol : Symbol
    {
        public ICollection<Symbol> Content { get; }

        public OrderedListItemSymbol(ICollection<Symbol> content)
        {
            Content = content;
        }
    }

    public class OrderedListSymbol : Symbol
    {
        public ICollection<Symbol> Items { get; }

        public OrderedListSymbol(ICollection<Symbol> items)
        {
            Items = items;
        }
    }
}
