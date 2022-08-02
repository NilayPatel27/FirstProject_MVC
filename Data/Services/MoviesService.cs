using FirstProject.Data.Base;
using FirstProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FirstProject.Data.Services
{
    public class MoviesService : BaseEntity, IMoviesService
    {
        private readonly AppDbContext _context;

        public MoviesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(n => n.Id == id);
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            string storedProcedure = "spGetMovies";
            return await _context.Movies.FromSqlRaw(storedProcedure).ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            List<Movie> data = await _context.Movies.FromSqlRaw("spGetMovieById @Id", parameters).ToListAsync();
            Movie result = data.Select(x => new Movie
            {
                Id = x.Id,
                Language = x.Language,
                Category = x.Category,
                Status = x.Status,
                Region = x.Region,
                Stars = x.Stars,
                ReleDate = x.ReleDate,
                MovieCategory = x.MovieCategory,
                Name = x.Name

            })?.FirstOrDefault();
            return result;
        }

        public Task UpdateAsync(int id, Movie movie)
        {
            EntityEntry entityEntry = _context.Entry(movie);
            entityEntry.State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
