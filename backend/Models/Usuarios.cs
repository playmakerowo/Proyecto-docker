using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Prueba")]
    public class User
    {
        [Column("prueba_id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Name { get; set; }

        [Column("edad")]
        public int Age { get; set; }
    }

    public class Images
    {
        public int Id { get; set; }
        public string? NombreImagen { get; set; }
        public byte[]? Imagen { get; set; }
    }
}