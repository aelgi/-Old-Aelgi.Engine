using Aelgi.Engine.Renderers.Processors;
using Markdig.Renderers;
using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Aelgi.Engine.Renderers
{
    public class BlazorRenderer : RendererBase
    {
        protected List<RenderFragment> _frags = new List<RenderFragment>();

        public BlazorRenderer() : base()
        {
            ObjectRenderers.Add(new HeadingRenderer());
            ObjectRenderers.Add(new LiteralInlineRenderer());
        }

        public void AddFragment(RenderFragment f)
        {
            _frags.Add(f);
        }

        public override object Render(MarkdownObject markdownObject)
        {
            Write(markdownObject);

            RenderFragment fragment = (builder) =>
            {
                for (var i = 0; i < _frags.Count; i++)
                    builder.AddContent(i, _frags[i]);
            };

            return fragment;
        }

        public RenderFragment RenderFragment(MarkdownObject markdownObject)
        {
            return (RenderFragment)Render(markdownObject);
        }
    }
}
