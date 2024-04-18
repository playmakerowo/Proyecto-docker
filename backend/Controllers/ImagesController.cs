using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace Slamdunk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IWebHostEnvironment environment;
        public ImagesController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost]
        [Route("UploadLocal")]
        public async Task<IActionResult> UploadLocal(IFormFile formFile, string nombreImagen)
        {
            try
            {
                string Filepath = this.environment.WebRootPath + @"/Images";//ver por que se usa el webroot
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                string imagepath = Filepath + @"/" + nombreImagen + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using FileStream stream = System.IO.File.Create(imagepath);
                await formFile.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "un error a ocurrido: " + ex);
            }
            return Ok();

        }

        [HttpPost]
        [Route("MultiUploadLocal")]
        public async Task<IActionResult> MultiUploadLocal(IFormFileCollection fileCollection, string nombreImagen)  //nombre imagen se puede cambiar para usar por ejemplo
        {                                                                                                           //"nombre colleciÃ³n" y que de esta manera cree una carpeta
            try
            {
                string Filepath = this.environment.WebRootPath + @"/Images";
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                foreach (var file in fileCollection)
                {
                    string imagepath = Filepath + @"/" + file.FileName; //aqui se puede hacer que cree una carpeta segun el nombreImagen
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using FileStream stream = System.IO.File.Create(imagepath);
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "un error a ocurrido: " + ex);
            }
            return Ok();
        }

        [HttpGet("{nombreArchivo}")]
        public IActionResult Get(string nombreArchivo)
        {
            var imagePath = Path.Combine("wwwroot/Images", $"{nombreArchivo}.jpg");

            // Verificar si el archivo con extensión .jpg no existe
            if (!System.IO.File.Exists(imagePath))
            {
                // Si no existe, intentar con extensión .png
                imagePath = Path.Combine("wwwroot/Images", $"{nombreArchivo}.PNG");

                // Verificar si el archivo existe
                if (!System.IO.File.Exists(imagePath))
                {
                    // Si el archivo no existe, devolver una imagen por defecto
                    imagePath = Path.Combine("wwwroot/Images", "big chungus.jpg");
                }
            }

            // Abrir el archivo y devolverlo como respuesta
            var imageStream = System.IO.File.OpenRead(imagePath);
            return File(imageStream, "image/jpeg");
        }
    }
}
