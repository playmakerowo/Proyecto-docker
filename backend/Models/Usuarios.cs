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

    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile File { get; set; }
    }
}