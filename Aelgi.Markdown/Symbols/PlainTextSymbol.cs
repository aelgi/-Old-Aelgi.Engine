namespace Aelgi.Markdown.Symbols
{
    public class PlainTextSymbol : Symbol
    {
        public string Content { get; }

        public PlainTextSymbol(string content)
        {
            Content = content;
        }
    }
}
