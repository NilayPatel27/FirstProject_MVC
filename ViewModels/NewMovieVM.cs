using FirstProject.Data.Base;

namespace FirstProject.ViewModels
{
    public class NewMovieVM : BaseEntity
    {
        public string? Language { get; set; }
        public string? Category { get; set; }
        public bool Status { get; set; }
        public string? Region { get; set; }
        public int Stars { get; set; }
        public DateTime ReleDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public string? DirectorIds { get; set; }
        public string? ActorIds { get; set; }
    }
}
