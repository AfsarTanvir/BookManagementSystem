using System.ComponentModel.DataAnnotations;

namespace Book_Management_System.Models
{
    public class BookDetails
    {
        [Key]
        public int Id { get; set; }
        public string? ISBN { get; set; }
        public string? Summary { get; set; }

        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}
