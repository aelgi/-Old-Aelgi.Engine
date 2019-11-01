using Aelgi.Markdown.IServices;
using Aelgi.Markdown.Symbols;
using System.Collections.Generic;
using System.Linq;

namespace Aelgi.Markdown.Services
{
    public class Parser : IParser
    {
        protected Queue<string> _lines;
        protected List<Symbol> _symbols;

        protected bool ProcessHeading()
        {
            var line = _lines.Peek();

            if (line.StartsWith("#"))
            {
                int i;
                for (i = 0; i < line.Count(); i++)
                    if (line[i] != '#')
                        break;

                if (i == line.Count() - 1) return false;

                var title = line.Substring(i + 1);
                _symbols.Add(new HeadingSymbol(i, new PlainTextSymbol(title)));
                _lines.Dequeue();

                return true;
            }

            return false;
        }

        protected bool ProcessNewLine()
        {
            var line = _lines.Peek();

            if (line.Trim().Count() == 0)
            {
                _symbols.Add(new NewLineSymbol());

                _lines.Dequeue();
                return true;
            }
            return false;
        }

        protected bool ProcessParagraph()
        {
            var line = _lines.Dequeue();

            var content = new List<Symbol>();
            content.Add(new PlainTextSymbol(line));

            _symbols.Add(new ParagraphSymbol(content));

            return true;
        }

        protected void ProcessLines()
        {
            if (ProcessHeading()) return;

            if (ProcessNewLine()) return;
            if (ProcessParagraph()) return;

            // Unsupported line, I need to report this somehow
            _lines.Dequeue();
        }

        public DocumentSymbol Parse(string content)
        {
            var lines = content.Split('\n').Select(x => x.TrimEnd());
            _lines = new Queue<string>(lines);
            _symbols = new List<Symbol>();

            while (_lines.Count > 0) ProcessLines();

            var doc = new DocumentSymbol(_symbols);

            return doc;
        }
    }
}
