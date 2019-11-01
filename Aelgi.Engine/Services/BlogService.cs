using Aelgi.Engine.Renderers;
using Aelgi.Markdown.IServices;
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
        protected IParser _parser;

        public BlogService(IParser parser)
        {
            _parser = parser;
        }

        public async Task<RenderFragment> LoadBlog(string name)
        {
            var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "blog", $"{name}.md");
            var data = await File.ReadAllTextAsync(fullFilePath);

            var doc = _parser.Parse(data);
            var renderer = new BlazorRenderer();
            var res = renderer.Render(doc);

            return res;
        }
    }
}
