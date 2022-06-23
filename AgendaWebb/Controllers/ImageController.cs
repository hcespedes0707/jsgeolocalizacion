using AgendaWeb.Data;
using AgendaWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AgendaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ImageController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var imageId = 0;
            var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = Path.GetFileName(fileContent.FileName.ToString());
            var randomFileName = Path.GetRandomFileName();
            var folderPath = _configuration["ApplicationFilesPath"];

            var path = folderPath + randomFileName;

            using (var fileStream = new FileStream(path, FileMode.Create))
            {

                await file.CopyToAsync(fileStream);

                Imagen img = new Imagen()
                {
                    FechaSubida = DateTime.Now,
                    FileName = fileName,
                    Path = path,
                    Temporal = true
                };
                await _dbContext.Imagen.AddAsync(img);
                await _dbContext.SaveChangesAsync();

                imageId = img.ImagenId;
            }


            return Ok(imageId);
        }


        [HttpGet]
        [Route("{imageId}")]
        public IActionResult GetImage(int imageId)
        {
            Imagen img = _dbContext.Imagen.FirstOrDefault(x => x.ImagenId == imageId);
            if(img == null)
            {
                return NotFound();
            }
            byte[] imageContent = System.IO.File.ReadAllBytes(img.Path);
            //foto.png => image/png
            return File(imageContent, GetMimeType(img.FileName));
        }

        public string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
