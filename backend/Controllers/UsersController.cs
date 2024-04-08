using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> UpdateUsuario(int id, string name, int edad)
    {
        try
        {
            // Buscar al Usuario por su ID
            var usuarioExistente = await _context.Users.FindAsync(id);

            //validar que exista el usuario
            if (usuarioExistente == null)
            {
                return NotFound("Error");
            }

            // Actualizar los campos del usuario existente con los valores proporcionados
            usuarioExistente.Name = name;
            usuarioExistente.Age = edad;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(usuarioExistente + "modificado con exito"); // Operación exitosa
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
}