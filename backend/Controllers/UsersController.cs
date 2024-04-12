using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iText.Html2pdf;

namespace Slamdunk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserContext _context;

    public UsersController(UserContext context)
    {
        _context = context;
    }

    // Listar Usuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    // Generar un Usuario
    [HttpPost]
    public async Task<IActionResult> AddUser(User Usuario)
    {
        _context.Users.Add(Usuario);
        await _context.SaveChangesAsync();

        return Ok(Usuario);
    }

    //Buscar la Usuario
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var Usuario = await _context.Users.FindAsync(id);

        return Ok(Usuario);
    }

    // Pasar parametros del usuario por URL
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(User Usuario)
    {
        try
        {
            // Buscar al Usuario por su ID
            var usuarioExistente = await _context.Users.FindAsync(Usuario.Id);

            //validar que exista el usuario
            if (usuarioExistente == null)
            {
                return NotFound("Error");
            }

            // Actualizar los campos del usuario existente con los valores proporcionados
            usuarioExistente.Name = Usuario.Name;
            usuarioExistente.Age = Usuario.Age;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(); // Operación exitosa
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al actualizar la invitación: {ex.Message}");
        }
    }

    // Eliminar un usuario
    [HttpDelete("{id}")]
    public async Task<IActionResult> BorrarUsuario(int id)
    {
        try
        {
            // Buscar al Usuario por su ID
            var usuarioExistente = await _context.Users.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound(); // Usuario no encontrado
            }

            // Eliminar al usuario de la base de datos
            _context.Users.Remove(usuarioExistente);
            await _context.SaveChangesAsync();

            return Ok(); // Operación de eliminación exitosa
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al eliminar el usuario: {ex.Message}");
        }
    }

    //PDF HTML
    [HttpGet("pdfHtml")]
    public async Task<ActionResult> GetPdfhtml()
    {
        string origen = Path.Combine(Directory.GetCurrentDirectory(), "archivos-pdf/plantillas/plantilla.html");
        string salida = Path.Combine(Directory.GetCurrentDirectory(), "archivos-pdf");

        // Crear la carpeta si no existe
        Directory.CreateDirectory(salida);

        var htmlFilePath = Path.Combine(origen);
        var pdfDest = Path.Combine(salida, "plantilla.pdf");

        // Convertir HTML a PDF
        using (FileStream htmlStream = new FileStream(htmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (FileStream pdfStream = new FileStream(pdfDest, FileMode.Create))
        {
            HtmlConverter.ConvertToPdf(htmlStream, pdfStream);
        }

        // Retornar el archivo PDF generado
        byte[] pdfBytes = System.IO.File.ReadAllBytes(pdfDest);
        return File(pdfBytes, "application/pdf", "plantilla.pdf");
    }

    // Listar Usuario pdf
    [HttpGet("pdf")]
    public async Task<ActionResult> GetPdf()
    {
        var Usuarios = await _context.Users.ToListAsync();
        string fileName = $"ListOfUsers.pdf";
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "archivos-pdf");
        string filePath = Path.Combine(directoryPath, fileName);

        // Crear la carpeta si no existe
        Directory.CreateDirectory(directoryPath);

        // Crear el contenido del PDF y guardarlo en disco
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            doc.Add(new Paragraph("Detalle del Usuario"));
            foreach (var usuario in Usuarios)
            {
                doc.Add(new Paragraph($"Id Usuario: {usuario.Id}"));
                doc.Add(new Paragraph($"Nombre: {usuario.Name}, Edad: {usuario.Age}")); // Suponiendo que la clase User tiene propiedades Nombre y Edad
                doc.Add(new Paragraph($" "));
            }
            doc.Close();
        }

        // Devolver el archivo PDF como resultado
        return PhysicalFile(filePath, "application/pdf", fileName);
    }

    //Usuario pdf
    [HttpGet("pdf" + "{id}")]
    public async Task<ActionResult> GetPdf(int id)
    {
        var Usuario = await _context.Users.FindAsync(id);
        string fileName = $"Detalle-{Usuario.Name}.pdf";
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "archivos-pdf");
        string filePath = Path.Combine(directoryPath, fileName);

        // Crear la carpeta si no existe
        Directory.CreateDirectory(directoryPath);

        // Crear el contenido del PDF y guardarlo en disco
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            doc.Add(new Paragraph("Detalle del Usuario"));
            doc.Add(new Paragraph($"Nombre: {Usuario.Name}"));
            doc.Add(new Paragraph($"Edad: {Usuario.Age}"));
            doc.Add(new Paragraph($"ID: {Usuario.Id}"));
            doc.Close();
        }

        // Devolver el archivo PDF como resultado
        return PhysicalFile(filePath, "application/pdf", fileName);
    }

}