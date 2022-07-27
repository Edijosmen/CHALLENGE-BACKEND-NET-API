using AutoMapper;
using CHALLENGE_BACKEND.Moldels;
using CHALLENGE_BACKEND.Moldels.Dtos;

namespace CHALLENGE_BACKEND.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Personaje, PersonajeDto>().ReverseMap();
            CreateMap<CrearPersonajeDto, Personaje>().ForMember(x => x.Imagen, options => options.Ignore());
            CreateMap<Personaje, CrearPersonajeDto>().ForMember(x => x.Imagen, options => options.Ignore());
            CreateMap<CrearPeliculaDto, Pelicula>().ForMember(x => x.Imagen, options => options.Ignore());
            CreateMap<Pelicula, CrearPeliculaDto>().ForMember(x => x.Imagen, options => options.Ignore());
            CreateMap<CrearGeneroDto, Genero>().ForMember(x => x.Imagen, options => options.Ignore());
            CreateMap<Genero, CrearGeneroDto>().ForMember(x => x.Imagen, options => options.Ignore());
            CreateMap<peliculaDto, Pelicula>();
        }
    }
}
