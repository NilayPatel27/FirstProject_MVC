namespace FirstProject.Models
{
    public class Director_Actor
    {
        public int DirectorId { get; set; }
        public Director? Director { get; set; }
        public int ActorId { get; set; }
        public Actor? Actor { get; set; }
    }
}
