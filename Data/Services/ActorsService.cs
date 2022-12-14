using System.Data;
using FirstProject.Data.Base;
using FirstProject.Models;
using FirstProject.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FirstProject.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Actor actor)
        {
            Actor data = new Actor
            {
                Name = actor.Name,
                Age = actor.Age,
                DOB = actor.DOB,
                City = actor.City,
                Gender = actor.Gender,
                Hobbie = actor.Hobbie,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await _context.Actors.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);
            _context.Actors.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            string storedProcedure = "spGetActors";
            return await _context.Actors.FromSqlRaw(storedProcedure).ToListAsync();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            List<Actor> data = await _context.Actors.FromSqlRaw("spGetActorById @Id", parameters).ToListAsync();
            Actor result = data.Select(x => new Actor
            {
                Id = x.Id,
                Age = x.Age,
                DOB = x.DOB,
                City = x.City,
                Gender = x.Gender,
                Name = x.Name,
                Hobbie = x.Hobbie,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate
            })?.FirstOrDefault();
            
            return result;
        }
        public async Task<ActorsMoviesViewModel> ActorsMoviesViewModelAsync(int id)
        {
            var response = new ActorsMoviesViewModel()
            {
                Movies = await _context.Movies.Where(n => n.ActorIds.Contains(id.ToString())).ToListAsync()
            };

            return response;
        }

        public Task UpdateAsync(int id, Actor newActor)
        {
            Actor data = new Actor
            {
                Id = newActor.Id,
                Name = newActor.Name,
                Age = newActor.Age,
                DOB = newActor.DOB,
                City = newActor.City,
                Gender = newActor.Gender,
                Hobbie = newActor.Hobbie,
                CreatedDate = newActor.CreatedDate,
                UpdatedDate = DateTime.Now
            };
            EntityEntry entityEntry = _context.Entry(data);
            entityEntry.State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
