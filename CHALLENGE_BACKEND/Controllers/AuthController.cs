using CHALLENGE_BACKEND.Moldels;
using CHALLENGE_BACKEND.Moldels.Dtos;
using CHALLENGE_BACKEND.Repository.IRepository;
using CHALLENGE_BACKEND.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CHALLENGE_BACKEND.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginRepository _LService;
        private readonly IConfiguration _Configuration;
        public AuthController(ILoginRepository lService, IConfiguration configuration)
        {
            _LService = lService;
            _Configuration = configuration;
        }
        [HttpPost("login")]
        //[Route("/login")]
        public async Task<IActionResult> Post(UsuarioAuthDto authDto)
        {
            try
            {
                var user = await _LService.ValidarUsuario(authDto.NombreUsuario);
                if (user == null) return BadRequest(new { message = "Usuario  incorrecto!!" });
                bool match = _LService.CheckPassword(user, authDto.Password);
                if (match == false) return BadRequest(new { message = "Contraseña  incorrecta!!" });
                string token = JwtConfiguration.GetToken(user, _Configuration);
                return Ok(new { message = "Contraseña Valida,Bienvenido!!", Token = token });
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registar(UsuarioAuthDto usuarioAuthDto)
        {
            try
            {
                Usuario usuario = new()
                {
                    NombreUsuario = usuarioAuthDto.NombreUsuario
                };
                var isuser = await _LService.ExisteUsuario(usuario);
                if (isuser == true) return BadRequest(new { messege = "Usuario ya existe!" });
                await _LService.RegistrarUsuario(usuario, usuarioAuthDto.Password);
                return Ok(new { message = "Usuario creado con exito!" });

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

    }
}
