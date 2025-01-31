using Book_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Book_Management_System.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int PublishYear { get; set; }

        public virtual BookDetails? BookDetails { get; set; }
        public List<int> authors { get; set; } = new List<int>();
    }
}
