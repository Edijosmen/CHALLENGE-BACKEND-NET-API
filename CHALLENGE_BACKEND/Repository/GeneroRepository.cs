using CHALLENGE_BACKEND.Data;
using CHALLENGE_BACKEND.Moldels;
using CHALLENGE_BACKEND.Repository.IRepository;

namespace CHALLENGE_BACKEND.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly AplicationDbContext _Dbcontext;
        public GeneroRepository(AplicationDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public async Task CrearGenero(Genero genero)
        {
            _Dbcontext.Generos.Add(genero);
            await _Dbcontext.SaveChangesAsync();
        }
    }
}
