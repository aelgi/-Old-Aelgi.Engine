using Aelgi.Markdown.Symbols;

namespace Aelgi.Markdown.IServices
{
    public interface IParser
    {
        DocumentSymbol Parse(string content);
    }
}
