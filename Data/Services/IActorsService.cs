using FirstProject.Data.Base;
using FirstProject.Models;
using FirstProject.ViewModels;

namespace FirstProject.Data.Services
{
    public interface IActorsService 
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetByIdAsync(int id);
        Task<ActorsMoviesViewModel> ActorsMoviesViewModelAsync(int id);
        Task AddAsync(Actor actor);
        Task UpdateAsync(int id, Actor newActor);
        Task DeleteAsync(int id);
    }
}
