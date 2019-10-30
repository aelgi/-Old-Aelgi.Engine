using Markdig;
using System.IO;
using System.Threading.Tasks;

namespace Aelgi.Engine.Services
{
    public interface IBlogService
    {
        Task<string> LoadBlog(string name);
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

        public async Task<string> LoadBlog(string name)
        {
            var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "blog", $"{name}.md");

            var data = await File.ReadAllTextAsync(fullFilePath);
            return Markdown.ToHtml(data, _pipeline);
        }
    }
}
