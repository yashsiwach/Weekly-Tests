using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryPattern.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
    }
}
