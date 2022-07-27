
using CHALLENGE_BACKEND.Data;
using CHALLENGE_BACKEND.Moldels;
using CHALLENGE_BACKEND.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CHALLENGE_BACKEND.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AplicationDbContext _Dbcontext;
        public LoginRepository(AplicationDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        public  bool CheckPassword(Usuario usuario, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(usuario.PasswordSalt))
            {
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //var DATO = hashComputado.SequenceEqual(passwordHash);
                //return DATO;
                Console.WriteLine(hashComputado);
                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != usuario.PasswordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<Usuario> ValidarUsuario(string usuario)
        {
            var isUser = await _Dbcontext.Usuarios.Where(x => x.NombreUsuario == usuario).FirstOrDefaultAsync();
            if (isUser != null)
            {
                return isUser;
            }
            return null;
        }

        public async Task<bool> ExisteUsuario(Usuario usuario)
        {
            var user = await _Dbcontext.Usuarios.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario);
            return user;
        }

        public async Task RegistrarUsuario(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSalt;
            CrearPasswordEncript(out passwordHash, out passwordSalt, password);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _Dbcontext.Usuarios.Add(usuario);
            await _Dbcontext.SaveChangesAsync();
        }

        private void CrearPasswordEncript(out byte[] passwordHash, out byte[] passwordSalt, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


    }
}
