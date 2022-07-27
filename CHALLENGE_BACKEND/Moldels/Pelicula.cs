using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHALLENGE_BACKEND.Moldels
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Imagen { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Titulo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        public enum TypeClasificacion { uno=1,dos=2,tres=3,cuatro=4, cinco=5 }
        public TypeClasificacion Clasificacion { get; set; }
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        public List<PersonajePelicula>  Ppersonaje { get; set; }
    }
}
