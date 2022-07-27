namespace CHALLENGE_BACKEND.Moldels.Dtos
{
    public class CrearPeliculaDto
    {
     
        public IFormFile Imagen { get; set; }

        public string Titulo { get; set; }
   
        public DateTime FechaCreacion { get; set; }
        public enum TypeClasificacion { uno = 1, dos = 2, tres = 3, cuatro = 4, cinco = 5 }
        public TypeClasificacion Clasificacion { get; set; }
        public int GeneroId { get; set; }
        //public List<PersonajePelicula> Ppersonaje { get; set; }

    }
}
