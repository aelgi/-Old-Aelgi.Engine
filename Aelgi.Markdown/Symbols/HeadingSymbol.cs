namespace Aelgi.Markdown.Symbols
{
    public class HeadingSymbol : Symbol
    {
        public int Depth { get; }
        public PlainTextSymbol Title { get; }

        public HeadingSymbol(int depth, PlainTextSymbol title)
        {
            Depth = depth;
            Title = title;
        }
    }
}
