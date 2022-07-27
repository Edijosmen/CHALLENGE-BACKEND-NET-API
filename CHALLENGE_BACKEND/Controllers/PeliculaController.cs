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
    [Route("api/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PeliculaController : ControllerBase
    {
        private readonly IPeliculaRepository _Pservice;
        private readonly IAlmacenArchivoRepository _AlmacenArchivo;
        private readonly IMapper _mapper;
        const string img = "Pelicula";
        public PeliculaController(IPeliculaRepository pservice, IMapper mapper, IAlmacenArchivoRepository AlmacenArchivo)
        {
            _Pservice = pservice;
            _mapper = mapper;
            _AlmacenArchivo = AlmacenArchivo;
        }

        [HttpGet("GET/movies")]
        public async Task<IActionResult> Get()
        {
            try
            {
                
                var peliculas= await _Pservice.ListaPelicula();
                var pelis = _mapper.Map<List<peliculaDto>>(peliculas);
                return Ok(pelis);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message=ex});
            }
        }

        [HttpGet]
        [Route("movies/name", Name = nameof(GetNameMovie))]
        public async Task<IActionResult> GetNameMovie(string name)
        {
            try
            {
                
                var peliculas = await _Pservice.ListaPeliculaName(name);
                return Ok(peliculas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("movies/genro", Name = nameof(GetGenero))]
        public async Task<IActionResult> GetGenero(int genro)
        {
            try
            {

                var peliculas = await _Pservice.ListaPeliculaGenero(genro);
                //var persj = _mapper.Map<List<PersonajeDto>>(personajes);
                return Ok(peliculas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("movies/order", Name = nameof(GetOrden))]
        public async Task<IActionResult> GetOrden(string order)
        {
            try
            {

                var peliculas = await _Pservice.ListaPeliculaOrden (order);
                //var persj = _mapper.Map<List<PersonajeDto>>(personajes);
                return Ok(peliculas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost("Crear/movies")]
        public async Task<IActionResult> Create([FromForm] CrearPeliculaDto crearPeliculaDto)
        {
            try
            {
                var entity = _mapper.Map<Pelicula>(crearPeliculaDto);

                if (crearPeliculaDto.Imagen == null) return BadRequest(new { message = "una imagen es requerida" });
                //const string img = "personaje";

                entity.Imagen = await _AlmacenArchivo.GuardarImagen(crearPeliculaDto.Imagen, img);
                if (!ModelState.IsValid) return BadRequest(new { message = "los campos son obligatorios!" });
                await _Pservice.GuardarPelicula(entity);
                //Task.CompletedTask;
                return Ok(new { message = "creado con exito" });
            }
            catch (Exception ex)
            {

                return BadRequest(new {message=ex});
            }
        }

        [HttpPut("update/movies/{id:int}")]
        public async Task<IActionResult> update( int id, [FromForm] CrearPeliculaDto crearPeliculaDto)
        {
            try
            {
                var persj = await _Pservice.BuscarPelicula(id);
                if (persj == null) return NotFound(new { messge = "no existe el personaje" });
                _mapper.Map(crearPeliculaDto, persj);
                

                if (crearPeliculaDto.Imagen == null) return BadRequest(new { message = "Imagen requerida" });
                if (!string.IsNullOrEmpty(persj.Imagen))
                {
                    await _AlmacenArchivo.BorraArchivo(persj.Imagen, img);
                }
                string urlpath = await _AlmacenArchivo.GuardarImagen(crearPeliculaDto.Imagen, img);
                persj.Imagen = urlpath;
                await _Pservice.Actualizar(persj);
                return Ok(new { message = "Personaje Actualizado!!" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex });
            }
        }

        [HttpDelete("delete/movies/{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var personaje = await _Pservice.BuscarPelicula(id);
                if (personaje == null) return NotFound(new { message = "No hay registro" });
                await _Pservice.Eliminar(personaje);
                return Ok(new { message = "Registro Eliminado" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex });
            }


        }
        [HttpGet("detalle/{id:int}")]
        public async Task<IActionResult> GetDetalle(int id)
        {
            try
            {

                var pelicula = await _Pservice.ListaPeliculaDetalle(id);
                //var persj = _mapper.Map<List<PersonajeDto>>(personajes);
                return Ok(pelicula);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
    
}
