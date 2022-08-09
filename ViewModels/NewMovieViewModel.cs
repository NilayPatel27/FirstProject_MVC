using FirstProject.Models;

namespace FirstProject.ViewModels
{
    public class NewMovieViewModel
    {
        public NewMovieViewModel()
        {
            Directors = new List<Director>();
            Movies = new List<Movie>();
            Actors = new List<Actor>();
        }
        public List<Actor>? Actors { get; set; }

        public List<Director>? Directors { get; set; }
        public List<Movie>? Movies { get; set; }


    }
}
