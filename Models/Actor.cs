using System.ComponentModel.DataAnnotations;
using FirstProject.Data.Base;

namespace FirstProject.Models
{
    public class Actor : BaseEntity
    {
        [Required (ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public string? Hobbie { get; set; }

        //Relatioships
        public ICollection<Actor_Movie>? Actors_Movies { get; set; }
        public ICollection<Director_Actor>? Directors_Actors { get; set; }

    }
}
