namespace Aelgi.Markdown.Symbols
{
    public class BoldedSymbol : Symbol
    {
        public string Content { get; }

        public BoldedSymbol(string content)
        {
            Content = content;
        }
    }
}
