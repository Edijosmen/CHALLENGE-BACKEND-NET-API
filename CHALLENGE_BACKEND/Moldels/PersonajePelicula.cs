using System.ComponentModel.DataAnnotations;

namespace CHALLENGE_BACKEND.Moldels
{
    public class PersonajePelicula
    {
        [Key]
        public int Id { get; set; }
        public int PersonajeId { get; set; }
        public Personaje Personaje { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
