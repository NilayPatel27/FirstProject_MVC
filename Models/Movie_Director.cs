namespace FirstProject.Models
{
    public class Movie_Director
    {
        public int DirectorId { get; set; }
        public Director? Director { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
