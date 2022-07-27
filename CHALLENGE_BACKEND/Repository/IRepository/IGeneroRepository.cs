using CHALLENGE_BACKEND.Moldels;

namespace CHALLENGE_BACKEND.Repository.IRepository
{
    public interface IGeneroRepository
    {
        Task CrearGenero(Genero genero);
    }
}
