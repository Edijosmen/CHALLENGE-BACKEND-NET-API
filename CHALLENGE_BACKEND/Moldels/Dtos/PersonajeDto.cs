using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHALLENGE_BACKEND.Moldels.Dtos
{
    public class PersonajeDto
    {
        public int Id { get; set; }
        
        public string Imagen { get; set; }
       
        public string Nombre { get; set; }
    }
}
