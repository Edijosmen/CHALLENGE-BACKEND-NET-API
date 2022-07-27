using CHALLENGE_BACKEND.Data;
using CHALLENGE_BACKEND.Moldels;
using CHALLENGE_BACKEND.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CHALLENGE_BACKEND.Repository
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly AplicationDbContext _Dbcontext;
        public PeliculaRepository(AplicationDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public async Task Actualizar(Pelicula pelicula)
        {
            _Dbcontext.Entry(pelicula).State = EntityState.Modified;
           
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task<Pelicula> BuscarPelicula(int idpel)
        {
            var pelicula= await _Dbcontext.Peliculas.Where(x=>x.Id==idpel).FirstOrDefaultAsync();
            return pelicula;
        }

        public async Task Eliminar(Pelicula pelicula)
        {
            _Dbcontext.Remove(pelicula);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task GuardarPelicula(Pelicula pelicula)
        {
            _Dbcontext.Add(pelicula);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task<List<Pelicula>> ListaPelicula()
        {
            var lista = await _Dbcontext.Peliculas.OrderBy(p => p.Id).Include(y=>y.Ppersonaje).ToListAsync();
            return lista;   
        }

        public async Task<Pelicula> ListaPeliculaDetalle(int id)
        {
            var peli = await _Dbcontext.Peliculas.Where(x=>x.Id==id).Include(y=>y.Ppersonaje).ThenInclude(x=>x.Personaje).FirstOrDefaultAsync();
            return peli;
        }

        public async Task<List<Pelicula>> ListaPeliculaGenero(int gen)
        {
            var pelis = await _Dbcontext.Peliculas.Where(x => x.GeneroId == gen).ToListAsync();
            return pelis;
        }

        public async Task<List<Pelicula>> ListaPeliculaName(string name)
        {
            var pelis = await _Dbcontext.Peliculas.Where(x => x.Titulo == name).ToListAsync();
            return pelis;
        }

        public async Task<List<Pelicula>> ListaPeliculaOrden(string orden)
        {
            if (orden == "ASC")
            {
                var pelis = await _Dbcontext.Peliculas.OrderBy(x=>x.Titulo).ToListAsync();
                return pelis;
            }else
            {
                var pelis = await _Dbcontext.Peliculas.OrderByDescending(x => x.Titulo).ToListAsync();
                return pelis;
            }
        }
    }
}
