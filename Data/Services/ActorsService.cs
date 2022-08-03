using System.Data;
using FirstProject.Data.Base;
using FirstProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FirstProject.Data.Services
{
    public class ActorsService : BaseEntity, IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
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
                Hobbie = x.Hobbie

            })?.FirstOrDefault();
            return result;
        }

        public Task UpdateAsync(int id, Actor newActor)
        {
            EntityEntry entityEntry = _context.Entry(newActor);
            entityEntry.State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
