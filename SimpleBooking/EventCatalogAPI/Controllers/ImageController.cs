using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace EventCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _env;

        public ImageController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        [Route("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var webRoot = _env.WebRootPath;
                var path = Path.Combine(webRoot + "/pics/", imageName);
                var buffer = System.IO.File.ReadAllBytes(path);
                return File(buffer, "image/png");
            }

            return NoContent();
        }
    }
}
