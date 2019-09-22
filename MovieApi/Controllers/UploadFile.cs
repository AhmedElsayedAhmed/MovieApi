using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    public class UploadFileController : Controller
    {
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
        private readonly IHostingEnvironment host;

        public UploadFileController(IHostingEnvironment host)
        {
            this.host = host;
        }

        // POST api/<controller>
        [HttpPost]
        public JsonResult Post()
        {
            var file = Request.Form.Files[0];
            var ImageModel = Upload(file);
            return Json(ImageModel.Result.FileName);
        }


        private async Task<Image> Upload(IFormFile filesData)
        {
            if (filesData == null)
                return null;
            if (filesData.Length == 0)
            {
                return null;
            }
            if (filesData.Length > 10 * 1024 * 1024) return null;
            if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(filesData.FileName).ToLower())) return null;
            var uploadFilesPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFilesPath))
                Directory.CreateDirectory(uploadFilesPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(filesData.FileName);
            var filePath = Path.Combine(uploadFilesPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await filesData.CopyToAsync(stream);
            }
            var Image = new Image { FileName = fileName };
            return Image;
        }
    }
}
