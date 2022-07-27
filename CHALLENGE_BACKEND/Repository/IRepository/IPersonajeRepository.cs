using CHALLENGE_BACKEND.Moldels;

namespace CHALLENGE_BACKEND.Repository.IRepository
{
    public interface IPersonajeRepository
    {
        Task<List<Personaje>> ListaPersonaje();
        Task<Personaje> ListaPersonajeDetalle(int id);
        Task<List<Personaje>> ListaPersonajeEdad(int edad);
        Task<List<Personaje>> ListaPersonajeNombre(string nombre);
        Task<List<PersonajePelicula>> ListaPersonajeMovie(int idmovi);
        Task SavePersonaje(Personaje personaje);
        Task<Personaje> BuscarPersonaje(int idper);
        Task Actualizar(Personaje personaje);
        Task Eliminar(Personaje personaje);
    }
}
