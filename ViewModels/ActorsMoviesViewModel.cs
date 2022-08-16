using FirstProject.Models;

namespace FirstProject.ViewModels
{
    public class ActorsMoviesViewModel
    {
        public ActorsMoviesViewModel()
        {
           
          Movies = new List<Movie>();
        }
        public List<Movie>? Movies { get; set; }

    }
}
