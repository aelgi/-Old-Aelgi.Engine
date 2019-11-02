using System;
using System.Collections.Generic;
using System.Linq;
using Aelgi.Markdown.IServices;
using Aelgi.Markdown.Symbols;

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

        public enum ParagraphState
        {
            None,
            Bold,
            Italics
        }

        protected ICollection<Symbol> ParseTextContent(string line)
        {
            var content = new List<Symbol>();

            var currentGroup = "";
            var isEscaped = false;
            var currentState = ParagraphState.None;
            for (var i = 0; i < line.Length; i++)
            {
                if (isEscaped)
                {
                    isEscaped = false;
                    continue;
                }

                if (line[i] == '\\')
                {
                    isEscaped = true;
                    continue;
                }

                if (line[i] == '*' || line[i] == '_')
                {
                    if ((i + 1) < line.Length && line[i + 1] == line[i])
                    {
                        i++;
                        if (currentState == ParagraphState.Bold)
                        {
                            content.Add(new BoldedSymbol(currentGroup));
                            currentGroup = "";
                            currentState = ParagraphState.None;
                        }
                        else if (currentState == ParagraphState.None)
                        {
                            content.Add(new PlainTextSymbol(currentGroup));
                            currentGroup = "";
                            currentState = ParagraphState.Bold;
                        }
                    }
                    else
                    {
                        if (currentState == ParagraphState.Italics)
                        {
                            content.Add(new ItalicsSymbol(currentGroup));
                            currentGroup = "";
                            currentState = ParagraphState.None;
                        }
                        else if (currentState == ParagraphState.None)
                        {
                            content.Add(new PlainTextSymbol(currentGroup));
                            currentGroup = "";
                            currentState = ParagraphState.Italics;
                        }
                    }
                    continue;
                }

                currentGroup += line[i];
            }

            if (currentGroup.Length > 0)
                content.Add(new PlainTextSymbol(currentGroup));
            return content;
        }

        protected bool ProcessParagraph()
        {
            var line = _lines.Dequeue();

            _symbols.Add(new ParagraphSymbol(ParseTextContent(line)));

            return true;
        }

        protected bool ProcessUnorderedList()
        {
            var line = _lines.Peek();
            var match = false;

            var items = new List<Symbol>();

            while (line.StartsWith("-") || line.StartsWith("+") || line.StartsWith("*"))
            {
                var spacePos = line.IndexOf(' ');
                var text = line.Substring(spacePos).Trim();
                items.Add(new UnorderedListItemSymbol(ParseTextContent(text)));

                _lines.Dequeue();
                match = true;
                line = _lines.Peek();
            }

            if (items.Count() > 0)
                _symbols.Add(new UnorderedListSymbol(items));

            return match;
        }

        protected bool ProcessOrderedList()
        {
            var line = _lines.Peek();
            var match = false;

            var items = new List<Symbol>();

            while (true)
            {
                var charLength = -1;
                for (var i = 0; i < line.Length; i++)
                    if (!Char.IsDigit(line[i]))
                    {
                        charLength = i;
                        break;
                    }

                if (charLength > 0 && charLength < line.Length && line[charLength + 1] == '.')
                {
                    var subset = line.Substring(charLength + 1).Trim();
                    items.Add(new OrderedListItemSymbol(ParseTextContent(subset)));

                    match = true;
                    _lines.Dequeue();
                    line = _lines.Peek();
                }
                else
                {
                    break;
                }
            }

            if (items.Count() > 0)
                _symbols.Add(new OrderedListSymbol(items));

            return match;
        }

        protected bool ProcessPageBreak()
        {
            var line = _lines.Peek().Trim();

            if (line.StartsWith("___") || line.StartsWith("***") || line.StartsWith("---"))
            {
                _symbols.Add(new PageBreakSymbol());
                _lines.Dequeue();
                return true;
            }

            return false;
        }

        protected void ProcessLines()
        {
            if (ProcessHeading()) return;

            if (ProcessNewLine()) return;
            if (ProcessPageBreak()) return;

            if (ProcessUnorderedList()) return;
            if (ProcessOrderedList()) return;

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
