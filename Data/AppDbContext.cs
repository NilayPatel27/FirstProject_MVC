using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });
            
            modelBuilder.Entity<Director_Actor>().HasKey(am => new
            {
                am.DirectorId,
                am.ActorId
            });

            modelBuilder.Entity<Movie_Director>().HasKey(am => new
            {
                am.DirectorId,
                am.MovieId
            });


            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);

            modelBuilder.Entity<Director_Actor>().HasOne(m => m.Director).WithMany(da => da.Directors_Actors).HasForeignKey(m => m.DirectorId);
            modelBuilder.Entity<Director_Actor>().HasOne(m => m.Actor).WithMany(da => da.Directors_Actors).HasForeignKey(m => m.ActorId);

            modelBuilder.Entity<Movie_Director>().HasOne(m => m.Movie).WithMany(md => md.Movies_Directors).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Movie_Director>().HasOne(m => m.Director).WithMany(md => md.Movies_Directors).HasForeignKey(m => m.DirectorId);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Director_Actor> Directors_Actors { get; set; }
        public DbSet<Movie_Director> Movies_Directors { get; set; }

    }
}
