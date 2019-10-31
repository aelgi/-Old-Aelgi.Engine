using Aelgi.Engine.Renderers;
using Markdig;
using Microsoft.AspNetCore.Components;
using System.IO;
using System.Threading.Tasks;

namespace Aelgi.Engine.Services
{
    public interface IBlogService
    {
        Task<RenderFragment> LoadBlog(string name);
    }

    public class BlogService : IBlogService
    {
        protected MarkdownPipeline _pipeline;

        public BlogService()
        {
            _pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();
        }

        public async Task<RenderFragment> LoadBlog(string name)
        {
            var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "blog", $"{name}.md");
            var data = await File.ReadAllTextAsync(fullFilePath);
            var output = Markdown.Parse(data);

            var renderer = new BlazorRenderer();
            _pipeline.Setup(renderer);
            var res = renderer.RenderFragment(output);

            return res;
        }
    }
}
