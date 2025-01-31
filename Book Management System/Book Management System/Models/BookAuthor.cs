using System.ComponentModel.DataAnnotations;

namespace Book_Management_System.Models
{
    public class BookAuthor
    {
        public BookAuthor(int BookId, int AuthorId)
        {
            this.BookId = BookId;
            this.AuthorId = AuthorId;
        }
        [Key]
        public int id { get; set; }

        public int BookId { get; set; }
        public virtual Book? Book { get; set; }

        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }
    }
}
