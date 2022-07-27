using CHALLENGE_BACKEND.Moldels;

namespace CHALLENGE_BACKEND.Repository.IRepository
{
    public interface IPeliculaRepository
    {
        Task<List<Pelicula>> ListaPelicula();
        Task<List<Pelicula>> ListaPeliculaName(string name);
        Task<List<Pelicula>> ListaPeliculaGenero(int gen);
        Task<List<Pelicula>> ListaPeliculaOrden(string orden);
        Task<Pelicula> ListaPeliculaDetalle(int id);
        Task GuardarPelicula(Pelicula pelicula);
        Task<Pelicula> BuscarPelicula(int idpel);
        Task Actualizar(Pelicula pelicula);
        Task Eliminar(Pelicula pelicula);
    }
}
