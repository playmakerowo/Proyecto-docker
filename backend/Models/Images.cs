using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Images
    {
        public int Id { get; set; }
        public string? NombreImagen { get; set; }
        public byte[]? Imagen { get; set; }
    }
}