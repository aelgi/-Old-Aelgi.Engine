using LibSassHost;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Aelgi.Engine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SassController : Controller
    {
        [Route("{fileName}")]
        //[Produces("text/css")]
        //[Consumes("text/css")]
        public ContentResult SassConverter(string fileName)
        {
            var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sass", $"{fileName}.scss");
            var result = SassCompiler.CompileFile(fullFilePath, options: new CompilationOptions
            {
                SourceMap = false,
                OutputStyle = OutputStyle.Compact,
                SourceComments = false,
            });

            //Response.ContentType = "text/css";
            return Content(result.CompiledContent, "text/css");
            //return result.CompiledContent;
        }
    }
}