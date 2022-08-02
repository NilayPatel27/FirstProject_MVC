using System.ComponentModel.DataAnnotations;

namespace FirstProject.Data.Base
{
    public class BaseEntity
    {   [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
