using System;
namespace Aelgi.Markdown.Symbols
{
    public class LinkSymbol : Symbol
    {
        public string Location { get; }
        public string Title { get; }

        public LinkSymbol(string location, string title)
        {
            Location = location;
            Title = title;
        }
    }
}
