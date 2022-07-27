using AutoMapper;
using CHALLENGE_BACKEND.Moldels;
using CHALLENGE_BACKEND.Moldels.Dtos;
using CHALLENGE_BACKEND.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CHALLENGE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _Gservice;
        private readonly IMapper _mapper;
        private readonly IAlmacenArchivoRepository _AlmacenArchivo;
        const string img = "Genero";
        public GeneroController(IGeneroRepository gservice,IMapper mapper, IAlmacenArchivoRepository almacenArchivo)
        {
            _Gservice = gservice;
            _mapper = mapper;
            _AlmacenArchivo = almacenArchivo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CrearGeneroDto crearGeneroDto)
        {
            try
            {
                var entity = _mapper.Map<Genero>(crearGeneroDto);

                if (crearGeneroDto.Imagen == null) return BadRequest(new { message = "una imagen es requerida" });
        

                entity.Imagen = await _AlmacenArchivo.GuardarImagen(crearGeneroDto.Imagen, img);

                if (!ModelState.IsValid) return BadRequest(new { message = "los campos son obligatorios!" });
                await _Gservice.CrearGenero(entity);
         
                return Ok(new { message = "creado con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
