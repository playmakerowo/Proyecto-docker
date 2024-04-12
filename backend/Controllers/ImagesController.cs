using Microsoft.AspNetCore.Mvc;
using Models;


namespace Slamdunk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        [HttpPost]
        [Route("UploadLocal")]
        public async Task<ActionResult> UploadFileLocal([FromForm] FileModel fileModel)
        {
            try
            {
                string path = Path.Combine(@"/app/Slamdunk.WebApi/images", fileModel.FileName + ".jpg");
                using(Stream stream = new FileStream(path, FileMode.Create))
                {
                    fileModel.File.CopyTo(stream);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Un error a ocurrido: " + ex);
            }
        }
        // [HttpPost]
        // [Route("UploadDB")]
        // agregar el metodo para guardar las imagenes en la bd,
        // si es necesario crear otra tabla para ese proposito ya que el IFormFile no lo pesca la wea de migraciones
    }
}
