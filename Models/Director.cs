using FirstProject.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace FirstProject.Models
{
    public class Director : BaseEntity
    {
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public string? City { get; set; }
        public bool Gender { get; set; }
        public string? Category { get; set; }
        public string? Language { get; set; }

        //Relatioships
        public ICollection<Director_Actor>? Directors_Actors { get; set; }
        public ICollection<Movie_Director>? Movies_Directors { get; set; }

    }
}
