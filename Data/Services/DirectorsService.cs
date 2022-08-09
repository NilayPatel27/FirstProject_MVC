using FirstProject.Data.Base;
using FirstProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FirstProject.Data.Services
{
    public class DirectorsService : IDirectorsService
    {
        private readonly AppDbContext _context;
        public DirectorsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Director director)
        {
            Director data = new Director
            {
                Name = director.Name,
                Age = director.Age,
                DOB = director.DOB,
                City = director.City,
                Gender = director.Gender,
                Category = director.Category,
                Language = director.Language,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await _context.Directors.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Directors.FirstOrDefaultAsync(n => n.Id == id);
            _context.Directors.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Director>> GetAllAsync()
        {
            string storedProcedure = "spGetDirectors";
            return await _context.Directors.FromSqlRaw(storedProcedure).ToListAsync();
        }

        public async Task<Director> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            List<Director> data = await _context.Directors.FromSqlRaw("spGetDirectorById @Id", parameters).ToListAsync();
            Director result = data.Select(x => new Director
            {
                Id = x.Id,
                Age = x.Age,
                DOB = x.DOB,
                City = x.City,
                Gender = x.Gender,
                Category = x.Category,
                Language = x.Language,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate

            })?.FirstOrDefault();
            return result;
        }

        public Task UpdateAsync(int id, Director director)
        {
            Director data = new Director
            {
                Id = director.Id,
                Name = director.Name,
                Age = director.Age,
                DOB = director.DOB,
                City = director.City,
                Gender = director.Gender,
                Category = director.Category,
                Language = director.Language,
                CreatedDate = director.CreatedDate,
                UpdatedDate = DateTime.Now
            };
            EntityEntry entityEntry = _context.Entry(data);
            entityEntry.State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }

}
