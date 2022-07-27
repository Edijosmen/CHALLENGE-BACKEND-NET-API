using CHALLENGE_BACKEND.Moldels;

namespace CHALLENGE_BACKEND.Repository.IRepository
{
    public interface ILoginRepository
    {
        Task<Usuario> ValidarUsuario(string usuario);
        bool CheckPassword(Usuario usuario,string password);
        Task RegistrarUsuario(Usuario usuario, string password);
        Task<bool> ExisteUsuario(Usuario usuario);
    }
}
