using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHALLENGE_BACKEND.Moldels
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Imagen { get; set; }
        public ICollection<Pelicula> listaPelicula { get; set; }
        
    }
}
