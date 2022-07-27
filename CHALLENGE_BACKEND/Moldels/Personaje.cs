using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHALLENGE_BACKEND.Moldels
{
    public class Personaje
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Imagen { get; set; }
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Nombre { get; set; }
        [Required]
        public int  Edad { get; set; }
        [Required]
        public int Peso { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Historia { get; set; }
      
        public List<PersonajePelicula> Ppelicula { get; set; }

    }
}
