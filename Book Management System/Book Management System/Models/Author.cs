using System.ComponentModel.DataAnnotations;

namespace Book_Management_System.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
