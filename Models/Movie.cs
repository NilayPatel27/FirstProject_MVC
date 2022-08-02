using FirstProject.Data;
using FirstProject.Data.Base;

namespace FirstProject.Models
{
    public class Movie : BaseEntity
    {
        public string? Language { get; set; }
        public string? Category { get; set; }
        public bool Status { get; set; }
        public string? Region { get; set; }
        public int Stars { get; set; }
        public DateTime ReleDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //Relatioships

        public ICollection<Actor_Movie>? Actors_Movies { get; set; }
        public ICollection<Movie_Director>? Movies_Directors { get; set; }
        

    }
}
