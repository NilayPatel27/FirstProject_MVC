using FirstProject.Data.Base;
using FirstProject.Models;
using FirstProject.ViewModels;

namespace FirstProject.Data.Services
{
    public interface IMoviesService 
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task AddAsync(NewMovieVM movie);
        Task UpdateAsync(int id, Movie movie);
        Task DeleteAsync(int id);
        Task<NewMovieViewModel> GetNewMovieData();

    }
    
}
