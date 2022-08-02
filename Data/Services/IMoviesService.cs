using FirstProject.Data.Base;
using FirstProject.Models;

namespace FirstProject.Data.Services
{
    public interface IMoviesService 
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(int id, Movie movie);
        Task DeleteAsync(int id);
    }
    
}
