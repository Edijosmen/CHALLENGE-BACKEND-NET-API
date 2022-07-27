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
    [Route("api/characters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonajeController : ControllerBase
    {
        private readonly IPersonajeRepository _PService;
        private readonly IAlmacenArchivoRepository _AlmacenArchivo;
        private readonly IMapper _mapper;
        const string img = "personaje";
        public PersonajeController(IPersonajeRepository pService, IMapper mapper, IAlmacenArchivoRepository AlmacenArchivo)
        {
            _PService = pService;
            _mapper = mapper;
            _AlmacenArchivo = AlmacenArchivo;  
        }

        [HttpGet]
        [Route("", Name = nameof(Get))]
        public async Task<IActionResult> Get()
        {
            try
            {
              
               var personajes= await _PService.ListaPersonaje();
                var persj = _mapper.Map<List<PersonajeDto>>(personajes);
                return Ok(persj);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet("detalle/{id:int}")]
        public async Task<IActionResult> GetDetalle(int id)
        {
            try
            {

                var personajes = await _PService.ListaPersonajeDetalle(id);
                //var persj = _mapper.Map<List<PersonajeDto>>(personajes);
                return Ok(personajes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("name", Name = nameof(GetName))]
        public async Task<IActionResult> GetName(string name)
        {
            try
            {
                int id = 1;
                var personajes = await _PService.ListaPersonajeNombre(name);
                //var persj = _mapper.Map<List<PersonajeDto>>(personajes);
                return Ok(personajes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("edad", Name = nameof(GetEdad))]
        public async Task<IActionResult> GetEdad(int edad)
        {
            try
            {
                
                var personajes = await _PService.ListaPersonajeEdad(edad);
                //var persj = _mapper.Map<List<PersonajeDto>>(personajes);
                return Ok(personajes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("movies", Name = nameof(GetMovie))]
        public async Task<IActionResult> GetMovie(int idmovie)
        {
            try
            {
               
                var personajes = await _PService.ListaPersonajeMovie(idmovie);
                //var persj = _mapper.Map<List<PersonajeDto>>(personajes);
                return Ok(personajes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm]CrearPersonajeDto personajeDto)
        {
            try
            {
                var entity = _mapper.Map<Personaje>(personajeDto);
                
                if (personajeDto.Imagen == null) return BadRequest(new { message = "una imagen es requerida" });
                //const string img = "personaje";
               
                entity.Imagen = await _AlmacenArchivo.GuardarImagen(personajeDto.Imagen,img);
                if (!ModelState.IsValid) return BadRequest(new {message="los campos son obligatorios!"});
                await _PService.SavePersonaje(entity);
                //Task.CompletedTask;
                return Ok(new {message="creado con exito"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CrearPersonajeDto personajeDto)
        {
            try
            {
                var persj = await _PService.BuscarPersonaje(id);
                if (persj == null) return NotFound(new { messge = "no existe el personaje" });
                _mapper.Map(personajeDto, persj);
                //entity.Imagen = persj.Imagen;

                if (personajeDto.Imagen == null) return BadRequest(new { message = "Imagen requerida" });
                if (!string.IsNullOrEmpty(persj.Imagen))
                {
                    await _AlmacenArchivo.BorraArchivo(persj.Imagen, img);
                }
                string urlpath = await _AlmacenArchivo.GuardarImagen(personajeDto.Imagen, img);
                persj.Imagen = urlpath;
                await _PService.Actualizar(persj);
                return Ok(new { message = "Personaje Actualizado!!" });
            }
            catch (Exception ex)
            {

                return BadRequest(new {message=ex});
            }

        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var personaje =  await _PService.BuscarPersonaje(id);
                if (personaje == null) return NotFound(new { message = "No hay registro" });
                await _PService.Eliminar(personaje);
                return Ok(new {message="Registro Eliminado"});
            }
            catch (Exception ex)
            {

                return BadRequest(new {message=ex});
            }
            

        }
    }
}
