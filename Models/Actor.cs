using FirstProject.Data.Base;

namespace FirstProject.Models
{
    public class Actor :BaseEntity
    {
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public string? City { get; set; }
        public bool Gender { get; set; }
        public string? Hobbie { get; set; }

        //Relatioships
        public ICollection<Actor_Movie>? Actors_Movies { get; set; }
        public ICollection<Director_Actor>? Directors_Actors { get; set; }

    }
}
