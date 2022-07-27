using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHALLENGE_BACKEND.Moldels.Dtos
{
    public class UsuarioAuthDto
    {
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }
}
