using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Management_System.Models
{
    public class Book
    {
        public Book(string? Title, int PublishYear)
        {
            this.Title = Title;
            this.PublishYear = PublishYear;
        }
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public int PublishYear { get; set; }

        public virtual BookDetails? BookDetails { get; set; } 
        public virtual List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
