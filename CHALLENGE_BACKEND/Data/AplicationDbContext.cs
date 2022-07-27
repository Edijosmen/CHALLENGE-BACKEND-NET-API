using CHALLENGE_BACKEND.Moldels;
using Microsoft.EntityFrameworkCore;

namespace CHALLENGE_BACKEND.Data
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options)
        {

        }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Personaje> Personajes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<PersonajePelicula> PersonajePeliculas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }

  
}
