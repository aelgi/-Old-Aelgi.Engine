namespace Aelgi.Markdown.Symbols
{
    public class ItalicsSymbol : Symbol
    {
        public string Content { get; }

        public ItalicsSymbol(string content)
        {
            Content = content;
        }
    }
}
