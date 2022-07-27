using CHALLENGE_BACKEND.Data;
using CHALLENGE_BACKEND.Moldels;
using CHALLENGE_BACKEND.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CHALLENGE_BACKEND.Repository
{
    public class PersonajeRepository : IPersonajeRepository
    {
        private readonly AplicationDbContext _Dbcontext;
        public PersonajeRepository(AplicationDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        public async Task<List<Personaje>> ListaPersonaje()
        {
            var lisPersonaje = await _Dbcontext.Personajes.OrderBy(x=>x.Nombre).ToListAsync();
            return lisPersonaje;
        }

        public async Task SavePersonaje(Personaje personaje)
        {
             _Dbcontext.Add(personaje);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task<Personaje> BuscarPersonaje(int idper)
        {
            var persj = await _Dbcontext.Personajes.Where(x => x.Id == idper).FirstOrDefaultAsync();
            return persj;
        }

        public async Task Actualizar(Personaje personaje)
        {
            _Dbcontext.Entry(personaje).State = EntityState.Modified;
            //_Dbcontext.Personajes.Update(personaje);
            await _Dbcontext.SaveChangesAsync();
            
        }

        public async Task Eliminar(Personaje personaje)
        {
            _Dbcontext.Remove(personaje);
            await _Dbcontext.SaveChangesAsync(); 
        }

        public async Task<Personaje> ListaPersonajeDetalle(int id)
        {
            var persoj = await _Dbcontext.Personajes.Where(x=>x.Id==id).Include(x=>x.Ppelicula).ThenInclude(x=>x.Pelicula).FirstOrDefaultAsync();
            return persoj;
        }

        public async Task<List<Personaje>> ListaPersonajeEdad(int edad)
        {
            var perj = await _Dbcontext.Personajes.Where(x => x.Edad == edad).ToListAsync();
            return perj;
        }

        public async Task<List<Personaje>> ListaPersonajeNombre(string nombre)
        {
            var perj = await _Dbcontext.Personajes.Where(x => x.Nombre == nombre).ToListAsync();
            return perj;
        }

        public async Task<List<PersonajePelicula>> ListaPersonajeMovie(int idmovi)
        {
            var perj =await _Dbcontext.PersonajePeliculas.Where(x => x.PeliculaId == idmovi).Include(x => x.Personaje).ToListAsync();
            return perj;
        }
    }
}
