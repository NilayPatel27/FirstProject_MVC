using FirstProject.Data.Base;
using FirstProject.Models;

namespace FirstProject.Data.Services
{
    public interface IDirectorsService
    {
        Task<IEnumerable<Director>> GetAllAsync();
        Task<Director> GetByIdAsync(int id);
        Task AddAsync(Director director);
        Task UpdateAsync(int id, Director newDirector);
        Task DeleteAsync(int id);
    }

}
