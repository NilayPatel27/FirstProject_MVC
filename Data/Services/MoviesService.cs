using FirstProject.Data.Base;
using FirstProject.Models;
using FirstProject.ViewModels;
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

        public async Task AddAsync(NewMovieVM movie)
        {
            Movie newMovie = new Movie
            {
                Name = movie.Name,
                Language = movie.Language,
                Category = movie.Category,
                Status = movie.Status,
                Region = movie.Region,
                Stars = movie.Stars,
                ReleDate = movie.ReleDate,
                MovieCategory = movie.MovieCategory,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ActorIds = movie.ActorIds,
                DirectorIds = movie.DirectorIds
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            foreach (var actorId in movie.ActorIds.Split(','))
            {
                Actor_Movie actorMovie = new Actor_Movie
                {
                    ActorId = int.Parse(actorId),
                    MovieId = newMovie.Id
                };
                await _context.Actors_Movies.AddAsync(actorMovie);
            }
            await _context.SaveChangesAsync();

            foreach (var directorId in movie.DirectorIds.Split(','))
            {
                Movie_Director movieDirector = new Movie_Director
                {
                    MovieId = newMovie.Id,
                    DirectorId = int.Parse(directorId)
                };
                await _context.Movies_Directors.AddAsync(movieDirector);
            }
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(n => n.Id == id);
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();

            var result2 = await _context.Actors_Movies.Where(n => n.MovieId == id).ToListAsync();
            _context.Actors_Movies.RemoveRange(result2);
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
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                ActorIds = x.ActorIds,
                DirectorIds = x.DirectorIds
            })?.FirstOrDefault();
            return result;
        }

        public Task UpdateAsync(int id, Movie movie)
        {
            Movie data = new Movie
            {
                Id = id,
                Name = movie.Name,
                Language = movie.Language,
                Category = movie.Category,
                Status = movie.Status,
                Region = movie.Region,
                Stars = movie.Stars,
                ReleDate = movie.ReleDate,
                MovieCategory = movie.MovieCategory,
                CreatedDate = movie.CreatedDate,
                UpdatedDate = DateTime.Now,
                ActorIds = movie.ActorIds,
                DirectorIds = movie.DirectorIds
            };
            EntityEntry entityEntry = _context.Entry(data);
            entityEntry.State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public async Task<NewMovieViewModel> GetNewMovieData()
        {
            var response = new NewMovieViewModel()
            {
                Actors = await _context.Actors.OrderBy(n => n.Name).ToListAsync(),
                Directors = await _context.Directors.OrderBy(n => n.Name).ToListAsync()
            };

            return response;
        }
    }
}
